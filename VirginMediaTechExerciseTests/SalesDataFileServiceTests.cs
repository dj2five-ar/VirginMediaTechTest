using CsvHelper;
using CsvHelper.TypeConversion;
using Shouldly;
using VirginMediaTechExercise.Services;

namespace VirginMediaTechExerciseTests
{
    public class SalesDataFileServiceTests
    {
        private readonly SalesDataFileService _salesDataService = new();

        [Fact]
        public void ParseSalesData_ValidData()
        {
            var data = @"Segment, Country, Product, Discount Band ,Units Sold, Manufacturing Price,Sale Price, Date
                        Government,Canada, Carretera , None ,1618.5,£3.00,£20.00,01 / 01 / 2014";

            var result = _salesDataService.ParseSalesData(data);
            result.Count.ShouldBe(1);
            result[0].Segment.ShouldBe("Government");
            result[0].Country.ShouldBe("Canada");
            result[0].Product.ShouldBe("Carretera");
            result[0].DiscountBand.ShouldBe("None");
            result[0].UnitsSold.ShouldBe((float)1618.5);
            result[0].ManufacturingPrice.ShouldBe((float)3.00);
            result[0].SalePrice.ShouldBe((float)20.00);
            result[0].Date.ShouldBe(DateTime.Parse("01/01/2014"));
        }

        [Fact]
        public void ParseSalesData_EmptyList()
        {
            var data = @"Segment, Country, Product, Discount Band ,Units Sold, Manufacturing Price,Sale Price, Date";

            var result = _salesDataService.ParseSalesData(data);
            result.Count.ShouldBe(0);
        }

        [Fact]
        public void ParseSalesData_GarbageHeader()
        {
            var data = @"Garbage data";

            Assert.Throws<HeaderValidationException>(() => _salesDataService.ParseSalesData(data));
        }

        [Fact]
        public void ParseSalesData_GarbageData()
        {
            var data = @"Segment,Country, Product , Discount Band ,Units Sold,Manufacturing Price,Sale Price,Date
                        Government,Canada, Carretera , None ,1618.5,GARBAGE,£20.00,GARBAGE";

            Assert.Throws<ReaderException>(() => _salesDataService.ParseSalesData(data));
        }

        [Fact]
        public void ParseSalesData_IllegalNumberFields()
        {
            var data = @"Segment,Country, Product , Discount Band ,Units Sold,Manufacturing Price,Sale Price,Date
                        Government, Canada, Carretera, None, abc1618.5, £3.00.0, £20.00, 01/01/2014";

            Assert.Throws<ReaderException>(() => _salesDataService.ParseSalesData(data));
        }

        [Fact]
        public void ParseSalesData_EmptyStringFields()
        {
            var data = @"Segment,Country, Product , Discount Band ,Units Sold,Manufacturing Price,Sale Price,Date
                        , , , , abc1618.5, £3.00, £20.00, 01/01/2014";

            var result =  _salesDataService.ParseSalesData(data);

            result[0].Segment.ShouldBeEmpty();
            result[0].Country.ShouldBeEmpty();
            result[0].Product.ShouldBeEmpty();
            result[0].DiscountBand.ShouldBeEmpty();
        }

        [Fact]
        public void ParseSalesData_InvalidDate()
        {
            var data = @"Segment,Country, Product , Discount Band ,Units Sold,Manufacturing Price,Sale Price,Date
                        , , , , abc1618.5, £3.00, £20.00, 99/13/2014";

            Assert.Throws<TypeConverterException>(() => _salesDataService.ParseSalesData(data));
        }
    }
}