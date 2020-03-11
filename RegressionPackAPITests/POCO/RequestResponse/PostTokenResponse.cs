using System;

namespace RegressionPackAPITests.POCO.RequestResponse
{
    public class PostTokenResponse
    {
        public string Token { get; set; }
        public DateTime ExpiryDateTime { get; set; }
        public DateTime IssueDateTime { get; set; }
        public User User { get; set; }
        public bool Success { get; set; }
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
        public Errors Errors { get; set; }
    }

    public class User
    {
        public string UserId { get; set; }
        public string EmailAddress { get; set; }
        public int UserTypeId { get; set; }
        public int StaffId { get; set; }
        public string StaffName { get; set; }
        public string StaffRef { get; set; }
        public bool Enabled { get; set; }
        public DateTime LastActiveDateTime { get; set; }
        public string[] AccessPolicies { get; set; }
    }

    public class Errors
    {
    }

    public class SessionDelete
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public object SuccessMessage { get; set; }
        public object ErrorMessage { get; set; }
        public Errors Errors { get; set; }
    }
}
