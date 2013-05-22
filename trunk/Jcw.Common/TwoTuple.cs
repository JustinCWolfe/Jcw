using System;
using System.Collections.Generic;
using System.Text;

using Jcw.Common.Interfaces;

namespace Jcw.Common
{
    public class TwoTuple<S, T> : ITuple
    {
        #region Fields

        private readonly S m_value1;
        private readonly T m_value2;
        private readonly int m_hashCode;

        #endregion

        #region ITuple Implementation

        public int Count
        {
            get { return 2; }
        }

        public object this[int index]
        {
            get
            {
                if (index == 0)
                    return Value1;
                else
                    return Value2;
            }
        }

        #endregion

        #region Properties

        public S Value1
        {
            get { return m_value1; }
        }

        public T Value2
        {
            get { return m_value2; }
        }

        #endregion

        #region Constructors

        public TwoTuple (S value1, T value2)
        {
            m_value1 = value1;
            m_value2 = value2;

            if (m_value1 != null)
            {
                m_hashCode ^= m_value1.GetHashCode ();
            }
            if (m_value2 != null)
            {
                m_hashCode ^= m_value2.GetHashCode ();
            }
        }

        #endregion

        #region Overrides

        public override bool Equals (object obj)
        {
            TwoTuple<S, T> castedObj = obj as TwoTuple<S, T>;
            if (castedObj != null)
            {
                if (m_value1 == null && castedObj.m_value1 != null)
                {
                    return false;
                }
                else if (m_value1 != null && !m_value1.Equals (castedObj.m_value1))
                {
                    return false;
                }

                if (m_value2 == null && castedObj.m_value2 != null)
                {
                    return false;
                }
                else if (m_value2 != null && !m_value2.Equals (castedObj.m_value2))
                {
                    return false;
                }

                return true;
            }

            return base.Equals (obj);
        }

        public override int GetHashCode ()
        {
            return m_hashCode;
        }

        public override string ToString ()
        {
            return string.Format ("{0}, {1}", m_value1, m_value2);
        }

        #endregion
    }
}
