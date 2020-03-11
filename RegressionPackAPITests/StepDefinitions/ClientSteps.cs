using FluentAssertions;
using RegressionPackAPITests.Constants;
using RegressionPackAPITests.Context;
using RegressionPackAPITests.POCO.RequestResponse;
using RegressionPackAPITests.Utils;
using System.Net.Http;
using TechTalk.SpecFlow;

namespace RegressionPackAPITests.StepDefinitions
{
    [Binding]
    public sealed class ClientSteps
    {
        private readonly ApiContext apiContext;
        private readonly ClientHelper clientHelper;

        public ClientSteps(ApiContext context)
        {
            this.apiContext = context;
            clientHelper = new ClientHelper(this.apiContext);
        }

        [Given(@"a user with valid credentials")]
        public void GivenAUserWithValidCredentials()
        {
            apiContext.PostTokenRequest = clientHelper.GetTokenRequest();
        }

        [Given(@"a valid token")]
        public void GivenAValidToken()
        {

        }

        [Given(@"a valid token and mileage history record")]
        public void GivenAValidTokenAndMileageHistoryRecord()
        {
            GivenAValidToken();

            apiContext.MileageRequest = new MileageRequest
            {
                MileageHistoryId = 164950,
                Mileage = 12345,
                Unit = 1
            };
        }

        [Given(@"a valid token and session")]
        public void GivenAValidTokenAndSession()
        {
            apiContext.endpoint = $"{ApiEndpoints.Sessions}?token={apiContext.Token}";

            apiContext.PostTokenResponse = clientHelper.GetRestResonse<PostTokenResponse>(
                apiContext.endpoint, HttpMethod.Get, apiContext.Client).Result;

            apiContext.PostTokenResponse.Success.Should().Be(true);
        }

        [When(@"the api/Tokens/Request is submitted")]
        public void WhenTheApiTokensRequestIsSubmitted()
        {
            apiContext.PostTokenResponse = clientHelper.PostToken(apiContext.Client, apiContext.PostTokenRequest);
        }

        [When(@"the api/dashboards/totals request is submitted")]
        public void WhenTheApiDashboardsTotalsRequestIsSubmitted()
        {
            apiContext.endpoint = $"{ApiEndpoints.DashboardsTotals}?token={apiContext.Token}";

            apiContext.DashboardTotalsResponse = clientHelper.GetRestResonse<DashboardTotalsResponse>(
                apiContext.endpoint, HttpMethod.Get, apiContext.Client).Result;
        }

        [When(@"the api/mileage request is submitted")]
        public void WhenTheApiMileageRequestIsSubmitted()
        {
            apiContext.endpoint = $"{ApiEndpoints.Mileage}/{apiContext.MileageRequest.MileageHistoryId}?" +
                $"mileage={apiContext.MileageRequest.Mileage}&unit={apiContext.MileageRequest.Unit}" +
                $"&token={apiContext.Token}";

            apiContext.MileageResponse = clientHelper.GetRestResonse<MileageResponse>(
                apiContext.endpoint, HttpMethod.Put, apiContext.Client).Result;
        }

        [When(@"the api/sessions delete request is submitted")]
        public void WhenTheApiSessionsDeleteRequestIsSubmitted()
        {
            apiContext.endpoint = $"{ApiEndpoints.Sessions}?token={apiContext.Token}";

            apiContext.SessionDelete = clientHelper.GetRestResonse<SessionDelete>(
                apiContext.endpoint, HttpMethod.Delete, apiContext.Client).Result;
        }

        [Then(@"the api/Tokens response is returned")]
        public void ThenTheApiTokensResponseIsReturned()
        {
            apiContext.PostTokenResponse.Success.Should().Be(true);
            apiContext.PostTokenResponse.Token.Should().NotBeNullOrEmpty();
        }

        [Then(@"the api/dashboards/total response is returned")]
        public void ThenTheApiDashboardsTotalResponseIsReturned()
        {
            apiContext.DashboardTotalsResponse.Success.Should().Be(true);
            apiContext.DashboardTotalsResponse.Data.PkId.Should().Be(1);
        }

        [Then(@"the api/mileage response is returned")]
        public void ThenTheApiMileageResponseIsReturned()
        {
            apiContext.MileageResponse.Success.Should().Be(true);
        }

        [Then(@"the session is deleted")]
        public void ThenTheSessionIsDeleted()
        {
            apiContext.SessionDelete.Success.Should().Be(true);
        }
    }
}
