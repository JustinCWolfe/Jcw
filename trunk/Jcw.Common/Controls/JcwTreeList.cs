using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;

using Jcw.Common.Controls;
using Jcw.Common.Interfaces;

namespace Jcw.Common.Controls
{
    public partial class JcwTreeList : JcwUserControl, IJcwTreeList
    {
        #region Static Fields

        private static string EnabledFieldName = "TreeNodeEnabled";
        private static string ExpandedFieldName = "TreeNodeExpanded";

        #endregion

        #region Events

        public event EventHandler OnInitializeTreeColumns;
        public event EventHandler OnInitializeTreeDataSource;

        public event EventHandler<NodeEventArgs> AfterCheckNode;
        public event EventHandler<NodeEventArgs> AfterFocusNode;

        public event EventHandler<GetSelectImageEventArgs> GetSelectImage;
        public event EventHandler<GetStateImageEventArgs> GetStateImage;

        public event EventHandler<CustomDrawNodeCellEventArgs> OnCustomDrawDataRowNodeCell;
        public event EventHandler<CustomDrawNodeCellEventArgs> OnCustomDrawGroupRowNodeCell;
        public event EventHandler<CustomDrawNodeCellEventArgs> OnCustomDrawRootRowNodeCell;

        #endregion

        #region Properties

        public bool Editable { get; set; }
        public bool Multiselect { get; set; }
        public ImageList ColumnImages { get; set; }
        public ImageList NodeImages { get; set; }
        public ImageList NodeStateImages { get; set; }
        public bool ShowColumnHeaders { get; set; }
        public bool ShowIndicator { get; set; }
        public bool ShowPlusMinus { get; set; }

        public TreeListNode FocusedNode
        {
            get { return this.treeList.FocusedNode; }
            set { this.treeList.FocusedNode = value; }
        }

        #endregion

        #region Constructors

        public JcwTreeList ()
        {
            InitializeComponent ();
        }

        #endregion

        #region Overrides

        protected override void JcwUserControl_Load ( object sender, EventArgs e )
        {
            base.JcwUserControl_Load ( sender, e );

            this.treeList.ColumnsImageList = ColumnImages;
            this.treeList.OptionsBehavior.Editable = Editable;
            this.treeList.OptionsSelection.MultiSelect = Multiselect;
            this.treeList.OptionsView.ShowButtons = ShowPlusMinus;
            this.treeList.OptionsView.ShowCheckBoxes = Multiselect;
            this.treeList.OptionsView.ShowColumns = ShowColumnHeaders;
            this.treeList.OptionsView.ShowIndicator = ShowIndicator;
            this.treeList.SelectImageList = NodeImages;
            this.treeList.StateImageList = NodeStateImages;

            this.treeList.Columns.Clear ();

            if ( OnInitializeTreeColumns != null )
                OnInitializeTreeColumns ( this, EventArgs.Empty );

            if ( OnInitializeTreeDataSource != null )
                OnInitializeTreeDataSource ( this, EventArgs.Empty );
        }

        #endregion

        #region Event Handlers

        private void treeList_BeforeExpand ( object sender, BeforeExpandEventArgs beea )
        {
        }

        private void treeList_BeforeCheckNode ( object sender, CheckNodeEventArgs cnea )
        {
            // if the tree list node is not enabled don't allow it to be checked
            if ( Convert.ToBoolean ( cnea.Node.GetValue ( EnabledFieldName ) ) == false )
                cnea.CanCheck = false;
        }

        private void treeList_AfterCheckNode ( object sender, NodeEventArgs nea )
        {
            if ( m_suspendedAfterCheckNode == false )
            {
                if ( AfterCheckNode != null )
                    AfterCheckNode ( this, nea );

                // check or uncheck all child nodes
                for ( int i = 0 ; i < nea.Node.Nodes.Count ; i++ )
                    ToggleTreeListNode ( nea.Node.Nodes[i], nea.Node.Checked );

                // check all parent nodes
                TreeListNode parentNode = nea.Node.ParentNode;
                if ( parentNode != null )
                {
                    SuspendAfterCheckNode ();
                    ToggleParentNodes ( parentNode, nea.Node.Checked );
                    ResumeAfterCheckNode ();
                }
            }
        }

