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
        readonly AccountPreferences preferences;

        public MailItemMonitor()
        {
            application = Globals.ThisAddIn.Application;
            preferences = Globals.ThisAddIn.AccountPreferences;
        }

        const string PR_SECURITY_FLAGS = @"http://schemas.microsoft.com/mapi/proptag/0x6E010003";

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
            // Don't do anything here if we're suspended
            if (Settings.Default.IsSuspended)
                return;

            // Always look up & set the account object as it may get nulled out if auto-saved or in drafts
            // look up account based on sender address in the message
            var userAddress = item.SenderEmailAddress;
            var userAccount = application.Session.Accounts.OfType<Outlook.Account>()
                                                            .FirstOrDefault(acct => acct.CurrentUser.Address == userAddress);
            var address = userAccount?.SmtpAddress;
            if (userAccount != null)
                item.SendUsingAccount = userAccount;
            
            if (address == null)
                return; // can't find it

            // Get prefs for account
            var pref = preferences[address];

            var secFlags = (int)item.PropertyAccessor.GetProperty(PR_SECURITY_FLAGS);

            if (pref.Sign)
            {
                secFlags = secFlags | SECFLAG_SIGNED;
            }

            if (pref.Encrypt)
            {
                secFlags = secFlags | SECFLAG_ENCRYPTED;
            }

            item.PropertyAccessor.SetProperty(PR_SECURITY_FLAGS, secFlags);   
            
        }
    }
}
