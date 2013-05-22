using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

using Jcw.Common.Data;

using BFC = Jcw.Common.Data.ButterworthFilterCoefficients;

namespace Jcw.Common
{
    public static class ButterworthFilter
    {
        #region Static Fields

        private static readonly ButterworthFilterCoefficients FilterCoefficientsDataSet = new ButterworthFilterCoefficients ();

        #endregion

        #region Static Constructor

        static ButterworthFilter ()
        {
            FilterCoefficientsDataSet.BeginInit ();

            PopulateFrequencyTable ();
            PopulateFilterOrderTable ();
            PopulateFilterCoefficientsTable ();

            FilterCoefficientsDataSet.EndInit ();
            FilterCoefficientsDataSet.AcceptChanges ();
        }

        #endregion

        #region Private Static Methods

        /// <summary>
        /// Loop throw all rows in the passed in table and zero out all cell values for columns we are trying to filter
        /// </summary>
        /// <typeparam name="T">type of the value to intialize cells to</typeparam>
        /// <param name="table">table to initialize</param>
        /// <param name="columnOrdinals">list of columns ordinals to initialize in table</param>
        /// <param name="value">value to set as initial</param>
        /// <returns>initialized data table</returns>
        private static DataTable InitializeFieldsToValue<T> (DataTable table, List<int> columnOrdinals, T value)
        {
            try
            {
                table.BeginInit ();

                foreach (DataRow row in table.Rows)
                {
                    foreach (int ordinal in columnOrdinals)
                    {
                        row[ordinal] = value;
                    }
                }

                table.EndInit ();
                table.AcceptChanges ();

                return table;
            }
            catch (Exception e)
            {
                Error.LastError = e.ToString ();
                return null;
            }
        }

        #endregion

        #region Private Static Methods

        private static void PopulateFilterOrderTable ()
        {
            FilterCoefficientsDataSet.Tables["FilterOrder"].BeginLoadData ();
            FilterCoefficientsDataSet.FilterOrder.Rows.Add (new object[] { 4 });
            FilterCoefficientsDataSet.Tables["FilterOrder"].EndLoadData ();
        }

        private static void PopulateFrequencyTable ()
        {
            FilterCoefficientsDataSet.Tables["SampleFrequency"].BeginLoadData ();

            // new sample frequencies for post computation filtering
            FilterCoefficientsDataSet.SampleFrequency.Rows.Add (new object[] { 1 });
            FilterCoefficientsDataSet.SampleFrequency.Rows.Add (new object[] { 2 });
            FilterCoefficientsDataSet.SampleFrequency.Rows.Add (new object[] { 3 });
            FilterCoefficientsDataSet.SampleFrequency.Rows.Add (new object[] { 4 });
            FilterCoefficientsDataSet.SampleFrequency.Rows.Add (new object[] { 5 });

            FilterCoefficientsDataSet.SampleFrequency.Rows.Add (new object[] { 25 });
            FilterCoefficientsDataSet.SampleFrequency.Rows.Add (new object[] { 50 });
            FilterCoefficientsDataSet.SampleFrequency.Rows.Add (new object[] { 75 });
            FilterCoefficientsDataSet.SampleFrequency.Rows.Add (new object[] { 100 });
            FilterCoefficientsDataSet.SampleFrequency.Rows.Add (new object[] { 125 });
            FilterCoefficientsDataSet.SampleFrequency.Rows.Add (new object[] { 150 });
            FilterCoefficientsDataSet.SampleFrequency.Rows.Add (new object[] { 175 });
            FilterCoefficientsDataSet.SampleFrequency.Rows.Add (new object[] { 200 });
            FilterCoefficientsDataSet.SampleFrequency.Rows.Add (new object[] { 225 });
            FilterCoefficientsDataSet.SampleFrequency.Rows.Add (new object[] { 250 });

            FilterCoefficientsDataSet.Tables["SampleFrequency"].EndLoadData ();
        }

