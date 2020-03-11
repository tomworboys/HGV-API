using Microsoft.Extensions.Configuration;
using RegressionPackAPITests.Context;
using RegressionPackAPITests.POCO.Setup;
using TechTalk.SpecFlow;

namespace RegressionPackAPITests.Utils
{
    [Binding]
    public sealed class Hooks
    {
        private static IConfiguration config;
        private readonly ApiContext apiContext;
        private readonly ClientHelper clientHelper;

        public Hooks(ApiContext apiContext)
        {
            this.apiContext = apiContext;
            clientHelper = new ClientHelper(this.apiContext);
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddJsonFile("appsettings.json")
                .Build();

            ExecutionConfig.BaseUrl = config.GetValue<string>("TestExecutionConfig:baseUrl");
            ExecutionConfig.Environment = config.GetValue<string>("TestExecutionConfig:environment");
            ExecutionConfig.ApiKey = config.GetValue<string>("TestExecutionConfig:apiKey");
            ExecutionConfig.Username = config.GetValue<string>("TestExecutionConfig:username");
            ExecutionConfig.Password = config.GetValue<string>("TestExecutionConfig:password");
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            apiContext.Client = clientHelper.GetClient();
            apiContext.Token = clientHelper.PostToken(apiContext.Client, clientHelper.GetTokenRequest()).Token;
        }

        [AfterScenario]
        public void AfterScenario()
        {
            apiContext.Client.Dispose();
        }
    }
}
