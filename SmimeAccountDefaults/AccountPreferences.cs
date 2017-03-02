using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using SmimeAccountDefaults.Properties;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace SmimeAccountDefaults
{
    class AccountPreferences
    {
        readonly Dictionary<string, AccountPreference> preferences = new Dictionary<string, AccountPreference>(StringComparer.OrdinalIgnoreCase);

        public void LoadFromString(string str)
        {
            preferences.Clear();

            // Get the list of configured accounts 
            var accts = GetOutlookAccounts();

            if (!string.IsNullOrWhiteSpace(str))
            {
                try
                {
                    var prefs = JsonConvert.DeserializeObject<List<AccountPreference>>(str);
                    prefs.ForEach(p =>
                                  {
                                      // only update if the key exists because those are the known accounts
                                      if (accts.Contains(p.SmtpAddress))
                                          preferences[p.SmtpAddress] = p;
                                  });
                }
                catch
                {
                    // bad prefs
                }
            }

            // get any that aren't present and add defaults
            var missing = accts.Except(preferences.Keys);
            foreach (var m in missing)
            {
                preferences[m] = new AccountPreference { SmtpAddress = m };
            }
        }

        public void SaveToSettings(IEnumerable<AccountPreference> prefs)
        {
            // Update the local cache
            preferences.Clear();

            // Filter out accounts that may not exist
            var accts = GetOutlookAccounts();
            prefs = prefs.Where(p => accts.Contains(p.SmtpAddress)).ToList();

            foreach (var p in prefs)
            {
                preferences[p.SmtpAddress] = p;
            }


            // get any that aren't present and add defaults
            var missing = accts.Except(preferences.Keys);
            foreach (var m in missing)
            {
                preferences[m] = new AccountPreference { SmtpAddress = m };
            }

            var str = JsonConvert.SerializeObject(prefs);
            Settings.Default.AccountPreferences = str;
            Settings.Default.Save();
        }

        public AccountPreference this[string smtpAddress] 
        {
            get
            {
                if (!preferences.TryGetValue(smtpAddress, out AccountPreference pref))
                {
                    pref = new AccountPreference
                    {
                        SmtpAddress = smtpAddress
                    };
                    preferences[smtpAddress] = pref;
                }
                return pref;
            }
        }
        

        public IEnumerable<AccountPreference> Preferences => preferences.Values;

        static HashSet<string> GetOutlookAccounts()
        {
            var set = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (Outlook.Account acct in Globals.ThisAddIn.Application.Session.Accounts)
            {
                set.Add(acct.SmtpAddress);
            }

            return set;
        }
    }
}
