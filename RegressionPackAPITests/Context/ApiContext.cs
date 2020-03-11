using RegressionPackAPITests.POCO.RequestResponse;
using System.Net.Http;

namespace RegressionPackAPITests.Context
{
    public class ApiContext
    {
        public HttpClient Client { get; set; }

        public string Token { get; set; }

        public PostTokenRequest PostTokenRequest { get; set; }

        public PostTokenResponse PostTokenResponse { get; set; }

        public DashboardTotalsResponse DashboardTotalsResponse { get; set; }

        public MileageRequest MileageRequest { get; set; }

        public MileageResponse MileageResponse { get; set; }

        public SessionDelete SessionDelete { get; set; }
    }
}
