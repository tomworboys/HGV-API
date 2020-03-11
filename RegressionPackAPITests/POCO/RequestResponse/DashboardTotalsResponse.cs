namespace RegressionPackAPITests.POCO.RequestResponse
{
    public class DashboardTotalsResponse
    {
        public DashboardData Data { get; set; }
        public object DataId { get; set; }
        public object LockInfo { get; set; }
        public bool Success { get; set; }
        public object SuccessMessage { get; set; }
        public object ErrorMessage { get; set; }
        public Errors Errors { get; set; }
    }

    public class DashboardData
    {
        public int PkId { get; set; }
        public int BreakdownCount { get; set; }
        public int DefectsCount { get; set; }
        public int ScheduledDefectsCount { get; set; }
        public int ScheduledMessageCount { get; set; }
    }

}
