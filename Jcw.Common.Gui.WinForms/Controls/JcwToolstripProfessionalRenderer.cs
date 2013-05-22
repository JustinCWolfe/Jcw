using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Jcw.Common.Gui.WinForms.Controls
{
    public class JcwProfessionalColorTable : ProfessionalColorTable
    {
        #region MenuStrip Color Overrides

        public override Color MenuBorder
        {
            get { return Color.DimGray; }
        }

        public override Color MenuStripGradientBegin
        {
            get { return Color.LightGray; }
        }

        public override Color MenuStripGradientEnd
        {
            get { return Color.DarkGray; }
        }

        #endregion

        #region ToolStripButton Color Overrides

        public override Color ButtonSelectedBorder
        {
            get { return Color.IndianRed; }
        }

        public override Color ButtonSelectedGradientBegin
        {
            get { return Color.LightYellow; }
        }

        public override Color ButtonSelectedGradientEnd
        {
            get { return Color.LightYellow; }
        }

        public override Color ButtonCheckedGradientBegin
        {
            get { return Color.LightBlue; }
        }

        public override Color ButtonCheckedGradientEnd
        {
            get { return Color.LightBlue; }
        }

        public override Color ButtonPressedGradientBegin
        {
            get { return Color.LightSteelBlue; }
        }

        public override Color ButtonPressedGradientEnd
        {
            get { return Color.LightSteelBlue; }
        }

        #endregion

        #region ToolStripMenuItem Color Overrides

        public override Color MenuItemBorder
        {
            get { return Color.IndianRed; }
        }

        public override Color MenuItemSelected
        {
            get { return Color.LightGray; }
        }

        public override Color MenuItemSelectedGradientBegin
        {
            get { return Color.Gainsboro; }
        }

        public override Color MenuItemSelectedGradientEnd
        {
            get { return Color.Silver; }
        }

        public override Color MenuItemPressedGradientBegin
        {
            get { return Color.WhiteSmoke; }
        }

        public override Color MenuItemPressedGradientMiddle
        {
            get { return Color.Gainsboro; }
        }

        public override Color MenuItemPressedGradientEnd
        {
            get { return Color.Gray; }
        }

        #endregion

        #region StatusStrip Color Overrides

        public override Color StatusStripGradientBegin
        {
            get { return Color.LightGray; }
        }

        public override Color StatusStripGradientEnd
        {
            get { return Color.DarkGray; }
        }

        #endregion

        #region ToolStripDropDownMenu Color Overrides

        public override Color ImageMarginGradientBegin
        {
            get { return Color.Gainsboro; }
        }

        public override Color ImageMarginGradientMiddle
        {
            get { return Color.LightGray; }
        }

        public override Color ImageMarginGradientEnd
        {
            get { return Color.Gray; }
        }

        #endregion
    }

    public partial class JcwToolstripProfessionalRenderer : ToolStripProfessionalRenderer
    {
        public JcwToolstripProfessionalRenderer ()
            : this (new JcwProfessionalColorTable ())
        {
        }

        public JcwToolstripProfessionalRenderer (ProfessionalColorTable colorTable)
            : base (colorTable)
        {
        }

        #region Overrides

        protected override void OnRenderSeparator (ToolStripSeparatorRenderEventArgs e)
        {
            if (e.Vertical)
            {
                using (Brush brush = new SolidBrush (Color.Black))
                {
                    float separatorWidth = 2f;
                    float separatorXCoord = (e.Item.Width / 2) - (separatorWidth / 2);
                    using (Pen pen = new Pen (brush, separatorWidth))
                    {
                        e.Graphics.DrawLine (pen, separatorXCoord, -e.Item.Size.Height, separatorXCoord, e.Item.Size.Height);
                    }
                }
            }
            else
            {
                base.OnRenderSeparator (e);
            }
        }

        #endregion
    }
}
