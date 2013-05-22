using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

using Jcw.Common.Interfaces;

namespace Jcw.Common
{
    public abstract class FactoryBase : IFactory
    {
        #region IFactory Implementation

        public abstract T Create<T>();
        public abstract T Create<T>(params object[] arguments);
        public abstract T Create<T>(Dictionary<string, KeyValuePair<Type, object>> propertyDescriptors, params object[] arguments);

        public abstract object Create(Type type, Dictionary<string, KeyValuePair<Type, object>> propertyDescriptors, params object[] arguments);

        #endregion
    }

    public class Factory : FactoryBase
    {
        static IFactory instance = null;
        static readonly object padlock = new object ();

        public static IFactory Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Factory ();
                    }
                    return instance;
                }
            }
        }

        #region Constructors

        private Factory() { }

        #endregion

        #region Overrides

        public override T Create<T>()
        {
            return Create<T> (null, null);
        }

        public override T Create<T>(params object[] arguments)
        {
            return Create<T> (null, arguments);
        }

        public override T Create<T>(Dictionary<string, KeyValuePair<Type, object>> propertyDescriptors, params object[] arguments)
        {
            Type instanceType = typeof (T);
            return (T)Create (instanceType, propertyDescriptors, arguments);
        }

        public override object Create(Type instanceType, Dictionary<string, KeyValuePair<Type, object>> propertyDescriptors, params object[] arguments)
        {
            object instance = Activator.CreateInstance (instanceType, arguments);

            if (propertyDescriptors != null)
            {
                foreach (string propertyName in propertyDescriptors.Keys)
                {
                    PropertyInfo pi = instanceType.GetProperty (propertyName);
                    pi.SetValue (instance, propertyDescriptors[propertyName].Value, null);
                }
            }

            return instance;
        }

        #endregion
    }
}
