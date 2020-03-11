namespace RegressionPackAPITests.POCO.RequestResponse
{
    public class MileageResponse
    {
        public object DataId { get; set; }
        public object LockInfo { get; set; }
        public bool Success { get; set; }
        public string SuccessMessage { get; set; }
        public object ErrorMessage { get; set; }
        public Errors Errors { get; set; }
    }

}