        private bool m_suspendedAfterCheckNode = false;
        private void SuspendAfterCheckNode ()
        {
            m_suspendedAfterCheckNode = true;
        }

        private void ResumeAfterCheckNode ()
        {
            m_suspendedAfterCheckNode = false;
        }

        private void treeList_BeforeFocusNode ( object sender, BeforeFocusNodeEventArgs bfnea )
        {
            // if the tree list node is not enabled don't allow it to be focused
            if ( Convert.ToBoolean ( bfnea.Node.GetValue ( EnabledFieldName ) ) == false )
                bfnea.CanFocus = false;
        }

        private void treeList_AfterFocusNode ( object sender, NodeEventArgs nea )
        {
            if ( AfterFocusNode != null )
                AfterFocusNode ( sender, nea );
        }

        private void treeList_CustomDrawNodeCell ( object sender, CustomDrawNodeCellEventArgs cdncea )
        {
            // if the tree list node is not enabled set the foreground color (should affect text color and checkbox color)
            if ( Convert.ToBoolean ( cdncea.Node.GetValue ( EnabledFieldName ) ) == false )
                cdncea.Appearance.ForeColor = Color.Gray;

            // the focused row should have a light blue backgound
            if ( cdncea.Focused )
                cdncea.Appearance.BackColor = Color.LightBlue;
            // the selected row (not focused so this row will be the one that was focused when the treelist
            // control was focused) should have a light gray background
            else if ( cdncea.Node.Selected )
                cdncea.Appearance.BackColor = Color.LightGray;

            if ( cdncea.Node.Level == 0 )
            {
                if ( OnCustomDrawRootRowNodeCell != null )
                    OnCustomDrawRootRowNodeCell ( this, cdncea );
            }

            if ( cdncea.Node.HasChildren )
            {
                if ( OnCustomDrawGroupRowNodeCell != null )
                    OnCustomDrawGroupRowNodeCell ( this, cdncea );
            }
            else
            {
                if ( OnCustomDrawDataRowNodeCell != null )
                    OnCustomDrawDataRowNodeCell ( this, cdncea );
            }
        }

        private void treeList_GetSelectImage ( object sender, GetSelectImageEventArgs gsiea )
        {
            if ( GetSelectImage != null )
                GetSelectImage ( sender, gsiea );
        }

        private void treeList_GetStateImage ( object sender, GetStateImageEventArgs gsiea )
        {
            if ( GetStateImage != null )
                GetStateImage ( sender, gsiea );
        }

        private void treeList_AfterCollapse ( object sender, NodeEventArgs nea )
        {
            nea.Node.SetValue ( ExpandedFieldName, false );
        }

        private void treeList_AfterExpand ( object sender, NodeEventArgs nea )
        {
            nea.Node.SetValue ( ExpandedFieldName, true );
        }

        #endregion

        #region Public Methods

        public int GetNodeIndex ( string fieldName, object fieldValue )
        {
            TreeListNode tln = this.treeList.FindNodeByFieldValue ( fieldName, fieldValue );
            if ( tln != null )
                return this.treeList.GetNodeIndex ( tln );

            return -1;
        }

        public void AddColumns ( TreeListColumn[] columns )
        {
            TreeListColumn[] builtInColumns = new TreeListColumn[2];

            // add the tree node enabled column
            TreeListColumn tlc1 = new TreeListColumn ();
            tlc1.Caption = EnabledFieldName;
            tlc1.UnboundType = DevExpress.XtraTreeList.Data.UnboundColumnType.Boolean;
            tlc1.VisibleIndex = -1;
            builtInColumns[0] = tlc1;

            // add the tree node expanded column
            TreeListColumn tlc2 = new TreeListColumn ();
            tlc2.Caption = ExpandedFieldName;
            tlc2.UnboundType = DevExpress.XtraTreeList.Data.UnboundColumnType.Boolean;
            tlc2.VisibleIndex = -2;
            builtInColumns[1] = tlc2;

            this.treeList.BeginInit ();
            this.treeList.Columns.AddRange ( builtInColumns );
            this.treeList.Columns.AddRange ( columns );
            this.treeList.EndInit ();
        }

