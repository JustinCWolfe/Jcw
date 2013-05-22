using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Jcw.Common.Controls
{
    public partial class JcwListView : ListView
    {
        #region Constructors

        public JcwListView ()
            : base ()
        {
            InitializeComponent ();
        }

        #endregion

        #region Event Handlers

        private void JcwListView_Layout (object sender, LayoutEventArgs e)
        {
            // Reset the tag on each item to re-enable the workaround in the MouseMove event handler.
            foreach (ListViewItem item in this.Items)
            {
                if (item != null)
                    item.Tag = null;
            }
        }

        private void JcwListView_DrawColumnHeader (object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.Graphics.FillRectangle (Brushes.LightGray, e.Bounds);
            e.DrawText (TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter);
        }

        private void JcwListView_DrawItem (object sender, DrawListViewItemEventArgs e)
        {
            if (e.Item.Selected)
            {
                e.Graphics.FillRectangle (Brushes.DarkGray, e.Bounds);
                e.Graphics.DrawString (e.Item.Text, this.Font, Brushes.White, e.Bounds.X, e.Bounds.Y);
            }
            else
            {
                e.Graphics.FillRectangle (Brushes.White, e.Bounds);
                e.Graphics.DrawString (e.Item.Text, this.Font, Brushes.Black, e.Bounds.X, e.Bounds.Y);
            }
        }

        /// <summary>
        /// Draws subitem text and applies content-based formatting
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void JcwListView_DrawSubItem (object sender, DrawListViewSubItemEventArgs e)
        {
        }

        /// <summary>
        /// Add handler for the MouseMove event to compensate for an extra DrawItem event that occurs the first time the mouse moves over each row
        /// </summary>
        private void JcwListView_MouseMove (object sender, MouseEventArgs e)
        {
            // Forces each row to repaint itself the 1st time the mouse moves over it, compensating for extra DrawItem event sent by wrapped Win32 control
            ListViewItem item = this.GetItemAt (e.X, e.Y);
            if (item != null && item.Tag == null)
            {
                this.Invalidate (item.Bounds);
                item.Tag = "tagged";
            }
        }

        /// <summary>
        /// Add a handler for the MouseUp event so an item can be selected by clicking anywhere along its width
        /// </summary>
        private void JcwListView_MouseUp (object sender, MouseEventArgs e)
        {
            // Selects and focuses an item when it is clicked anywhere along its width. The click must normally be on the parent item text.
            ListViewItem clickedItem = this.GetItemAt (5, e.Y);
            if (clickedItem != null)
            {
                clickedItem.Selected = true;
                clickedItem.Focused = true;
            }
        }

        #endregion
    }
}
