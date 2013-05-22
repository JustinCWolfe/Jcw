using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

using Jcw.Common;

namespace Jcw.Common.Test
{
    class Program
    {
        static void RunTest (int numberOfVectors, int lengthOfVectors)
        {
            // Assemble the jagged array of vectors to filter.
            double[][] vectorsToFilter = new double[numberOfVectors][];
            for (int vectorNumber = 0 ; vectorNumber < vectorsToFilter.Length ; vectorNumber++)
            {
                vectorsToFilter[vectorNumber] = new double[lengthOfVectors];

                double[] vector = vectorsToFilter[vectorNumber];
                for (int vectorIndex = 0 ; vectorIndex < vector.Length ; vectorIndex++)
                {
                    vector[vectorIndex] = vectorIndex;
                }
            }

            // Run a single threaded test.
            Stopwatch sw = new Stopwatch ();
            sw.Start ();
            double[][] filteredVectors = ButterworthFilter.FourthOrder (250, vectorsToFilter);
            sw.Stop ();
            Debug.WriteLine (string.Format ("Filter (vectors={0}, length={1}) Time: {2}", numberOfVectors, lengthOfVectors, sw.ElapsedMilliseconds));

            System.GC.Collect ();
            Thread.Sleep (5000);

            // Run a parallel test.
            sw.Reset ();
            sw.Start ();
            double[][] filteredVectors2 = ButterworthFilter.ParallelFourthOrder (250, vectorsToFilter);
            sw.Stop ();
            Debug.WriteLine (string.Format ("Parallel Filter (vectors={0}, length={1}) Time: {2}", numberOfVectors, lengthOfVectors, sw.ElapsedMilliseconds));

            // Now validate that each result is the same between the 2 different filtering method calls.
            for (int vectorNumber = 0 ; vectorNumber < filteredVectors.Length ; vectorNumber++)
            {
                double[] vector = filteredVectors[vectorNumber];
                double[] vector2 = filteredVectors2[vectorNumber];
                for (int vectorIndex = 0 ; vectorIndex < vector.Length ; vectorIndex++)
                {
                    Debug.Assert (vector[vectorIndex] == vector2[vectorIndex], "Mismatch");
                }
            }

            System.GC.Collect ();
            Thread.Sleep (5000);

            // Run a parallel test.
            sw.Reset ();
            sw.Start ();
            double[][] filteredVectors3 = ButterworthFilter.ParallelFourthOrder (250, vectorsToFilter);
            sw.Stop ();
            Debug.WriteLine (string.Format ("Parallel Filter (vectors={0}, length={1}) Time: {2}", numberOfVectors, lengthOfVectors, sw.ElapsedMilliseconds));

            System.GC.Collect ();
            Thread.Sleep (5000);

            // Run a single threaded test.
            sw.Reset ();
            sw.Start ();
            double[][] filteredVectors4 = ButterworthFilter.FourthOrder (250, vectorsToFilter);
            sw.Stop ();
            Debug.WriteLine (string.Format ("Filter (vectors={0}, length={1}) Time: {2}", numberOfVectors, lengthOfVectors, sw.ElapsedMilliseconds));

            // Now validate that each result is the same between the 2 different filtering method calls.
            for (int vectorNumber = 0 ; vectorNumber < filteredVectors3.Length ; vectorNumber++)
            {
                double[] vector = filteredVectors3[vectorNumber];
                double[] vector2 = filteredVectors4[vectorNumber];
                for (int vectorIndex = 0 ; vectorIndex < vector.Length ; vectorIndex++)
                {
                    Debug.Assert (vector[vectorIndex] == vector2[vectorIndex], "Mismatch");
                }
            }
        }

        static void Main (string[] args)
        {
            RunTest (10, 5000);

            System.GC.Collect ();
            Thread.Sleep (5000);

            RunTest (10, 50000);

            System.GC.Collect ();
            Thread.Sleep (5000);

            RunTest (10, 500000);

            System.GC.Collect ();
            Thread.Sleep (5000);

            RunTest (50, 500000);
        }
    }
}
