using VirginMediaTechExercise.Models;

namespace VirginMediaTechExercise.Services
{
    public interface ISalesDataFileService
    {
        List<SalesData> ParseSalesData(string csvData);
    }
}
