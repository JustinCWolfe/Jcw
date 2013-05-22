using System;
using System.Collections.Generic;
using System.Text;

namespace Jcw.Common
{
    public class JcwEventArgs<T> : EventArgs
    {
        #region Fields

        private T m_value;

        #endregion

        #region Properties

        public T Value
        {
            get { return m_value; }
        }

        #endregion

        #region ctors

        public JcwEventArgs ( T value )
        {
            m_value = value;
        }

        #endregion
    }

    public class JcwEventArgs<T, U> : EventArgs
    {
        #region Fields

        private T m_value1;
        private U m_value2;

        #endregion

        #region Properties

        public T Value1
        {
            get { return m_value1; }
        }

        public U Value2
        {
            get { return m_value2; }
        }

        #endregion

        #region ctors

        public JcwEventArgs ( T value1, U value2 )
        {
            m_value1 = value1;
            m_value2 = value2;
        }

        #endregion
    }

    public class JcwEventArgs<T, U, V> : EventArgs
    {
        #region Fields

        private T m_value1;
        private U m_value2;
        private V m_value3;

        #endregion

        #region Properties

        public T Value1
        {
            get { return m_value1; }
        }

        public U Value2
        {
            get { return m_value2; }
        }

        public V Value3
        {
            get { return m_value3; }
        }

        #endregion

        #region ctors

        public JcwEventArgs ( T value1, U value2, V value3 )
        {
            m_value1 = value1;
            m_value2 = value2;
            m_value3 = value3;
        }

        #endregion
    }
}