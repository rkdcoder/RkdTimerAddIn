namespace RkdTimerAddIn
{
    partial class TimerRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public TimerRibbon()
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
            this.tab1 = this.Factory.CreateRibbonTab();
            this.config = this.Factory.CreateRibbonGroup();
            this.editSecondsToRed = this.Factory.CreateRibbonEditBox();
            this.btnInsertTimer = this.Factory.CreateRibbonButton();
            this.tab1.SuspendLayout();
            this.config.SuspendLayout();
            this.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab1.Groups.Add(this.config);
            this.tab1.Label = "Rkd Timer";
            this.tab1.Name = "tab1";
            // 
            // config
            // 
            this.config.Items.Add(this.editSecondsToRed);
            this.config.Items.Add(this.btnInsertTimer);
            this.config.Label = "Configuration";
            this.config.Name = "config";
            // 
            // editSecondsToRed
            // 
            this.editSecondsToRed.Label = "Seconds to Red:";
            this.editSecondsToRed.Name = "editSecondsToRed";
            this.editSecondsToRed.Text = null;
            // 
            // btnInsertTimer
            // 
            this.btnInsertTimer.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnInsertTimer.Image = global::RkdTimerAddIn.Properties.Resources.RkdTimer;
            this.btnInsertTimer.Label = "Insert Timer";
            this.btnInsertTimer.Name = "btnInsertTimer";
            this.btnInsertTimer.ShowImage = true;
            this.btnInsertTimer.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnInsertTimer_Click);
            // 
            // TimerRibbon
            // 
            this.Name = "TimerRibbon";
            this.RibbonType = "Microsoft.PowerPoint.Presentation";
            this.Tabs.Add(this.tab1);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.TimerRibbon_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.config.ResumeLayout(false);
            this.config.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup config;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnInsertTimer;
        internal Microsoft.Office.Tools.Ribbon.RibbonEditBox editSecondsToRed;
    }

    partial class ThisRibbonCollection
    {
        internal TimerRibbon TimerRibbon
        {
            get { return this.GetRibbon<TimerRibbon>(); }
        }
    }
}
