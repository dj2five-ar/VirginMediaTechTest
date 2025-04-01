using System.Globalization;
using VirginMediaTechExercise.Models;
using CsvHelper;
using CsvHelper.Configuration;

namespace VirginMediaTechExercise.Services
{
    public class SalesDataFileService : ISalesDataFileService
    {
        public List<SalesData> ParseSalesData(string csvData)
        {
            CsvConfiguration config = new(CultureInfo.InvariantCulture)
            {
                TrimOptions = TrimOptions.Trim,
            };

            using var reader = new StringReader(csvData);
            using var csv = new CsvReader(reader, config);
            return [.. csv.GetRecords<SalesData>()];
        }
    }
}
