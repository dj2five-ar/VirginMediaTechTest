using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using CsvHelper;
using System.Globalization;
using System.Text.RegularExpressions;

namespace VirginMediaTechExercise.Services
{
    public class NumberValidator : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            // In case of empty field then assume 0. If fails to parse then throw error.
            if (string.IsNullOrEmpty (text))
            {
                return (float)0.0;
            }
            // Remove non-numeric characters (except for decimal points)
            string cleanedText = Regex.Replace(text, @"[^\d.]", string.Empty);

            return float.TryParse(cleanedText, NumberStyles.Number, CultureInfo.InvariantCulture, out var result) ? 
                result : throw new InvalidDataException($"Failed to parse number {text}");
        }
    }
}
