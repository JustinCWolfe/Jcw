using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

using Jcw.Common;

namespace Jcw.Common.Gui.WinForms
{
    public static class Utilities
    {
        #region Public Static Methods

        /// <summary>
        /// Display in a standard way usage information to be displayed for command line applications
        /// </summary>
        public static void Usage (string usage)
        {
            string program = AppDomain.CurrentDomain.FriendlyName;
            Console.WriteLine (String.Format ("\nUsage: {0}\n{1}", program, usage));
        }

        public static Cursor CreateCursor (Bitmap bmp, int xHotSpot, int yHotSpot)
        {
            // create icon info struct 
            NativeMethods.IconInfo tmp = new NativeMethods.IconInfo ();

            IntPtr ptr = bmp.GetHicon ();

            // call native method to populate icon info struct with icon information from bitmap image
            NativeMethods.GetIconInfo (ptr, ref tmp);

            // set hot spot on cursor
            tmp.xHotspot = xHotSpot;
            tmp.yHotspot = yHotSpot;
            // marks image as a cursor rather than an icon
            tmp.fIcon = false;

            // native method to return pointer to the new cursor icon
            ptr = NativeMethods.CreateIconIndirect (ref tmp);

            return new Cursor (ptr);
        }

        public static ToolStripDropDownDirection GetToolStripDropDownDirection (Control ctl, Point startPoint)
        {
            // get the location of the bottom right corner of the context menu 
            Point endPoint = startPoint;
            endPoint.Offset (ctl.Size.Width, ctl.Size.Height);

            Rectangle totalScreenSize = Screen.PrimaryScreen.Bounds;

            // set the toolstrip drop down direction based on the control bounds and the screen size 
            if (endPoint.X > totalScreenSize.Width && endPoint.Y > totalScreenSize.Height)
                return ToolStripDropDownDirection.AboveLeft;
            else if (endPoint.X > totalScreenSize.Width)
                return ToolStripDropDownDirection.BelowLeft;
            else if (endPoint.Y > totalScreenSize.Height)
                return ToolStripDropDownDirection.AboveRight;
            else
                return ToolStripDropDownDirection.BelowRight;
        }

        #endregion
    }
}
