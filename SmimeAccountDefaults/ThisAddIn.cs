using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using SmimeAccountDefaults.Properties;
using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;

namespace SmimeAccountDefaults
{
    public partial class ThisAddIn
    {
        MailItemMonitor monitor;
        SettingsRibbon ribbon;

        internal AccountPreferences AccountPreferences { get; } = new AccountPreferences();

        void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            DoUpgrade(Settings.Default);
            AccountPreferences.LoadFromString(Settings.Default.AccountPreferences);

            monitor = new MailItemMonitor();
            Application.ItemSend += monitor.OnItemSend;

            ribbon = Globals.Ribbons.SettingsRibbon;
        }

        // the name of the setting that flags whether we
        // should perform an upgrade or not
        const string UpgradeFlag = "UpgradeRequired";

        static void DoUpgrade(ApplicationSettingsBase settings)
        {
            try
            {
                // attempt to read the upgrade flag
                if ((bool)settings[UpgradeFlag])
                {
                    // upgrade the settings to the latest version
                    settings.Upgrade();

                    // clear the upgrade flag
                    settings[UpgradeFlag] = false;
                    settings.Save();
                }
                else
                {
                    // the settings are up to date
                }
            }
            catch (SettingsPropertyNotFoundException ex)
            {
                // notify the developer that the upgrade
                // flag should be added to the settings file
                throw ex;
            }
        }
    

        void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
            // Note: Outlook no longer raises this event. If you have code that 
            //    must run when Outlook shuts down, see http://go.microsoft.com/fwlink/?LinkId=506785
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
