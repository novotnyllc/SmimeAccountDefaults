using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;

namespace SmimeAccountDefaults
{
    class ConfigurationWindowViewModel : ObservableObject
    {
        Outlook.Application application = Globals.ThisAddIn.Application;
        public ConfigurationWindowViewModel()
        {
            foreach (Outlook.Account acct in application.Session.Accounts)
            {
                accounts.Add(acct.SmtpAddress);
            }

            selectedAccount = accounts.FirstOrDefault();
        }

        readonly List<string> accounts = new List<string>();
        string selectedAccount;
        public IEnumerable<string> Accounts => accounts;

        public string SelectedAccount
        {
            get { return selectedAccount; }
            set
            {
                Set(ref selectedAccount, value);
            }
        }
    }
}
