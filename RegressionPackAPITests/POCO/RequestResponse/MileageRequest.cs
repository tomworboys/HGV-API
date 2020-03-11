namespace RegressionPackAPITests.POCO.RequestResponse
{
    public class MileageRequest
    {
        public long MileageHistoryId { get; set; }

        public int Mileage { get; set; }

        public int Unit { get; set; }

        public string Token { get; set; }
    }
}
