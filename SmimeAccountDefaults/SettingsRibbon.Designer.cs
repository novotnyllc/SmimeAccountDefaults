namespace SmimeAccountDefaults
{
    partial class SettingsRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public SettingsRibbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Microsoft.Office.Tools.Ribbon.RibbonDialogLauncher ribbonDialogLauncherImpl1 = this.Factory.CreateRibbonDialogLauncher();
            this.tab1 = this.Factory.CreateRibbonTab();
            this.securityGroup = this.Factory.CreateRibbonGroup();
            this.toggleSuspend = this.Factory.CreateRibbonToggleButton();
            this.tab1.SuspendLayout();
            this.securityGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab1.ControlId.OfficeId = "TabOptions";
            this.tab1.Groups.Add(this.securityGroup);
            this.tab1.Label = "TabOptions";
            this.tab1.Name = "tab1";
            this.tab1.Position = this.Factory.RibbonPosition.AfterOfficeId("GroupRightsManagement");
            // 
            // securityGroup
            // 
            ribbonDialogLauncherImpl1.ScreenTip = "Preferences";
            this.securityGroup.DialogLauncher = ribbonDialogLauncherImpl1;
            this.securityGroup.Items.Add(this.toggleSuspend);
            this.securityGroup.Label = "S/MIME";
            this.securityGroup.Name = "securityGroup";
            this.securityGroup.DialogLauncherClick += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.securityGroup_DialogLauncherClick);
            // 
            // toggleSuspend
            // 
            this.toggleSuspend.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.toggleSuspend.Label = "Suspend Account Defaults";
            this.toggleSuspend.Name = "toggleSuspend";
            this.toggleSuspend.OfficeImageId = "SignatureShow";
            this.toggleSuspend.ShowImage = true;
            this.toggleSuspend.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.toggleSuspend_Click);
            // 
            // SettingsRibbon
            // 
            this.Name = "SettingsRibbon";
            this.RibbonType = "Microsoft.Outlook.Mail.Compose";
            this.Tabs.Add(this.tab1);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.SettingsRibbon_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.securityGroup.ResumeLayout(false);
            this.securityGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup securityGroup;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton toggleSuspend;
    }

    partial class ThisRibbonCollection
    {
        internal SettingsRibbon SettingsRibbon
        {
            get { return this.GetRibbon<SettingsRibbon>(); }
        }
    }
}
