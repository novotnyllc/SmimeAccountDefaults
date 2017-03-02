using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmimeAccountDefaults.Properties;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace SmimeAccountDefaults
{
    class MailItemMonitor
    {
        readonly Outlook.Application application;

        public MailItemMonitor(Outlook.Application application)
        {
            this.application = application;
        }

        const string AddressToCheck = "oren@novotny.org";

        const string PR_SECURITY_FLAGS = @"http://schemas.microsoft.com/mapi/proptag/0x6E010003";
        const string PR_SMTP_ADDRESS = @"http://schemas.microsoft.com/mapi/proptag/0x39FE001E";

        const byte SECFLAG_ENCRYPTED = 0x01;
        const byte SECFLAG_SIGNED = 0x02;

        public void OnItemSend(object item, ref bool cancel)
        {
            if (item is Outlook.MailItem)
            {
                OnMailItemSend((Outlook.MailItem)item);
            }
        }
                        
        void OnMailItemSend(Outlook.MailItem item)
        {
            var address = item.SendUsingAccount?.SmtpAddress ?? (application.Session.Accounts.Count > 0 ? 
                                                                 application.Session.Accounts[1].SmtpAddress : null);
                    

            if (string.Equals(address, AddressToCheck, StringComparison.OrdinalIgnoreCase))
            {
                // coming from the address we want to check. 
                if(Settings.Default.IsSuspended)
                    return;

                var secFlags = (int)item.PropertyAccessor.GetProperty(PR_SECURITY_FLAGS);

                secFlags = secFlags | SECFLAG_SIGNED;

                item.PropertyAccessor.SetProperty(PR_SECURITY_FLAGS, secFlags);   
            }
        }

        

    }
}


