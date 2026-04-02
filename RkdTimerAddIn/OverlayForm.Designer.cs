namespace RkdTimerAddIn
{
    partial class OverlayForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTime;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTime = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // lblTime
            this.lblTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTime.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblTime.ForeColor = System.Drawing.Color.Lime;
            this.lblTime.BackColor = System.Drawing.Color.Transparent;
            this.lblTime.Name = "lblTime";

            // OverlayForm
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(400, 120);
            this.Controls.Add(this.lblTime);
            this.Name = "OverlayForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;

            this.ResumeLayout(false);
        }
    }
}