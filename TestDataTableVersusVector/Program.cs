using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Data;
using System.Linq;
using System.Text;

namespace DataTableVersusVectorTestApp
{
    class Program
    {
        private const int NumberOfRecords = 100000;
        private const int NumberOfTestIterations = 10;

        private Stopwatch sw = new Stopwatch ();

        static void Main(string[] args)
        {
            var p = new Program ();

            var startMemory = System.GC.GetTotalMemory (true) / 1024;
            Console.WriteLine ("Start memory (Kbytes): {0}", startMemory);

            p.TestVectorSet ();
            p.TestDataTable ();

            var endMemory = System.GC.GetTotalMemory (true) / 1024;
            Console.WriteLine ("End memory (Kbytes): {0}", endMemory);
            Console.WriteLine ("Memory difference (Kbytes): {0}", (endMemory - startMemory));
        }

        private void TestVectorSet()
        {
            var vs = InitializeVectorSet (NumberOfRecords);

            var iterationTimes = new List<double> ();
            for (int iterationNumber = 1 ; iterationNumber <= NumberOfTestIterations ; iterationNumber++)
            {
                sw.Reset ();
                sw.Start ();
                VectorSetOperations (vs);
                sw.Stop ();
                iterationTimes.Add (sw.ElapsedMilliseconds);
                Console.WriteLine ("VectorSet Iteration {0} duration (milliseconds): {1}", iterationNumber, sw.ElapsedMilliseconds);
            }

            var average = iterationTimes.Average ();
            var standardDeviation = Math.Sqrt (1 / (double)iterationTimes.Count * iterationTimes.Sum (value => Math.Pow (value - average, 2)));
            Console.WriteLine ("VectorSet Min {0}, Max {1}, Ave {2}, StdDev {3}", iterationTimes.Min (), iterationTimes.Max (), average, standardDeviation);

            var endMemory = System.GC.GetTotalMemory (true) / 1024;
            Console.WriteLine ("VectorSet End memory (Kbytes): {0}", endMemory);
        }

        private void TestDataTable()
        {
            var dt = InitializeDataTable (NumberOfRecords);

            var iterationTimes = new List<double> ();
            for (int iterationNumber = 1 ; iterationNumber <= NumberOfTestIterations ; iterationNumber++)
            {
                sw.Reset ();
                sw.Start ();
                DataTableOperations (dt);
                sw.Stop ();
                iterationTimes.Add (sw.ElapsedMilliseconds);
                Console.WriteLine ("DataTable Iteration {0} duration (milliseconds): {1}", iterationNumber, sw.ElapsedMilliseconds);
            }

            var average = iterationTimes.Average ();
            var standardDeviation = Math.Sqrt (1 / (double)iterationTimes.Count * iterationTimes.Sum (value => Math.Pow (value - average, 2)));
            Console.WriteLine ("DataTable Min {0}, Max {1}, Ave {2}, StdDev {3}", iterationTimes.Min (), iterationTimes.Max (), average, standardDeviation);

            var endMemory = System.GC.GetTotalMemory (true) / 1024;
            Console.WriteLine ("DataTable End memory (Kbytes): {0}", endMemory);
        }

