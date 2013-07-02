using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace PercentileInterpolator
{
    class Program
    {
        [DebuggerDisplay ("{Age}/{Length}/{Weight}")]
        private class Measurement
        {
            public double Age { get; set; }
            public double? Length { get; set; }
            public double? Weight { get; set; }
        }

        private const double DaysToMonthsFactor = 12.0 / 365;

        private const string ErrorMessage = @"
There must be 3 elements in the input data file lines.
The first element should be the age in days.
The second element should be the height in cm.
The third element should be the weight in kg.";

        private const char Separator = ',';
        private const string LengthByAgeCSVFilename = "LengthByAge.csv";
        private const string WeightByAgeCSVFilename = "WeightByAge.csv";

        /// <summary>
        /// Key is age in months.
//        /// Value is and ordered dictionary where key is height and value is percentile.
        /// Value is and ordered dictionary where key is percentile and value is height.
        /// </summary>
        private static SortedDictionary<double, SortedDictionary<double, double>> LengthPerAge =
            new SortedDictionary<double, SortedDictionary<double, double>> ();

        /// <summary>
        /// Key is age in months.
//        /// Value is and ordered dictionary where key is weight and value is percentile.
        /// Value is and ordered dictionary where key is percentile and value is weight.
        /// </summary>
        private static SortedDictionary<double, SortedDictionary<double, double>> WeightPerAge =
            new SortedDictionary<double, SortedDictionary<double, double>> ();

        private static void LoadFileIntoDictionary(string fileName, SortedDictionary<double, SortedDictionary<double, double>> dictionary)
        {
            // The first line contains the column captions.
            // The second line contains the percentiles.
            // All other lines contain data.
            string[] lines = File.ReadAllLines (fileName);
            List<string> percentiles = new List<string> ();
            for (int lineIndex = 0 ; lineIndex < lines.Length ; lineIndex++)
            {
                string[] lineParts = lines[lineIndex].Split (Separator);
                if (lineParts.Length > 0)
                {
                    if (lineIndex == 1)
                    {
                        percentiles.AddRange (lineParts);
                    }
                    else if (lineIndex > 1)
                    {
                        SortedDictionary<double, double> ageVector = new SortedDictionary<double, double> ();
                        for (int linePartIndex = 1 ; linePartIndex < lineParts.Length ; linePartIndex++)
                        {
                            int percentileIndex = linePartIndex - 1;
                            //ageVector.Add (Convert.ToDouble (lineParts[linePartIndex]), Convert.ToDouble (percentiles[percentileIndex]));
                            ageVector.Add (Convert.ToDouble (percentiles[percentileIndex]), Convert.ToDouble (lineParts[linePartIndex]));
                        }
                        dictionary.Add (Convert.ToDouble (lineParts[0]), ageVector);
                    }
                }
            }
        }

        private static List<Measurement> LoadInputDataFileIntoDictionary(string fileName)
        {
            List<Measurement> measurements = new List<Measurement> ();
            string[] lines = File.ReadAllLines (fileName);
            for (int lineIndex = 0 ; lineIndex < lines.Length ; lineIndex++)
            {
                string[] lineParts = lines[lineIndex].Split (Separator);
                if (lineParts.Length == 3)
                {
                    // We need the age in months so convert it before storing in the measurement object.
                    double ageInDays = Convert.ToDouble (lineParts[0]);
                    Measurement measurement = new Measurement () { Age = ageInDays * DaysToMonthsFactor };
                    measurements.Add (measurement);
                    if (lineParts[1] != string.Empty)
                    {
                        measurement.Length = Convert.ToDouble (lineParts[1]);
                    }
                    if (lineParts[2] != string.Empty)
                    {
                        measurement.Weight = Convert.ToDouble (lineParts[2]);
                    }
                }
                else
                {
                    Console.WriteLine (ErrorMessage);
                }
            }
            return measurements;
        }

        private static SortedDictionary<double, double> CreateInterpolatedAgeVector(double factor,
            SortedDictionary<double, double> vector1,
            SortedDictionary<double, double> vector2)
        {
            SortedDictionary<double, double> interpolatedAgeVector = new SortedDictionary<double, double> ();
            foreach (double percentile in vector1.Keys)
            {
                double vector1Value = vector1[percentile];
                double vector2Value = vector2[percentile];
                interpolatedAgeVector.Add (percentile, (vector1Value + vector2Value) * factor);
            }
            return interpolatedAgeVector;
        }

        /// <summary>
        /// Look for an exact match age vector in the passed in dictionary for the measurement.
        /// If no exact match is found, interpolate between bounding age vectors and return interpolated vector.
        /// </summary>
        private static SortedDictionary<double, double> FindAgeVectorInDictionary(double age,
            SortedDictionary<double, SortedDictionary<double, double>> dictionary)
        {
            double previousAge = 0;
            SortedDictionary<double, double> previousAgeVector = null;
            foreach (double dictionaryAge in dictionary.Keys)
            {
                if (age == dictionaryAge)
                {
                    return dictionary[dictionaryAge];
                }
                else if (dictionaryAge < age)
                {
                    previousAge = dictionaryAge;
                    previousAgeVector = dictionary[dictionaryAge];
                }
                else
                {
                    SortedDictionary<double, double> currentAgeVector = dictionary[dictionaryAge];
                    double factor = age / (previousAge + dictionaryAge);
                    return CreateInterpolatedAgeVector (factor, previousAgeVector, currentAgeVector);
                }
            }
            /*
    loop through input dates to find the 2 map values we are between (age vectors)
    interpolate between values to create new series of percentile values matching age
             */
            return null;
        }

        static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                LoadFileIntoDictionary (LengthByAgeCSVFilename, LengthPerAge);
                LoadFileIntoDictionary (WeightByAgeCSVFilename, WeightPerAge);

                string inputDataFilename = args[0];
                List<Measurement> measurements = LoadInputDataFileIntoDictionary (inputDataFilename);

                foreach (Measurement measurement in measurements)
                {
                    if (measurement.Length.HasValue)
                    {
                        FindAgeVectorInDictionary (measurement.Length.Value, LengthPerAge);
                    }
                    if (measurement.Weight.HasValue)
                    {
                        FindAgeVectorInDictionary (measurement.Weight.Value, WeightPerAge);
                    }
                }

                /*
    loop through interpolated age vector to find the 2 map values we are between (percentiles)
    calculate percentile for input
                 */
            }
            else
            {
                Console.WriteLine ("Missing input data filename");
            }
        }
    }
}
