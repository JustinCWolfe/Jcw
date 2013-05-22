using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace Jcw.Common
{
    public static class Utilities
    {
        #region Private Static Fields

        private const string DefaultTimeSeparator = ":";

        private static DateTime m_lastDT;
        private static string m_dateSep = "/";
        private static string m_dateTimeSep = " -- ";
        private static string m_timeSep = DefaultTimeSeparator;

        #endregion

        #region Properties

        /// <summary>
        /// Static member to store the System.DateTime.Now value
        /// </summary>
        public static DateTime LastDateTime
        {
            get { return m_lastDT; }
            set { m_lastDT = value; }
        }

        /// <summary>
        /// Static string member used when joining time in date time string
        /// </summary>
        public static string TimeSeparator
        {
            get { return m_timeSep; }
            set { m_timeSep = value; }
        }

        /// <summary>
        /// Static string member used when joining month, day and year in date
        /// </summary>
        public static string DateSeparator
        {
            get { return m_dateSep; }
            set { m_dateSep = value; }
        }

        /// <summary>
        /// Static string member used for joining date and time
        /// </summary>
        public static string DateTimeSeparator
        {
            get { return m_dateTimeSep; }
            set { m_dateTimeSep = value; }
        }

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Convert the passed in value in inches to meters
        /// </summary>
        /// <param name="inches">number of inches</param>
        /// <returns>number of meters</returns>
        public static double GetMetersFromInches(double inches)
        {
            // there are .0254 meters in 1 inch
            return inches * .0254;
        }

        /// <summary>
        /// Convert the passed in value in meters to inches 
        /// </summary>
        /// <param name="meters">number of meters</param>
        /// <returns>number of inches</returns>
        public static double GetInchesFromMeters(double meters)
        {
            // there are 39.37007874 inches in 1 meter 
            return meters * 39.37007874;
        }

        /// <summary>
        /// Convert the passed in value in meters to feet
        /// </summary>
        /// <param name="meters">number of meters</param>
        /// <returns>number of feet</returns>
        public static double GetFeetFromMeters(double meters)
        {
            // there are 3.280839895 feet in 1 meter
            return meters * 3.280839895;
        }

        /// <summary>
        /// Convert the passed in value in feet to meters
        /// </summary>
        /// <param name="feet">number of feet</param>
        /// <returns>number of meters</returns>
        public static double GetMetersFromFeet(double feet)
        {
            // there are .3048 meters in 1 foot
            return feet * .3048;
        }

        /// <summary>
        /// Convert the passed in value in inches to feet
        /// </summary>
        /// <param name="meters">number of inches</param>
        /// <returns>number of feet</returns>
        public static double GetFeetFromInches(double inches)
        {
            // there are .083333333333 feet in 1 inch 
            return inches * .083333333333;
        }

        /// <summary>
        /// Convert the passed in value in N-m to ft-lbs
        /// </summary>
        /// <param name="newtonMeters">number of N-m</param>
        /// <returns>number of ft-lbs</returns>
        public static double GetFeetLbsFromNewtonMeters(double newtonMeters)
        {
            // there are .73756214728 lb-ft in 1 N-m
            return newtonMeters * .73756214728;
        }

        /// <summary>
        /// Convert the passed in value in ft/s to mph
        /// </summary>
        /// <param name="feetPerSecond">number of ft/s</param>
        /// <returns>number of mph</returns>
        public static double GetMPHFromFeetPerSecond(double feetPerSecond)
        {
            // there are .68181818182 mph in 1 ft/s
            return feetPerSecond * .68181818182;
        }

        /// <summary>
        /// Convert the passed in value in mph to ft/s
        /// </summary>
        /// <param name="mpg">number of miles per hour</param>
        /// <returns>number of ft/s</returns>
        public static double GetFeetPerSecondFromMPH(double mph)
        {
            // there are 1.4666666667 ft/s in 1 mph 
            return mph * 1.4666666667;
        }

        /// <summary>
        /// Convert the passed in value in ft-lbs force/s to horsepower 
        /// </summary>
        /// <param name="feetPerSecond">number of ft-lbs force/s</param>
        /// <returns>number of horsepower</returns>
        public static double GetHPFromFeetLbsPerSecond(double feetLbsPerSecond)
        {
            // there are .0018181817572 hp in 1 ft-lbs/s
            return feetLbsPerSecond * .0018181817572;
        }

        /// <summary>
        /// Convert the passed in value in pounds to kilograms
        /// </summary>
        /// <param name="pounds">number of pounds</param>
        /// <returns>number of kilograms</returns>
        public static double GetKilogramsFromPoundsWeight(double pounds)
        {
            // there are .45359237 kg in 1 lb
            return pounds * .45359237;
        }

        /// <summary>
        /// Convert the passed in value in kilograms to pounds weight
        /// </summary>
        /// <param name="kilograms">number of kilograms</param>
        /// <returns>number of pounds weight</returns>
        public static double GetPoundsWeightFromKilograms(double kilograms)
        {
            // there are 2.2046226218 lbs in 1 kg
            return kilograms * 2.2046226218;
        }

        /// <summary>
        /// Convert the passed in value in joules to pounds calories
        /// </summary>
        /// <param name="joules">number of joules</param>
        /// <returns>number of calories</returns>
        public static double GetCaloriesFromJoules(double joules)
        {
            // there are 0.2388458966 calories in 1 joule
            return joules * 0.2388458966;
        }

        /// <summary>
        /// Get the current time of day, join it with the current date using the date time separator
        /// and return it
        /// </summary>
        /// <returns>string current date and time</returns>
        public static string GetCurrentDateTime()
        {
            LastDateTime = System.DateTime.Now;
            TimeSpan t = LastDateTime.TimeOfDay;

            // remove the milliseconds from the current time
            string time = t.ToString ().Split (new string[] { "." }, StringSplitOptions.None)[0];

            // if time separator is something other than the default value, change the time to use this separator
            if (TimeSeparator != DefaultTimeSeparator)
            {
                time = String.Join (TimeSeparator, time.Split (new string[] { DefaultTimeSeparator }, StringSplitOptions.None));
            }

            // add the month, day and year onto the time and return it
            return LastDateTime.Month + DateSeparator + LastDateTime.Day + DateSeparator + LastDateTime.Year + DateTimeSeparator + time;
        }

        public static void WriteConfigurationFile(Dictionary<string, string> appSettings)
        {
            // Get the current configuration file
            Configuration config = ConfigurationManager.OpenExeConfiguration (ConfigurationUserLevel.None);

            // clear out the settings before we write the current data
            config.AppSettings.Settings.Clear ();

            foreach (string key in appSettings.Keys)
            {
                config.AppSettings.Settings.Add (key, appSettings[key]);
            }

            // save the configuration file
            config.Save (ConfigurationSaveMode.Modified);

            // reload the config file section
            ConfigurationManager.RefreshSection ("appSettings");
        }

        /// <summary>
        /// Low byte is always 0 - 255 because it is the less significant of the bytes.  It
        /// is less significant because it only adds 0 - 255 to the total value while the 
        /// high byte adds Y * 256 to the total value
        /// Convert 2^8 and 2^7 integers to a single 16 bit integer
        /// </summary>
        /// <param name="low">8 bit integer</param>
        /// <param name="high">7 bit integer</param>
        /// <returns>16 bit integer with a max value of 2^15</returns>
        public static ushort Convert15To16Bit(byte low, byte high)
        {
            UInt16 data = Convert.ToUInt16 (high * 256 + low);
            UInt16 limit = Convert.ToUInt16 (Math.Pow (2, 15));
            return (data > limit) ? limit : data;
        }

        /// <summary>
        /// Low byte is always 0 - 255 because it is the less significant of the bytes.  It
        /// is less significant because it only adds 0 - 255 to the total value while the 
        /// high byte adds Y * 256 to the total value
        /// Convert 2^8 and 2^8 integers to a single 16 bit integer
        /// </summary>
        /// <param name="low">8 bit integer</param>
        /// <param name="high">8 bit integer</param>
        /// <returns>16 bit integer with a max value of 2^16</returns>
        public static uint JoinLowAndHighByte(byte low, byte high)
        {
            uint data = Convert.ToUInt32 (high * 256 + low);
            uint limit = Convert.ToUInt32 (Math.Pow (2, 16));
            return (data > limit) ? limit : data;
        }

        /// <summary>
        /// Low byte is always 0 - 255 because it is the less significant of the bytes.  It
        /// is less significant because it only adds 0 - 255 to the total value while the 
        /// high byte adds Y * 256 to the total value
        /// Convert 2^8 and 2^4 integers to a single 16 bit integer
        /// </summary>
        /// <param name="low">8 bit integer</param>
        /// <param name="high">4 bit integer</param>
        /// <returns>16 bit integer with max value of 2^12</returns>
        public static ushort Convert12To16Bit(byte low, byte high)
        {
            UInt16 data = Convert.ToUInt16 (high * 256 + low);
            UInt16 limit = Convert.ToUInt16 (Math.Pow (2, 12));
            return (data > limit) ? limit : data;
        }

        /// <summary>
        /// Low byte is always 0 - 255 because it is the less significant of the bytes.  It
        /// is less significant because it only adds 0 - 255 to the total value while the 
        /// high byte adds Y * 256 to the total value
        /// Convert a single 16 bit integer to a low and high byte
        /// </summary>
        /// <param name="combined">integer value to split into low and high bytes</param>
        /// <param name="low">low byte</param>
        /// <param name="high">high byte</param>
        public static void SplitIntoLowAndHighBytes(int combined, out byte low, out byte high)
        {
            high = Convert.ToByte (combined / (int)Math.Pow (2, 8));
            low = Convert.ToByte (combined - high * (int)Math.Pow (2, 8));
        }

        /// <summary>
        /// Method to create a single data table that is a union of the rows from 2 passed-in data tables
        /// </summary>
        /// <param name="first">first data table</param>
        /// <param name="second">second data table</param>
        /// <returns>union data table</returns>
        public static DataTable DataTableRowUnion(DataTable first, DataTable second)
        {
            DataTable unionTable = new DataTable ("Union");

            // build new columns
            DataColumn[] newColumns = new DataColumn[first.Columns.Count];
            for (int i = 0 ; i < first.Columns.Count ; i++)
            {
                newColumns[i] = new DataColumn (first.Columns[i].ColumnName, first.Columns[i].DataType);
            }

            // add new columns to result table
            unionTable.Columns.AddRange (newColumns);

            unionTable.BeginLoadData ();

            // load data from first table
            foreach (DataRow row in first.Rows)
            {
                unionTable.LoadDataRow (row.ItemArray, true);
            }

            // load data from second table
            foreach (DataRow row in second.Rows)
            {
                unionTable.LoadDataRow (row.ItemArray, true);
            }

            unionTable.EndLoadData ();

            return unionTable;
        }

        /// <summary>
        /// Method to create a single data table that is a union of 2 passed-in data tables columns and rows
        /// </summary>
        /// <param name="first">first data table</param>
        /// <param name="second">second data table</param>
        /// <returns>union data table</returns>
        public static DataTable DataTableUnion(DataTable first, DataTable second)
        {
            DataTable unionTable = new DataTable ("Union");

            // build new columns
            DataColumn[] newColumns = new DataColumn[first.Columns.Count + second.Columns.Count];
            for (int i = 0 ; i < first.Columns.Count ; i++)
            {
                newColumns[i] = new DataColumn (first.Columns[i].ColumnName, first.Columns[i].DataType);
            }
            for (int i = 0 ; i < second.Columns.Count ; i++)
            {
                newColumns[first.Columns.Count + i] = new DataColumn (second.Columns[i].ColumnName, second.Columns[i].DataType);
            }

            // add new columns to result table
            unionTable.Columns.AddRange (newColumns);

            unionTable.BeginLoadData ();

            // load data from first and second tables into union table
            for (int i = 0 ; i < first.Rows.Count ; i++)
            {
                List<object> rowData = new List<object> ();
                rowData.AddRange (first.Rows[i].ItemArray);
                rowData.AddRange (second.Rows[i].ItemArray);
                unionTable.LoadDataRow (rowData.ToArray (), true);
            }

            unionTable.EndLoadData ();

            return unionTable;
        }

        public static byte[] GetByteArrayFromString(string input)
        {
            return ASCIIEncoding.ASCII.GetBytes (input);
        }

        /// <summary>
        /// Calculate the voltage for a 12 bit value
        /// </summary>
        /// <param name="value">12 bit integer value</param>
        /// <returns>calculated voltage</returns>
        public static double CalculateVoltage(double value)
        {
            // the voltage is the 2 byte value divided by 4096 * 3.3V
            // 3.3V is the operating voltage of the device
            return (value / Math.Pow (2, 12)) * 3.3;
        }

        /// <summary>
        /// The NumberOfDigits static method calculates the number of digit characters in the passed in string
        /// </summary>
        /// <param name="theString"></param>
        /// <returns>number of digits</returns>
        public static int NumberOfDigits(string theString)
        {
            int count = 0;
            for (int i = 0 ; i < theString.Length ; i++)
            {
                if (Char.IsDigit (theString[i]))
                    count++;
            }

            return count;
        }

        /// <summary>
        /// The NumberOfDigits static method calculates the number of decimal digit characters in the passed in integer
        /// </summary>
        /// <param name="numberToCount"></param>
        /// <returns>number of digits</returns>
        public static int NumberOfDigits(int numberToCount)
        {
            return NumberOfDigits (numberToCount.ToString ());
        }

        public static double GetAverage(List<int> vector)
        {
            if (vector.Count > 0)
            {
                int sum = 0;
                for (int vectorIndex = 0 ; vectorIndex < vector.Count ; vectorIndex++)
                {
                    sum += vector[vectorIndex];
                }
                return (double)sum / vector.Count;
            }

            return 0;
        }

        public static double GetAverage(List<double> vector)
        {
            if (vector.Count > 0)
            {
                double sum = 0;
                for (int vectorIndex = 0 ; vectorIndex < vector.Count ; vectorIndex++)
                {
                    sum += vector[vectorIndex];
                }
                return sum / vector.Count;
            }

            return 0;
        }

        public static int GetMinimum(List<int> vector)
        {
            int minimum = int.MaxValue;
            for (int vectorIndex = 0 ; vectorIndex < vector.Count ; vectorIndex++)
            {
                int element = vector[vectorIndex];
                if (element < minimum)
                {
                    minimum = element;
                }
            }
            return minimum;
        }

        public static double GetMinimum(List<double> vector)
        {
            double minimum = double.MaxValue;
            for (int vectorIndex = 0 ; vectorIndex < vector.Count ; vectorIndex++)
            {
                double element = vector[vectorIndex];
                if (element < minimum)
                {
                    minimum = element;
                }
            }
            return minimum;
        }

        public static int GetMaximum(List<int> vector)
        {
            int maximum = int.MinValue;
            for (int vectorIndex = 0 ; vectorIndex < vector.Count ; vectorIndex++)
            {
                int element = vector[vectorIndex];
                if (element > maximum)
                {
                    maximum = element;
                }
            }
            return maximum;
        }

        public static double GetMaximum(List<double> vector)
        {
            double maximum = double.MinValue;
            for (int vectorIndex = 0 ; vectorIndex < vector.Count ; vectorIndex++)
            {
                double element = vector[vectorIndex];
                if (element > maximum)
                {
                    maximum = element;
                }
            }
            return maximum;
        }

        public static double GetStandardDeviation(List<double> vector)
        {
            double average = GetAverage (vector);
            double standardDeviation = 0;
            for (int vectorIndex = 0 ; vectorIndex < vector.Count ; vectorIndex++)
            {
                double element = vector[vectorIndex];
                standardDeviation += Math.Pow ((element - average), 2);
            }
            standardDeviation *= 1 / (double)vector.Count;
            return Math.Sqrt (standardDeviation);
        }

        public static Bitmap ResizeImage(int width, int height, Bitmap image)
        {
            Bitmap newImage = new Bitmap (width, height);
            using (Graphics gr = Graphics.FromImage (newImage))
            {
                gr.SmoothingMode = SmoothingMode.AntiAlias;
                gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
                gr.DrawImage (image, new Rectangle (0, 0, width, height));
            }
            return newImage;
        }

        #endregion
    }
}
