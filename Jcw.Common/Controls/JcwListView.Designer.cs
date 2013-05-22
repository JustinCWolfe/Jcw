namespace Jcw.Common.Controls
{
    partial class JcwListView
    {
        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent ()
        {
            this.AllowColumnReorder = false;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DoubleBuffered = true;
            this.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler (JcwListView_DrawColumnHeader);
            this.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler (JcwListView_DrawItem);
            this.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler (JcwListView_DrawSubItem);
            this.Font = JcwStyle.JcwStyleFont;
            this.FullRowSelect = true;
            this.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.HideSelection = false;
            this.Layout += new System.Windows.Forms.LayoutEventHandler (JcwListView_Layout);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler (JcwListView_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler (JcwListView_MouseUp);
            this.OwnerDraw = true;
            this.View = System.Windows.Forms.View.Details;
        }

        #endregion
    }
}
