using System;
using System.Collections.Generic;
using System.Text;

using Jcw.Common;
using Jcw.Common.Interfaces;

namespace Jcw.Charting.Interfaces
{
    public interface IJcwChartVectorGroup<T>
        where T : struct
    {
        string AxisTitle { get; }
        string Caption { get; }

        /// <summary>
        /// List of vectors in this chart vector group.
        /// </summary>
        List<IVector<T>> Vectors { get; }

        void SetVectors (params IVector<T>[] vectors);
        void AddVectors (params IVector<T>[] vectors);

        void ConvertVectorGroup<U> (out IJcwChartVectorGroup<U> convertedVectorGroup)
            where U : struct;
    }

    public class JcwChartVectorGroup<T> : IJcwChartVectorGroup<T>
        where T : struct
    {
        public string AxisTitle { get; private set; }
        public string Caption { get; private set; }
        public List<IVector<T>> Vectors { get; private set; }

        public JcwChartVectorGroup (string axisTitle, string caption)
        {
            AxisTitle = axisTitle;
            Caption = caption;
            Vectors = new List<IVector<T>> ();
        }

        public void SetVectors (params IVector<T>[] vectors)
        {
            Vectors.Clear ();
            Vectors.AddRange (vectors);
        }

        public void AddVectors (params IVector<T>[] vectors)
        {
            Vectors.AddRange (vectors);
        }

        public void ConvertVectorGroup<U> (out IJcwChartVectorGroup<U> convertedVectorGroup)
            where U : struct
        {
            convertedVectorGroup = new JcwChartVectorGroup<U> (AxisTitle, Caption);

            List<IVector<U>> convertedVectors = new List<IVector<U>> ();
            foreach (IVector<T> vector in Vectors)
            {
                IVector<U> convertedVector = new Vector<U> (vector.Name);

                for (int vectorIndex = 0 ; vectorIndex < vector.Data.Count ; vectorIndex++)
                {
                    U convertedValue = (U)Convert.ChangeType (vector.Data[vectorIndex], typeof (U));
                    convertedVector.Data.Add (convertedValue);
                }

                convertedVectors.Add (convertedVector);
            }

            if (convertedVectors.Count > 0)
            {
                convertedVectorGroup.SetVectors (convertedVectors.ToArray ());
            }
        }

        public static void ConvertVectorGroup<U, V> (IJcwChartVectorGroup<U> vectorGroupToConvert, out IJcwChartVectorGroup<V> convertedVectorGroup)
            where U : struct
            where V : struct
        {
            vectorGroupToConvert.ConvertVectorGroup<V> (out convertedVectorGroup);
        }
    }
}
