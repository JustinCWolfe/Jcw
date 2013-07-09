using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

using Jcw.Common.Interfaces;
using Jcw.Common.Interpolation;

namespace PercentileInterpolator
{
    class Program
    {
        [DebuggerDisplay ("Age: {Age}/Length: {Length}/Weight: {Weight}")]
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

        private static IInterpolator Interpolator = InterpolationFactory.Create (InterpolationType.Linear);

        /// <summary>
        /// Key is age in months.
        /// Value is an ordered dictionary where key is percentile and value is height.
        /// </summary>
        private static SortedDictionary<double, SortedDictionary<double, double>> LengthPerAge =
            new SortedDictionary<double, SortedDictionary<double, double>> ();

        /// <summary>
        /// Key is age in months.
        /// Value is an ordered dictionary where key is percentile and value is weight.
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

        private static SortedDictionary<double, double> CreateInterpolatedAgeVector(
            double previousAge, double currentAge, double ageToCreateVectorFor,
            SortedDictionary<double, double> vector1, SortedDictionary<double, double> vector2)
        {
            SortedDictionary<double, double> interpolatedAgeVector = new SortedDictionary<double, double> ();
            foreach (double percentile in vector1.Keys)
            {
                double vector1Value = vector1[percentile];
                double vector2Value = vector2[percentile];
                interpolatedAgeVector[percentile] = Interpolator.Interpolate (previousAge, vector1Value, currentAge, vector2Value, ageToCreateVectorFor);
            }
            return interpolatedAgeVector;
        }

        /// <summary>
        /// Look for an exact match age vector in the passed in dictionary for the measurement.
        /// If no exact match is found, interpolate between bounding age vectors and return interpolated vector.
        /// </summary>
        private static SortedDictionary<double, double> FindAgeVectorInDictionary(double ageToFindVectorFor,
            SortedDictionary<double, SortedDictionary<double, double>> dictionary)
        {
            double previousAge = 0;
            SortedDictionary<double, double> previousAgeVector = null;
            foreach (double currentAge in dictionary.Keys)
            {
                if (ageToFindVectorFor == currentAge)
                {
                    return dictionary[currentAge];
                }
                else if (currentAge < ageToFindVectorFor)
                {
                    previousAge = currentAge;
                    previousAgeVector = dictionary[currentAge];
                }
                else
                {
                    SortedDictionary<double, double> currentAgeVector = dictionary[currentAge];
                    return CreateInterpolatedAgeVector (previousAge, currentAge, ageToFindVectorFor, previousAgeVector, currentAgeVector);
                }
            }
            throw new Exception ("Could not find age vector for: " + ageToFindVectorFor);
        }

        /// <summary>
        /// Look for an exact match of our measurement attribute value (length or weight) in the passed-in percentile
        /// vector.  If no exact match is found, interpolate between bounding values to get the percentile for 
        /// this passed-in measurement attribute value.
        /// </summary>
        private static double FindPercentileInAgeVector(double value, SortedDictionary<double, double> percentileVector)
        {
            double previousPercentile = 0;
            double previousValue = 0;
            foreach (double currentPercentile in percentileVector.Keys)
            {
                double currentValue = percentileVector[currentPercentile];
                if (value == currentValue)
                {
                    return currentPercentile;
                }
                else if (currentValue < value)
                {
                    previousPercentile = currentPercentile;
                    previousValue = currentValue;
                }
                else
                {
                    return Interpolator.Interpolate (previousValue, previousPercentile, currentValue, currentPercentile, value);
                }
            }
            throw new Exception ("Could not find percentile for: " + value);
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
                        SortedDictionary<double, double> percentileVector = FindAgeVectorInDictionary (measurement.Age, LengthPerAge);
                        double percentile = FindPercentileInAgeVector (measurement.Length.Value, percentileVector);
                        Console.WriteLine ("Age {0} Length {1} Percentile {2}", measurement.Age, measurement.Length, percentile);
                    }
                    if (measurement.Weight.HasValue)
                    {
                        SortedDictionary<double, double> percentileVector = FindAgeVectorInDictionary (measurement.Age, WeightPerAge);
                        double percentile = FindPercentileInAgeVector (measurement.Weight.Value, percentileVector);
                        Console.WriteLine ("Age {0} Weight {1} Percentile {2}", measurement.Age, measurement.Weight, percentile);
                    }
                }
            }
            else
            {
                Console.WriteLine ("Missing input data filename");
            }
        }
    }
}
