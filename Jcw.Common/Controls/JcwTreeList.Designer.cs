namespace Jcw.Common.Controls
{
    partial class JcwTreeList
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose ( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose ();
            }
            base.Dispose ( disposing );
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent ()
        {
            this.treeList = new DevExpress.XtraTreeList.TreeList ();
            ( (System.ComponentModel.ISupportInitialize) ( this.dxErrorProvider ) ).BeginInit ();
            ( (System.ComponentModel.ISupportInitialize) ( this.errorProvider ) ).BeginInit ();
            ( (System.ComponentModel.ISupportInitialize) ( this.treeList ) ).BeginInit ();
            this.SuspendLayout ();
            // 
            // treeList
            // 
            this.treeList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeList.Location = new System.Drawing.Point ( 0, 0 );
            this.treeList.Name = "treeList";
            this.treeList.OptionsBehavior.AllowExpandOnDblClick = false;
            this.treeList.OptionsBehavior.Editable = false;
            this.treeList.OptionsView.ShowIndicator = false;
            this.treeList.Size = new System.Drawing.Size ( 150, 150 );
            this.treeList.TabIndex = 0;
            this.treeList.AfterCollapse += new DevExpress.XtraTreeList.NodeEventHandler ( this.treeList_AfterCollapse );
            this.treeList.AfterExpand += new DevExpress.XtraTreeList.NodeEventHandler ( this.treeList_AfterExpand );
            this.treeList.BeforeExpand += new DevExpress.XtraTreeList.BeforeExpandEventHandler ( treeList_BeforeExpand );
            this.treeList.BeforeCheckNode += new DevExpress.XtraTreeList.CheckNodeEventHandler ( this.treeList_BeforeCheckNode );
            this.treeList.CustomDrawNodeCell += new DevExpress.XtraTreeList.CustomDrawNodeCellEventHandler ( this.treeList_CustomDrawNodeCell );
            this.treeList.AfterFocusNode += new DevExpress.XtraTreeList.NodeEventHandler ( this.treeList_AfterFocusNode );
            this.treeList.BeforeFocusNode += new DevExpress.XtraTreeList.BeforeFocusNodeEventHandler ( this.treeList_BeforeFocusNode );
            this.treeList.GetStateImage += new DevExpress.XtraTreeList.GetStateImageEventHandler ( this.treeList_GetStateImage );
            this.treeList.GetSelectImage += new DevExpress.XtraTreeList.GetSelectImageEventHandler ( this.treeList_GetSelectImage );
            this.treeList.AfterCheckNode += new DevExpress.XtraTreeList.NodeEventHandler ( this.treeList_AfterCheckNode );
            // 
            // JcwTreeList
            // 
            this.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Appearance.Font = new System.Drawing.Font ( "Arial", 8F );
            this.Appearance.Options.UseBackColor = true;
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF ( 6F, 13F );
            this.Controls.Add ( this.treeList );
            this.Name = "JcwTreeList";
            ( (System.ComponentModel.ISupportInitialize) ( this.dxErrorProvider ) ).EndInit ();
            ( (System.ComponentModel.ISupportInitialize) ( this.errorProvider ) ).EndInit ();
            ( (System.ComponentModel.ISupportInitialize) ( this.treeList ) ).EndInit ();
            this.ResumeLayout ( false );

        }

        #endregion

        private DevExpress.XtraTreeList.TreeList treeList;
    }
}