        private static void PopulateFilterCoefficientsTable ()
        {
            FilterCoefficientsDataSet.Tables["FilterCoefficients"].BeginLoadData ();

            // rows for the 1Hz samle frequency - numerator, denominator, frequency, order 
            // each numerator coefficient below must be multipled by 1.0e-4 
            // 0.00024136223131   0.00096544892525   0.00144817338787   0.00096544892525   0.00024136223131
            // 1.00000000000000  -3.93432582079874   5.80512542105514  -3.80723245722885   0.93643324315202
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.00024136223131e-4, 1.00000000000000, 1, 4 });
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.00096544892525e-4, -3.93432582079874, 1, 4 });
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.00144817338787e-4, 5.80512542105514, 1, 4 });
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.00096544892525e-4, -3.80723245722885, 1, 4 });
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.00024136223131e-4, 0.93643324315202, 1, 4 });

            // rows for the 2Hz samle frequency - numerator, denominator, frequency, order
            // each numerator coefficient below must be multipled by 1.0e-4 
            // 0.00373937862841   0.01495751451364   0.02243627177045   0.01495751451364   0.00373937862841
            // 1.00000000000000  -3.86865666790855   5.61452684963494  -3.62276075956140   0.87689656084082
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.00373937862841e-4, 1.00000000000000, 2, 4 });
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.01495751451364e-4, -3.86865666790855, 2, 4 });
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.02243627177045e-4, 5.61452684963494, 2, 4 });
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.01495751451364e-4, -3.62276075956140, 2, 4 });
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.00373937862841e-4, 0.87689656084082, 2, 4 });

            // rows for the 3Hz samle frequency - numerator, denominator, frequency, order
            // each numerator coefficient below must be multipled by 1.0e-4 
            // 0.01833801797357   0.07335207189429   0.11002810784144   0.07335207189429   0.01833801797357
            // 1.00000000000000  -3.80299754165438   5.42816596368389  -3.44626421809320   0.82112513689245
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.01833801797357e-4, 1.00000000000000, 3, 4 });
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.07335207189429e-4, -3.80299754165438, 3, 4 });
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.11002810784144e-4, 5.42816596368389, 3, 4 });
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.07335207189429e-4, -3.44626421809320, 3, 4 });
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.01833801797357e-4, 0.82112513689245, 3, 4 });

            // rows for the 4Hz samle frequency - numerator, denominator, frequency, order
            // each numerator coefficient below must be multipled by 1.0e-4 
            // 0.05616562286370   0.22466249145481   0.33699373718221   0.22466249145481   0.05616562286370
            // 1.00000000000000  -3.73735339098582   5.24600330601763  -3.27743279390188   0.76887274386665
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.05616562286370e-4, 1.00000000000000, 4, 4 });
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.22466249145481e-4, -3.73735339098582, 4, 4 });
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.33699373718221e-4, 5.24600330601763, 4, 4 });
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.22466249145481e-4, -3.27743279390188, 4, 4 });
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.05616562286370e-4, 0.76887274386665, 4, 4 });

            // rows for the 5Hz samle frequency - numerator, denominator, frequency, order
            // each numerator coefficient below must be multipled by 1.0e-4 
            // 0.13293728898744   0.53174915594978   0.79762373392467   0.53174915594978   0.13293728898744
            // 1.00000000000000  -3.67172908916194   5.06799838673419  -3.11596692520174   0.71991032729187
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.13293728898744e-4, 1.00000000000000, 5, 4 });
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.53174915594978e-4, -3.67172908916194, 5, 4 });
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.79762373392467e-4, 5.06799838673419, 5, 4 });
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.53174915594978e-4, -3.11596692520174, 5, 4 });
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.13293728898744e-4, 0.71991032729187, 5, 4 });

            // rows for the 25Hz samle frequency - numerator, denominator, frequency, order
            // 0.04658290663644   0.18633162654577   0.27949743981866   0.18633162654577    0.04658290663644
            // 1.00000000000000  -0.78209519802334   0.67997852691630  -0.18267569775303   0.03011887504317
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.04658290663644, 1.00000000000000, 25, 4 });
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.18633162654577, -0.78209519802334, 25, 4 });
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.27949743981866, 0.67997852691630, 25, 4 });
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.18633162654577, -0.18267569775303, 25, 4 });
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.04658290663644, 0.03011887504317, 25, 4 });

            // rows for the 50Hz samle frequency - numerator, denominator, frequency, order
            // 0.00482434335772   0.01929737343086   0.02894606014630   0.01929737343086    0.00482434335772
            // 1.00000000000000  -2.36951300718204   2.31398841441588  -1.05466540587856   0.18737949236818
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.00482434335772, 1.00000000000000, 50, 4 });
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.01929737343086, -2.36951300718204, 50, 4 });
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.02894606014630, 2.31398841441588, 50, 4 });
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.01929737343086, -1.05466540587856, 50, 4 });
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.00482434335772, 0.18737949236818, 50, 4 });

            // rows for the 75Hz samle frequency - numerator, denominator, frequency, order
            // 0.00117527954957   0.00470111819828   0.00705167729742   0.00470111819828    0.00117527954957
            // 1.00000000000000  -2.90904949325273   3.28399602666864  -1.68764549946868   0.33150343884590
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.00117527954957, 1.00000000000000, 75, 4 });
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.00470111819828, -2.90904949325273, 75, 4 });
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.00705167729742, 3.28399602666864, 75, 4 });
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.00470111819828, -1.68764549946868, 75, 4 });
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.00117527954957, 0.33150343884590, 75, 4 });

            // rows for the 100Hz samle frequency - numerator, denominator, frequency, order
            // 0.00041659920441   0.00166639681763   0.00249959522644   0.00166639681763    0.00041659920441
            // 1.00000000000000  -3.18063854887472   3.86119434899422  -2.11215535511097   0.43826514226198
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.00041659920441, 1.00000000000000, 100, 4 });
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.00166639681763, -3.18063854887472, 100, 4 });
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.00249959522644, 3.86119434899422, 100, 4 });
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.00166639681763, -2.11215535511097, 100, 4 });
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.00041659920441, 0.43826514226198, 100, 4 });

            // rows for the 125Hz samle frequency - numerator, denominator, frequency, order
            // 0.00018321602337   0.00073286409348   0.00109929614022   0.00073286409348    0.00018321602337
            // 1.00000000000000  -3.34406783771187   4.23886395088407  -2.40934285658632   0.51747819978804
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.00018321602337, 1.00000000000000, 125, 4 });
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.00073286409348, -3.34406783771187, 125, 4 });
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.00109929614022, 4.23886395088407, 125, 4 });
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.00073286409348, -2.40934285658632, 125, 4 });
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.00018321602337, 0.51747819978804, 125, 4 });

            // rows for the 150Hz samle frequency - numerator, denominator, frequency, order
            // 0.00009276462029   0.00037105848117   0.00055658772175   0.00037105848117    0.00009276462029
            // 1.00000000000000  -3.45318513758661   4.50413909163395  -2.62730361822823   0.57783389810556
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.00009276462029, 1.00000000000000, 150, 4 });
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.00037105848117, -3.45318513758661, 150, 4 });
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.00055658772175, 4.50413909163395, 150, 4 });
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.00037105848117, -2.62730361822823, 150, 4 });
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.00009276462029, 0.57783389810556, 150, 4 });

            // rows for the 175Hz samle frequency - numerator, denominator, frequency, order
            // 0.00005187706087   0.00020750824347   0.00031126236520   0.00020750824347    0.00005187706087
            // 1.00000000000000  -3.53119446617849   4.70036614668454  -2.79343915428201   0.62509750674983
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.00005187706087, 1.00000000000000, 175, 4 });
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.00020750824347, -3.53119446617849, 175, 4 });
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.00031126236520, 4.70036614668454, 175, 4 });
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.00020750824347, -2.79343915428201, 175, 4 });
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.00005187706087, 0.62509750674983, 175, 4 });

            // rows for the 200Hz samle frequency - numerator, denominator, frequency, order
            // 0.00003123897692   0.00012495590767   0.00018743386150   0.00012495590767    0.00003123897692
            // 1.00000000000000  -3.58973388711218   4.85127588251942  -2.92405265616246   0.66301048438589
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.00003123897692, 1.00000000000000, 200, 4 });
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.00012495590767, -3.58973388711218, 200, 4 });
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.00018743386150, 4.85127588251942, 200, 4 });
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.00012495590767, -2.92405265616246, 200, 4 });
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.00003123897692, 0.66301048438589, 200, 4 });

            // rows for the 225Hz samle frequency - numerator, denominator, frequency, order
            // 0.00001991914817   0.00007967659269   0.00011951488904   0.00007967659269    0.00001991914817
            // 1.00000000000000  -3.63528148250896   4.97088810856765  -3.02933941727876   0.69405149759084
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.00001991914817, 1.00000000000000, 225, 4 });
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.00007967659269, -3.63528148250896, 225, 4 });
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.00011951488904, 4.97088810856765, 225, 4 });
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.00007967659269, -3.02933941727876, 225, 4 });
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.00001991914817, 0.69405149759084, 225, 4 });

            // rows for the 250Hz samle frequency - numerator, denominator, frequency, order
            // each numerator coefficient below must be multipled by 1.0e-4 
            // 0.13293728898744   0.53174915594978   0.79762373392467   0.53174915594978   0.13293728898744
            // 1.00000000000000  -3.67172908916194   5.06799838673419  -3.11596692520174   0.71991032729187
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.13293728898744e-4, 1.00000000000000, 250, 4 });
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.53174915594978e-4, -3.67172908916194, 250, 4 });
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.79762373392467e-4, 5.06799838673419, 250, 4 });
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.53174915594978e-4, -3.11596692520174, 250, 4 });
            FilterCoefficientsDataSet.FilterCoefficients.Rows.Add (new object[] { 0.13293728898744e-4, 0.71991032729187, 250, 4 });

            FilterCoefficientsDataSet.FilterCoefficients.EndLoadData ();
        }

        private static void GetCoefficients (ref List<double> numerators, ref List<double> denominators, double sampleFrequency)
        {
            // get the list of numerators and denominators for this sample frequency
            string coefficientSelect = "FREQUENCY_FK = " + sampleFrequency.ToString ();

            foreach (DataRow row in FilterCoefficientsDataSet.Tables["FilterCoefficients"].Select (coefficientSelect))
            {
                numerators.Add (Convert.ToDouble (row["NUMERATOR"]));
                denominators.Add (Convert.ToDouble (row["DENOMINATOR"]));
            }
        }

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Filter a vector using a fourth order butterworth filter based on the data sample frequency
        /// </summary>
        /// <param name="vector">vector of data to filter</param>
        /// <param name="sampleFrequency">sample frequency for the vector data</param>
        /// <returns>vector of filtered data</returns>
        public static double[] FourthOrder (double sampleFrequency, double[] vector)
        {
            // array to store filtered vector data
            double[] filteredVector = new double[vector.Length];

            List<double> numerators = new List<double> ();
            List<double> denominators = new List<double> ();
            GetCoefficients (ref numerators, ref denominators, sampleFrequency);

            // 4th order low pass butterworth filter with a freq ratio of 5/125
            for (int vectorIndex = numerators.Count ; vectorIndex < vector.Length ; vectorIndex++)
            {
                filteredVector[vectorIndex] =
                    1 / denominators[0] * (

                    numerators[0] * vector[vectorIndex] +
                    numerators[1] * vector[vectorIndex - 1] +
                    numerators[2] * vector[vectorIndex - 2] +
                    numerators[3] * vector[vectorIndex - 3] +
                    numerators[4] * vector[vectorIndex - 4] -

                    denominators[1] * filteredVector[vectorIndex - 1] -
                    denominators[2] * filteredVector[vectorIndex - 2] -
                    denominators[3] * filteredVector[vectorIndex - 3] -
                    denominators[4] * filteredVector[vectorIndex - 4]);
            }

            return filteredVector;
        }

        /// <summary>
        /// Filter a list of vectors using a fourth order butterworth filter based on the data sample frequency
        /// </summary>
        /// <param name="vector">list of vectors to filter</param>
        /// <param name="sampleFrequency">sample frequency for the vector list data</param>
        /// <returns>list of filtered vectors</returns>
        public static double[][] FourthOrder (double sampleFrequency, double[][] vectors)
        {
            // Jagged array to store filtered vector data.
            double[][] filteredVectors = new double[vectors.Length][];

            // Initialize the arrays stored in the jagged array.
            for (int vectorNumber = 0 ; vectorNumber < vectors.Length ; vectorNumber++)
            {
                double[] vector = vectors[vectorNumber];
                filteredVectors[vectorNumber] = new double[vector.Length];
            }

            List<double> numerators = new List<double> ();
            List<double> denominators = new List<double> ();
            GetCoefficients (ref numerators, ref denominators, sampleFrequency);

            // loop through all vectors in vector list
            for (int vectorNumber = 0 ; vectorNumber < vectors.Length ; vectorNumber++)
            {
                double[] vector = vectors[vectorNumber];
                double[] filteredVector = filteredVectors[vectorNumber];

                // 4th order low pass butterworth filter with a freq ratio of 5/125
                for (int vectorIndex = numerators.Count ; vectorIndex < vector.Length ; vectorIndex++)
                {
                    filteredVector[vectorIndex] =
                        1 / denominators[0] * (

                        numerators[0] * vector[vectorIndex] +
                        numerators[1] * vector[vectorIndex - 1] +
                        numerators[2] * vector[vectorIndex - 2] +
                        numerators[3] * vector[vectorIndex - 3] +
                        numerators[4] * vector[vectorIndex - 4] -

                        denominators[1] * filteredVector[vectorIndex - 1] -
                        denominators[2] * filteredVector[vectorIndex - 2] -
                        denominators[3] * filteredVector[vectorIndex - 3] -
                        denominators[4] * filteredVector[vectorIndex - 4]);
                }
            }

            return filteredVectors;
        }

        public static double[][] ParallelFourthOrder (double sampleFrequency, double[][] vectors)
        {
            // Jagged array to store filtered vector data.
            double[][] filteredVectors = new double[vectors.Length][];

            // Delegates that will be called to filter each vector.
            Func<double, double[], double[]>[] filterMethods = new Func<double, double[], double[]>[vectors.Length];

            // Array to store filtering method first arguments.
            double[] methodArguments1 = new double[vectors.Length];

            for (int vectorNumber = 0 ; vectorNumber < vectors.Length ; vectorNumber++)
            {
                // Initialize the arrays stored in the jagged array.
                double[] vector = vectors[vectorNumber];
                filteredVectors[vectorNumber] = new double[vector.Length];

                methodArguments1[vectorNumber] = sampleFrequency;
                filterMethods[vectorNumber] = FourthOrder;
            }

            ParallelProcessor.ExecuteParallel<double, double[], double[]> (filterMethods, methodArguments1, vectors, filteredVectors);

            return filteredVectors;
        }

        /// <summary>
        /// Filter a list of columns in a data table using a fourth order butterworth filter based on the data sample frequency
        /// </summary>
        /// <param name="table">data table containing columns to filter</param>
        /// <param name="columns">list of data columns to filter</param>
        /// <param name="sampleFrequency">sample frequency for the data</param>
        /// <returns>filtered data table</returns>
        public static DataTable FourthOrder (DataTable table, DataColumn[] columns, double sampleFrequency)
        {
            // get the list of numerators and denominators for this sample frequency
            List<double> numerators = new List<double> ();
            List<double> denominators = new List<double> ();
            GetCoefficients (ref numerators, ref denominators, sampleFrequency);

            List<int> columnOrdinals = new List<int> ();
            foreach (DataColumn column in columns)
                columnOrdinals.Add (column.Ordinal);

            // create new table to store filtered data and call method to initialize each row,col cell to zero
            DataTable newTable = table.Copy ();
            newTable = InitializeFieldsToValue<double> (newTable, columnOrdinals, 0);

            // 4th order low pass butterworth filter with a freq ratio of 5/125
            for (int rowIndex = numerators.Count - 1 ; rowIndex < table.Rows.Count ; rowIndex++)
            {
                // loop through collection of data columns to filter
                foreach (int ordinal in columnOrdinals)
                {
                    newTable.Rows[rowIndex][ordinal] =
                        1 / denominators[0] * (
                            numerators[0] * (double)table.Rows[rowIndex][ordinal] +
                            numerators[1] * (double)table.Rows[rowIndex - 1][ordinal] +
                            numerators[2] * (double)table.Rows[rowIndex - 2][ordinal] +
                            numerators[3] * (double)table.Rows[rowIndex - 3][ordinal] +
                            numerators[4] * (double)table.Rows[rowIndex - 4][ordinal] -
                            denominators[1] * (double)newTable.Rows[rowIndex - 1][ordinal] -
                            denominators[2] * (double)newTable.Rows[rowIndex - 2][ordinal] -
                            denominators[3] * (double)newTable.Rows[rowIndex - 3][ordinal] -
                            denominators[4] * (double)newTable.Rows[rowIndex - 4][ordinal]
                        );
                }
            }

            return newTable;
        }

        #endregion
    }
}
