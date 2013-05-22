using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

using Jcw.Common;

namespace Jcw.Common.Gui.Interfaces
{
    public interface INavigationController<T>
    {
        EventHandler ApplicationStartCallback { set; }
        Bitmap Artwork { set; }
        T RootForm { set; }

        void Push(Type formType);
        void Push(Type formType, EventHandler<JcwEventArgs<T>> popCallback, Dictionary<string, KeyValuePair<Type, object>> propertyDescriptors);

        void Pop();
        void PopToRoot();
    }
}
