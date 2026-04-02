using System;
using System.Windows.Forms;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;

namespace RkdTimerAddIn
{
    public partial class ThisAddIn
    {
        private DateTime slideStart;
        private System.Windows.Forms.Timer timer;
        private OverlayForm form;
        private int currentWarningSeconds = 30;
        private bool timerActiveForCurrentSlide = false;
        private PowerPoint.Shape hiddenAnchorShape = null;

        private void RestoreHiddenAnchor()
        {
            if (hiddenAnchorShape != null)
            {
                try { hiddenAnchorShape.Visible = Microsoft.Office.Core.MsoTriState.msoTrue; } catch { }
                hiddenAnchorShape = null;
            }
        }

        private void ThisAddIn_Startup(object sender, EventArgs e)
        {
            Application.SlideShowBegin += OnSlideShowBegin;
            Application.SlideShowNextSlide += OnNextSlide;
            Application.SlideShowEnd += OnSlideShowEnd;

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 50;
            timer.Tick += UpdateTimer;
        }

        private void OnSlideShowBegin(PowerPoint.SlideShowWindow Wn)
        {
            if (form == null || form.IsDisposed)
            {
                form = new OverlayForm();
                var forceHandle = form.Handle;
            }
            SetupTimerForSlide(Wn);
        }

        private void OnNextSlide(PowerPoint.SlideShowWindow Wn)
        {
            SetupTimerForSlide(Wn);
        }

        private void SetupTimerForSlide(PowerPoint.SlideShowWindow Wn)
        {
            timer.Stop();
            timerActiveForCurrentSlide = false;

            if (form != null)
            {
                form.Hide();
            }

            RestoreHiddenAnchor();

            var slide = TryGetSlide(Wn);
            if (slide == null) return;

            PowerPoint.Shape anchorShape = null;
            foreach (PowerPoint.Shape shape in slide.Shapes)
            {
                if (shape.Name.Trim().Equals("RKD_TIMER_ANCHOR", StringComparison.OrdinalIgnoreCase))
                {
                    anchorShape = shape;
                    break;
                }
            }

            if (anchorShape != null)
            {
                int tempoConfigurado = 30;
                if (Globals.Ribbons.TimerRibbon != null)
                {
                    string textoRibbon = Globals.Ribbons.TimerRibbon.editSecondsToRed.Text;
                    if (int.TryParse(textoRibbon, out int valor) && valor > 0)
                    {
                        tempoConfigurado = valor;
                    }
                }

                currentWarningSeconds = tempoConfigurado;
                PositionForm(Wn, anchorShape);

                anchorShape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                hiddenAnchorShape = anchorShape;

                slideStart = DateTime.Now;
                timerActiveForCurrentSlide = true;

                form.UpdateProgress(1.0f, null);

                var pptWindow = new WindowWrapper((IntPtr)Wn.HWND);
                form.Show(pptWindow);
                form.TopMost = true;

                timer.Start();
            }
        }

        private void UpdateTimer(object sender, EventArgs e)
        {
            if (!timerActiveForCurrentSlide || form == null || form.IsDisposed || !form.IsHandleCreated)
                return;

            var elapsed = DateTime.Now - slideStart;
            double elapsedSeconds = elapsed.TotalSeconds;

            try
            {
                if (elapsedSeconds <= currentWarningSeconds)
                {
                    float progress = 1.0f - (float)(elapsedSeconds / currentWarningSeconds);
                    form.UpdateProgress(progress, null);
                }
                else
                {
                    TimeSpan excess = TimeSpan.FromSeconds(elapsedSeconds - currentWarningSeconds);
                    string excessText = excess.ToString(@"hh\:mm\:ss");
                    form.UpdateProgress(0f, excessText);
                }
            }
            catch { }
        }

        private void OnSlideShowEnd(PowerPoint.Presentation Pres)
        {
            timer?.Stop();
            if (form != null && !form.IsDisposed && form.IsHandleCreated)
            {
                form.Hide();
            }

            RestoreHiddenAnchor();
            try
            {
                foreach (PowerPoint.Slide s in Pres.Slides)
                {
                    foreach (PowerPoint.Shape sh in s.Shapes)
                    {
                        if (sh.Name.Trim().Equals("RKD_TIMER_ANCHOR", StringComparison.OrdinalIgnoreCase))
                        {
                            sh.Visible = Microsoft.Office.Core.MsoTriState.msoTrue;
                        }
                    }
                }
            }
            catch { }
        }

        private void PositionForm(PowerPoint.SlideShowWindow Wn, PowerPoint.Shape anchorShape)
        {
            try
            {
                Screen screen = Screen.FromHandle((IntPtr)Wn.HWND);

                float screenWidth = screen.Bounds.Width;
                float screenHeight = screen.Bounds.Height;

                float slideWidth = Wn.Presentation.PageSetup.SlideWidth;
                float slideHeight = Wn.Presentation.PageSetup.SlideHeight;

                float screenRatio = screenWidth / screenHeight;
                float slideRatio = slideWidth / slideHeight;

                float scale, offsetX = 0, offsetY = 0;

                if (screenRatio > slideRatio)
                {
                    scale = screenHeight / slideHeight;
                    offsetX = (screenWidth - (slideWidth * scale)) / 2f;
                }
                else
                {
                    scale = screenWidth / slideWidth;
                    offsetY = (screenHeight - (slideHeight * scale)) / 2f;
                }

                int x = (int)(screen.Bounds.Left + offsetX + (anchorShape.Left * scale));
                int y = (int)(screen.Bounds.Top + offsetY + (anchorShape.Top * scale));
                int w = (int)(anchorShape.Width * scale);
                int h = (int)(anchorShape.Height * scale);

                form.Location = new System.Drawing.Point(x, y);
                form.Size = new System.Drawing.Size(w, h);

                float fontSize = Math.Max(12f, h * 0.4f);
                form.UpdateFontSize(fontSize);
            }
            catch { }
        }

        private PowerPoint.Slide TryGetSlide(PowerPoint.SlideShowWindow Wn)
        {
            try { return Wn?.View?.Slide; } catch { return null; }
        }

        private void ThisAddIn_Shutdown(object sender, EventArgs e)
        {
            timer?.Stop();
            timer?.Dispose();
            if (form != null && !form.IsDisposed) form.Dispose();
        }

        #region VSTO generated code
        private void InternalStartup()
        {
            this.Startup += new EventHandler(ThisAddIn_Startup);
            this.Shutdown += new EventHandler(ThisAddIn_Shutdown);
        }
        #endregion

        public class WindowWrapper : System.Windows.Forms.IWin32Window
        {
            private IntPtr _hwnd;
            public WindowWrapper(IntPtr handle) { _hwnd = handle; }
            public IntPtr Handle { get { return _hwnd; } }
        }
    }
}