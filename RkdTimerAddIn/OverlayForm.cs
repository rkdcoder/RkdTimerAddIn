using System.Drawing;
using System.Windows.Forms;

namespace RkdTimerAddIn
{
    public partial class OverlayForm : Form
    {
        public OverlayForm()
        {
            InitializeComponent();
            ConfigureForm();
        }

        private void ConfigureForm()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.TopMost = true;
            this.ShowInTaskbar = false;
            this.DoubleBuffered = true;

            this.BackColor = Color.Black;
            this.Opacity = 0.65;
        }

        public void UpdateTime(string text, bool warning)
        {
            lblTime.Text = text;

            if (warning)
                lblTime.ForeColor = Color.FromArgb(255, 80, 80);
            else
                lblTime.ForeColor = Color.FromArgb(120, 255, 120);
        }

        public void UpdateFontSize(float size)
        {
            if (lblTime.Font.Size != size)
            {
                lblTime.Font = new Font("Segoe UI", size, FontStyle.Bold);
            }
        }
    }
}
