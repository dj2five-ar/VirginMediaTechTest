namespace VirginMediaTechExercise.Models
{
    public class SalesDataViewModel
    {
        private List<SalesData> _salesData = [];
        private float _totalRevenue;
        private float _totalProfit;
        private int _totalUnitsSold;
        private List<string> _segments = [];
        private List<string> _countries = [];
        private List<string> _products = [];

        // Only update properties when new sales data is set.
        public List<SalesData> SalesData
        {
            get => _salesData;
            set
            {
                _salesData = value ?? [];
                _totalRevenue = _salesData.Sum(r => r.Revenue);
                _totalProfit = _salesData.Sum(r => r.Profit);
                _totalUnitsSold = (int)_salesData.Sum(r => r.UnitsSold);
                _segments = _salesData.Select(r => r.Segment).Distinct().OrderBy(s => s).ToList();
                _countries = _salesData.Select(r => r.Country).Distinct().OrderBy(c => c).ToList();
                _products = _salesData.Select(r => r.Product).Distinct().OrderBy(p => p).ToList();
            }
        }

        public float TotalRevenue => _totalRevenue;
        public float TotalProfit => _totalProfit;
        public int TotalUnitsSold => _totalUnitsSold;
        public List<string> Segments => _segments;
        public List<string> Countries => _countries;
        public List<string> Products => _products;
    }
}
