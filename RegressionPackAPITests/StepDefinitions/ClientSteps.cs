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

        public ClientSteps(ApiContext context)
        {
            this.apiContext = context;
        }

        [Given(@"a user with valid credentials")]
        public void GivenAUserWithValidCredentials()
        {
            apiContext.PostTokenRequest = ClientHelper.GetTokenRequest();
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
            apiContext.PostTokenResponse = ClientHelper.GetRestResonse<PostTokenResponse>(
                $"{ApiEndpoints.Sessions}?token={apiContext.Token}", HttpMethod.Get, apiContext.Client).Result;

            apiContext.PostTokenResponse.Success.Should().Be(true);
        }

        [When(@"the api/Tokens/Request is submitted")]
        public void WhenTheApiTokensRequestIsSubmitted()
        {
            apiContext.PostTokenResponse = ClientHelper.PostToken(apiContext.Client, apiContext.PostTokenRequest);
        }

        [When(@"the api/dashboards/totals request is submitted")]
        public void WhenTheApiDashboardsTotalsRequestIsSubmitted()
        {
            apiContext.DashboardTotalsResponse = ClientHelper.GetRestResonse<DashboardTotalsResponse>(
                $"{ApiEndpoints.DashboardsTotals}?token={apiContext.Token}", HttpMethod.Get, apiContext.Client).Result;
        }

        [When(@"the api/mileage request is submitted")]
        public void WhenTheApiMileageRequestIsSubmitted()
        {
            apiContext.MileageResponse = ClientHelper.GetRestResonse<MileageResponse>(
                $"{ApiEndpoints.Mileage}/{apiContext.MileageRequest.MileageHistoryId}?" +
                $"mileage={apiContext.MileageRequest.Mileage}&unit={apiContext.MileageRequest.Unit}" +
                $"&token={apiContext.Token}", HttpMethod.Put, apiContext.Client).Result;
        }

        [When(@"the api/sessions delete request is submitted")]
        public void WhenTheApiSessionsDeleteRequestIsSubmitted()
        {
            apiContext.SessionDelete = ClientHelper.GetRestResonse<SessionDelete>(
                $"{ApiEndpoints.Sessions}?token={apiContext.Token}", HttpMethod.Delete, apiContext.Client).Result;
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
