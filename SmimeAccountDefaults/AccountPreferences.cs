using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
                    var dcjs = new DataContractJsonSerializer(typeof(List<AccountPreference>));
                    using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(str)))
                    {
                        var prefs = (List<AccountPreference>)dcjs.ReadObject(ms);
                        prefs.ForEach(p =>
                                      {
                                          // only update if the key exists because those are the known accounts
                                          if (accts.Contains(p.SmtpAddress))
                                              preferences[p.SmtpAddress] = p;
                                      });
                    }
                    
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

            var dcjs = new DataContractJsonSerializer(typeof(IEnumerable<AccountPreference>));
            using (var ms = new MemoryStream())
            {
                dcjs.WriteObject(ms, prefs);

                ms.Position = 0;
                using (var sr = new StreamReader(ms))
                {
                    Settings.Default.AccountPreferences = sr.ReadToEnd();
                    Settings.Default.Save();
                }
                    
            }
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
