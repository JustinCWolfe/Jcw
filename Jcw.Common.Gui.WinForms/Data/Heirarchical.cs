using System;
using System.Collections.Generic;
using System.Text;

using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList;

using Jcw.Common.Interfaces;

namespace Jcw.Common.Gui.WinForms.Data
{
    public class Heirarchical : IHeirarchical, TreeList.IVirtualTreeListData
    {
        public Heirarchical (IHeirarchical parent, Dictionary<string, object> cellData)
        {
            m_cellData = cellData;
            m_parent = parent;

            if (m_parent != null)
                this.Parent.Children.Add (this);
        }

        #region TreeList.IVirtualTreeListData Implementation

        void TreeList.IVirtualTreeListData.VirtualTreeGetChildNodes (VirtualTreeGetChildNodesInfo info)
        {
            info.Children = Children;
        }

        void TreeList.IVirtualTreeListData.VirtualTreeGetCellValue (VirtualTreeGetCellValueInfo info)
        {
            info.CellData = CellData[info.Column.FieldName];
        }

        void TreeList.IVirtualTreeListData.VirtualTreeSetCellValue (VirtualTreeSetCellValueInfo info)
        {
            CellData[info.Column.FieldName] = info.NewCellData;
        }

        #endregion

        #region IHeirarchical Implementation

        private Dictionary<string, object> m_cellData;
        public Dictionary<string, object> CellData
        {
            get { return m_cellData; }
        }

        private List<IHeirarchical> m_children = new List<IHeirarchical> ();
        public List<IHeirarchical> Children
        {
            get { return m_children; }
        }

        private IHeirarchical m_parent;
        public IHeirarchical Parent
        {
            get { return m_parent; }
        }

        #endregion

        #region Static Methods

        public static IHeirarchical FindElement (IHeirarchical root, string fieldName, object fieldValue)
        {
            if (root.CellData.ContainsKey (fieldName))
            {
                object value = root.CellData[fieldName];
                if (value != null && value.Equals (fieldValue))
                    return root;

                foreach (IHeirarchical child in root.Children)
                {
                    IHeirarchical element = FindElement (child, fieldName, fieldValue);
                    if (element != null)
                        return element;
                }
            }

            return null;
        }

        #endregion
    }
}