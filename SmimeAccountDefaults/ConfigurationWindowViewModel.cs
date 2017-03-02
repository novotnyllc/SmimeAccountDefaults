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
        public ConfigurationWindowViewModel()
        {
            SelectedAccount = Accounts.FirstOrDefault();
        }

        AccountPreference selectedAccount;

        // Create a copy for modifications
        public IEnumerable<AccountPreference> Accounts { get; } = Globals.ThisAddIn.AccountPreferences.Preferences.Select(ap => ap.Clone()).ToList();

        public AccountPreference SelectedAccount
        {
            get { return selectedAccount; }
            set
            {
                Set(ref selectedAccount, value);
            }
        }


        public void Save()
        {
            Globals.ThisAddIn.AccountPreferences.SaveToSettings(Accounts);
        }
    }
}