        public void SetDataSource ( IHeirarchical root )
        {
            AddBuiltInColumnDefaultValues ( root );

            this.treeList.BeginInit ();
            this.treeList.Nodes.Clear ();
            this.treeList.DataSource = root;
            this.treeList.EndInit ();
        }

        public void ExpandAll ()
        {
            this.treeList.ExpandAll ();
        }

        public void CollapseAll ()
        {
            this.treeList.CollapseAll ();
        }

        public List<TreeListNode> GetCheckedNodes ()
        {
            List<TreeListNode> checkedNodes = new List<TreeListNode> ();
            foreach ( TreeListNode tln in this.treeList.Nodes )
            {
                if ( tln.Checked == true )
                    checkedNodes.Add ( tln );
                checkedNodes.AddRange ( GetCheckedNodesRecursive ( tln ) );
            }
            return checkedNodes;
        }

        public List<TreeListNode> GetNodes ()
        {
            List<TreeListNode> nodes = new List<TreeListNode> ();
            foreach ( TreeListNode tln in this.treeList.Nodes )
            {
                nodes.Add ( tln );
                nodes.AddRange ( GetNodesRecursive ( tln ) );
            }
            return nodes;
        }

        public void Refresh ( IHeirarchical node )
        {
            AddBuiltInColumnDefaultValues ( node );
            this.treeList.RefreshDataSource ();
        }

        public void SetNodeEnabled ( string fieldName, object fieldValue, bool isEnabled )
        {
            TreeListNode tln = this.treeList.FindNodeByFieldValue ( fieldName, fieldValue );
            if ( tln != null )
                EnableTreeListNode ( tln, isEnabled );
        }

        public void SetNodeFocused ( string fieldName, object fieldValue )
        {
            TreeListNode tln = this.treeList.FindNodeByFieldValue ( fieldName, fieldValue );
            if ( tln != null )
                this.treeList.FocusedNode = tln;
        }

        public void ToggleNode ( string fieldName, object fieldValue, bool isChecked )
        {
            TreeListNode tln = this.treeList.FindNodeByFieldValue ( fieldName, fieldValue );
            if ( tln != null )
                ToggleTreeListNode ( tln, isChecked );
        }

        #endregion

        #region Private Methods

        private void AddBuiltInColumnDefaultValues ( IHeirarchical node )
        {
            if ( node != null )
            {
                if ( node.Children != null && node.Children.Count > 0 )
                {
                    foreach ( IHeirarchical child in node.Children )
                        AddBuiltInColumnDefaultValues ( child );
                }

                if ( node.CellData != null )
                {
                    // add the tree node default column values to each record in the data source
                    node.CellData[EnabledFieldName] = true;
                    node.CellData[ExpandedFieldName] = false;
                }
            }
        }

        private List<TreeListNode> GetCheckedNodesRecursive ( TreeListNode node )
        {
            List<TreeListNode> checkedNodes = new List<TreeListNode> ();

            foreach ( TreeListNode tln in node.Nodes )
            {
                if ( tln.Checked == true )
                    checkedNodes.Add ( tln );

                if ( tln.HasChildren )
                    checkedNodes.AddRange ( GetCheckedNodesRecursive ( tln ) );
            }

            return checkedNodes;
        }

        private List<TreeListNode> GetNodesRecursive ( TreeListNode node )
        {
            List<TreeListNode> nodes = new List<TreeListNode> ();

            foreach ( TreeListNode tln in node.Nodes )
            {
                nodes.Add ( tln );

                if ( tln.HasChildren )
                    nodes.AddRange ( GetNodesRecursive ( tln ) );
            }

            return nodes;
        }