        private DataTable InitializeDataTable(int numberOfRecords)
        {
            var dt = new DataTable ();

            dt.Columns.AddRange (new DataColumn[] {
                 new DataColumn ("Index",typeof (int)),
                 new DataColumn ("Col1",typeof (double)),
                 new DataColumn ("Col2",typeof (double)),
                 new DataColumn ("Col3",typeof (double)),
                 new DataColumn ("Col4",typeof (double)),
                 new DataColumn ("Col5",typeof (double)),
                 new DataColumn ("Col6",typeof (double)),
                 new DataColumn ("Col7",typeof (double)),
                 new DataColumn ("Col8",typeof (double)),
                 new DataColumn ("Col9",typeof (double)),
                 new DataColumn ("Col10",typeof (double)),
                 new DataColumn ("Col11",typeof (double)),
                 new DataColumn ("Col12",typeof (double)),
                 new DataColumn ("Col13",typeof (double)),
                 new DataColumn ("Col14",typeof (double)),
                 new DataColumn ("Col15",typeof (double)),
                 new DataColumn ("Col16",typeof (double)),
                 new DataColumn ("Col17",typeof (double)),
                 new DataColumn ("Col18",typeof (double)),
                 new DataColumn ("Col19",typeof (double)),
            });

            for (var rowIndex = 0 ; rowIndex < numberOfRecords ; rowIndex++)
            {
                var dRowIndex = 1 / (double)(rowIndex + 1);
                dt.Rows.Add (rowIndex, dRowIndex, dRowIndex, dRowIndex, dRowIndex, dRowIndex, dRowIndex, dRowIndex, dRowIndex, dRowIndex,
                        dRowIndex, dRowIndex, dRowIndex, dRowIndex, dRowIndex, dRowIndex, dRowIndex, dRowIndex, dRowIndex, dRowIndex);
            }

            return dt;
        }

        private VectorSet InitializeVectorSet(int numberOfRecords)
        {
            var vs = new VectorSet ();

            for (var rowIndex = 0 ; rowIndex < numberOfRecords ; rowIndex++)
            {
                var dRowIndex = 1 / (double)(rowIndex + 1);
                vs.Add (rowIndex, dRowIndex, dRowIndex, dRowIndex, dRowIndex, dRowIndex, dRowIndex, dRowIndex, dRowIndex, dRowIndex,
                        dRowIndex, dRowIndex, dRowIndex, dRowIndex, dRowIndex, dRowIndex, dRowIndex, dRowIndex, dRowIndex, dRowIndex);
            }

            return vs;
        }

