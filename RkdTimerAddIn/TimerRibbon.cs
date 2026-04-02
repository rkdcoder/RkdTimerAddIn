using Microsoft.Office.Core;
using Microsoft.Office.Tools.Ribbon;
using System;

namespace RkdTimerAddIn
{
    public partial class TimerRibbon
    {
        private void TimerRibbon_Load(object sender, RibbonUIEventArgs e)
        {

        }

        private void btnInsertTimer_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                var app = Globals.ThisAddIn.Application;
                Microsoft.Office.Interop.PowerPoint.Slide slide = app.ActiveWindow.View.Slide;

                foreach (Microsoft.Office.Interop.PowerPoint.Shape shape in slide.Shapes)
                {
                    if (shape.Name == "RKD_TIMER_ANCHOR") return;
                }

                Microsoft.Office.Interop.PowerPoint.Shape anchor = slide.Shapes.AddShape(
                    MsoAutoShapeType.msoShapeRectangle,
                    app.ActivePresentation.PageSetup.SlideWidth - 300, 20, 280, 60);

                anchor.Name = "RKD_TIMER_ANCHOR";

                anchor.Fill.Visible = MsoTriState.msoFalse;
                anchor.Line.DashStyle = MsoLineDashStyle.msoLineDash;
                anchor.Line.ForeColor.RGB = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Gray);

                anchor.TextFrame.TextRange.Text = "Rkd Timer Area (Resize as desired)";
                anchor.TextFrame.TextRange.Font.Color.RGB = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Gray);
                anchor.TextFrame.TextRange.Font.Size = 12;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Open a slide to insert the timer.", "Alert");
            }
        }
    }
}
