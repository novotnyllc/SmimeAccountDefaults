using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;

namespace SmimeAccountDefaults
{
    public partial class SettingsRibbon
    {
        private void SettingsRibbon_Load(object sender, RibbonUIEventArgs e)
        {

        }

        private void securityGroup_DialogLauncherClick(object sender, RibbonControlEventArgs e)
        {
            var configWindow = new ConfigurationWindow();
            configWindow.ShowDialog();
            
        }
    }
}