        /// <summary>
        /// backtrack through hierarchy of parent nodes toggling the state of any that do not match the passed-in state
        /// </summary>
        /// <param name="node"></param>
        private void ToggleParentNodes ( TreeListNode node, bool isChecked )
        {
            // a child node under this node (which is a parent node) was checked so now we need to see if all the other
            // child nodes under this node are checked. if they are we need to check this node
            if ( isChecked == true )
            {
                // check all children of this node to see if all other children are checked - in which case we should check the parent
                bool allChildrenChecked = true;

                foreach ( TreeListNode childNode in node.Nodes )
                    if ( childNode.Checked == false )
                        allChildrenChecked = false;

                if ( allChildrenChecked )
                    ToggleTreeListNode ( node, isChecked );
            }
            // if any child node under this node was unchecked, uncheck this node (parent)
            else
                ToggleTreeListNode ( node, isChecked );

            // keep going up the tree to all parents to toggle them accordingly
            TreeListNode parentNode = node.ParentNode;
            if ( parentNode != null )
                ToggleParentNodes ( parentNode, isChecked );
        }

        private void ToggleTreeListNode ( TreeListNode currentNode, bool isChecked )
        {
            if ( currentNode.Checked != isChecked )
            {
                currentNode.Checked = isChecked;
                if ( AfterCheckNode != null )
                    AfterCheckNode ( this, new NodeEventArgs ( currentNode ) );

                if ( m_suspendedAfterCheckNode == false )
                {
                    // check or uncheck all child nodes
                    for ( int i = 0 ; i < currentNode.Nodes.Count ; i++ )
                        ToggleTreeListNode ( currentNode.Nodes[i], currentNode.Checked );
                }
            }
        }

        private void EnableTreeListNode ( TreeListNode currentNode, bool isEnabled )
        {
            if ( currentNode.HasChildren )
            {
                foreach ( TreeListNode childNode in currentNode.Nodes )
                    EnableTreeListNode ( childNode, isEnabled );
            }

            if ( Convert.ToBoolean ( currentNode.GetValue ( EnabledFieldName ) ) != isEnabled )
                currentNode.SetValue ( EnabledFieldName, isEnabled );
        }

        #endregion
    }

    public interface IJcwTreeList
    {
        event EventHandler OnInitializeTreeColumns;
        event EventHandler OnInitializeTreeDataSource;

        event EventHandler<NodeEventArgs> AfterCheckNode;
        event EventHandler<NodeEventArgs> AfterFocusNode;

        event EventHandler<CustomDrawNodeCellEventArgs> OnCustomDrawDataRowNodeCell;
        event EventHandler<CustomDrawNodeCellEventArgs> OnCustomDrawGroupRowNodeCell;
        event EventHandler<CustomDrawNodeCellEventArgs> OnCustomDrawRootRowNodeCell;

        bool Editable { get; set; }
        bool Multiselect { get; set; }
        ImageList ColumnImages { get; set; }
        ImageList NodeImages { get; set; }
        ImageList NodeStateImages { get; set; }
        bool ShowColumnHeaders { get; set; }
        bool ShowIndicator { get; set; }
        bool ShowPlusMinus { get; set; }
        TreeListNode FocusedNode { get; set; }

        void AddColumns ( TreeListColumn[] columns );
        void ExpandAll ();
        void CollapseAll ();
        List<TreeListNode> GetCheckedNodes ();
        List<TreeListNode> GetNodes ();
        void Refresh ( IHeirarchical node );
        void SetDataSource ( IHeirarchical root );
        void SetNodeEnabled ( string fieldName, object fieldValue, bool isEnabled );
        void SetNodeFocused ( string fieldName, object fieldValue );
        void ToggleNode ( string fieldName, object fieldValue, bool isChecked );
    }
}
