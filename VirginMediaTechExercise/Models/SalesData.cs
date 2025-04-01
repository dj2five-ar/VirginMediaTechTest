using CsvHelper.Configuration.Attributes;
using VirginMediaTechExercise.Services;

namespace VirginMediaTechExercise.Models
{
    public class SalesData
    {
        [Name("Segment")]
        public string Segment { get; set; } = string.Empty;

        [Name("Country")]
        public string Country { get; set; } = string.Empty;

        [Name("Product")]
        public string Product { get; set; } = string.Empty;

        [Name("Discount Band")]
        public string DiscountBand { get; set; } = string.Empty;

        [Name("Units Sold")]
        [TypeConverter(typeof(NumberValidator))]
        public float UnitsSold { get; set; } = 0;

        [Name("Manufacturing Price")]
        [TypeConverter(typeof(NumberValidator))]
        public float ManufacturingPrice { get; set; } = 0;

        [Name("Sale Price")]
        [TypeConverter(typeof(NumberValidator))]
        public float SalePrice { get; set; } = 0;

        [Name("Date")]
        public DateTime Date { get; set; }

        public float Revenue => UnitsSold * SalePrice;

        public float Profit => Revenue - (UnitsSold * ManufacturingPrice);

    }
}