        private void DataTableOperations(DataTable dt)
        {
            // multiply each cell value by 2
            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn col in dt.Columns)
                {
                    if (!"Index".Equals (col.ColumnName))
                        row[col] = Convert.ToDouble (row[col]) * 2;
                }
            }
        }

        private void VectorSetOperations(VectorSet vs)
        {
            // multiply each cell value by 2
            for (int index = 0 ; index < vs.Index.Data.Count ; index++)
            {
                vs.Col1.Data[index] = Convert.ToDouble (vs.Col1.Data[index]) * 2;
                vs.Col2.Data[index] = Convert.ToDouble (vs.Col2.Data[index]) * 2;
                vs.Col3.Data[index] = Convert.ToDouble (vs.Col3.Data[index]) * 2;
                vs.Col4.Data[index] = Convert.ToDouble (vs.Col4.Data[index]) * 2;
                vs.Col5.Data[index] = Convert.ToDouble (vs.Col5.Data[index]) * 2;
                vs.Col6.Data[index] = Convert.ToDouble (vs.Col6.Data[index]) * 2;
                vs.Col7.Data[index] = Convert.ToDouble (vs.Col7.Data[index]) * 2;
                vs.Col8.Data[index] = Convert.ToDouble (vs.Col8.Data[index]) * 2;
                vs.Col9.Data[index] = Convert.ToDouble (vs.Col9.Data[index]) * 2;
                vs.Col10.Data[index] = Convert.ToDouble (vs.Col10.Data[index]) * 2;
                vs.Col11.Data[index] = Convert.ToDouble (vs.Col11.Data[index]) * 2;
                vs.Col12.Data[index] = Convert.ToDouble (vs.Col12.Data[index]) * 2;
                vs.Col13.Data[index] = Convert.ToDouble (vs.Col13.Data[index]) * 2;
                vs.Col14.Data[index] = Convert.ToDouble (vs.Col14.Data[index]) * 2;
                vs.Col15.Data[index] = Convert.ToDouble (vs.Col15.Data[index]) * 2;
                vs.Col16.Data[index] = Convert.ToDouble (vs.Col16.Data[index]) * 2;
                vs.Col17.Data[index] = Convert.ToDouble (vs.Col17.Data[index]) * 2;
                vs.Col18.Data[index] = Convert.ToDouble (vs.Col18.Data[index]) * 2;
                vs.Col19.Data[index] = Convert.ToDouble (vs.Col19.Data[index]) * 2;
            }
        }
    }

    class Vector<T>
    {
        public string Name { get; set; }
        public List<T> Data { get; private set; }

        public Vector()
        {
            Data = new List<T> ();
        }
    }

    class VectorSet
    {
        public Vector<long> Index { get; private set; }
        public Vector<double> Col1 { get; private set; }
        public Vector<double> Col2 { get; private set; }
        public Vector<double> Col3 { get; private set; }
        public Vector<double> Col4 { get; private set; }
        public Vector<double> Col5 { get; private set; }
        public Vector<double> Col6 { get; private set; }
        public Vector<double> Col7 { get; private set; }
        public Vector<double> Col8 { get; private set; }
        public Vector<double> Col9 { get; private set; }
        public Vector<double> Col10 { get; private set; }
        public Vector<double> Col11 { get; private set; }
        public Vector<double> Col12 { get; private set; }
        public Vector<double> Col13 { get; private set; }
        public Vector<double> Col14 { get; private set; }
        public Vector<double> Col15 { get; private set; }
        public Vector<double> Col16 { get; private set; }
        public Vector<double> Col17 { get; private set; }
        public Vector<double> Col18 { get; private set; }
        public Vector<double> Col19 { get; private set; }

        public VectorSet()
        {
            Index = new Vector<long> () { Name = "Index" };
            Col1 = new Vector<double> () { Name = "Col1" };
            Col2 = new Vector<double> () { Name = "Col2" };
            Col3 = new Vector<double> () { Name = "Col3" };
            Col4 = new Vector<double> () { Name = "Col4" };
            Col5 = new Vector<double> () { Name = "Col5" };
            Col6 = new Vector<double> () { Name = "Col6" };
            Col7 = new Vector<double> () { Name = "Col7" };
            Col8 = new Vector<double> () { Name = "Col8" };
            Col9 = new Vector<double> () { Name = "Col9" };
            Col10 = new Vector<double> () { Name = "Col10" };
            Col11 = new Vector<double> () { Name = "Col11" };
            Col12 = new Vector<double> () { Name = "Col12" };
            Col13 = new Vector<double> () { Name = "Col13" };
            Col14 = new Vector<double> () { Name = "Col14" };
            Col15 = new Vector<double> () { Name = "Col15" };
            Col16 = new Vector<double> () { Name = "Col16" };
            Col17 = new Vector<double> () { Name = "Col17" };
            Col18 = new Vector<double> () { Name = "Col18" };
            Col19 = new Vector<double> () { Name = "Col19" };
        }

        public void Add(params object[] values)
        {
            Index.Data.Add (Convert.ToInt64 (values[0]));
            Col1.Data.Add (Convert.ToDouble (values[1]));
            Col2.Data.Add (Convert.ToDouble (values[2]));
            Col3.Data.Add (Convert.ToDouble (values[3]));
            Col4.Data.Add (Convert.ToDouble (values[4]));
            Col5.Data.Add (Convert.ToDouble (values[5]));
            Col6.Data.Add (Convert.ToDouble (values[6]));
            Col7.Data.Add (Convert.ToDouble (values[7]));
            Col8.Data.Add (Convert.ToDouble (values[8]));
            Col9.Data.Add (Convert.ToDouble (values[9]));
            Col10.Data.Add (Convert.ToDouble (values[10]));
            Col11.Data.Add (Convert.ToDouble (values[11]));
            Col12.Data.Add (Convert.ToDouble (values[12]));
            Col13.Data.Add (Convert.ToDouble (values[13]));
            Col14.Data.Add (Convert.ToDouble (values[14]));
            Col15.Data.Add (Convert.ToDouble (values[15]));
            Col16.Data.Add (Convert.ToDouble (values[16]));
            Col17.Data.Add (Convert.ToDouble (values[17]));
            Col18.Data.Add (Convert.ToDouble (values[18]));
            Col19.Data.Add (Convert.ToDouble (values[19]));
        }
    }
}
