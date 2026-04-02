using System;
using System.Drawing;
using System.Windows.Forms;

namespace RkdTimerAddIn
{
    public partial class OverlayForm : Form
    {
        private float _progress = 1.0f;
        private string _excessText = null;
        private float _fontSize = 24f;

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
            this.Opacity = 0.80;
        }

        public void UpdateProgress(float progress, string excessText)
        {
            _progress = Math.Max(0, Math.Min(1, progress));
            _excessText = excessText;
            this.Invalidate();
        }

        public void UpdateFontSize(float size)
        {
            _fontSize = size;
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            Rectangle rect = this.ClientRectangle;
            int padding = 4;
            Rectangle barRect = new Rectangle(padding, padding, rect.Width - padding * 2, rect.Height - padding * 2);

            using (SolidBrush bgBrush = new SolidBrush(Color.FromArgb(40, 40, 40)))
            {
                g.FillRectangle(bgBrush, barRect);
            }

            if (string.IsNullOrEmpty(_excessText))
            {
                int fillWidth = (int)(barRect.Width * _progress);
                if (fillWidth > 0)
                {
                    Rectangle fillRect = new Rectangle(barRect.X, barRect.Y, fillWidth, barRect.Height);

                    int r, gValue;
                    int b = 0;

                    if (_progress > 0.5f)
                    {
                        float percent = (1.0f - _progress) * 2.0f;
                        r = (int)(255 * percent);
                        gValue = 255;
                    }
                    else
                    {
                        float percent = _progress * 2.0f;
                        r = 255;
                        gValue = (int)(255 * percent);
                    }

                    r = Math.Max(0, Math.Min(255, r));
                    gValue = Math.Max(0, Math.Min(255, gValue));

                    Color barColor = Color.FromArgb(r, gValue, b);

                    using (SolidBrush fillBrush = new SolidBrush(barColor))
                    {
                        g.FillRectangle(fillBrush, fillRect);
                    }
                }
            }
            else
            {
                using (Font font = new Font("Segoe UI", _fontSize, FontStyle.Bold))
                using (SolidBrush textBrush = new SolidBrush(Color.FromArgb(255, 80, 80)))
                using (StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                {
                    g.DrawString($"+{_excessText}", font, textBrush, rect, sf);
                }
            }
        }
    }
}