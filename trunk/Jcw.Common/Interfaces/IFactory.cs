using System;
using System.Collections.Generic;
using System.Text;

namespace Jcw.Common.Interfaces
{
    public interface IFactory
    {
        T Create<T> ();
        T Create<T> (params object[] arguments);
        T Create<T> (Dictionary<string, KeyValuePair<Type, object>> propertyDescriptors, params object[] arguments);

        object Create(Type type, Dictionary<string, KeyValuePair<Type, object>> propertyDescriptors, params object[] arguments);
    }
}
