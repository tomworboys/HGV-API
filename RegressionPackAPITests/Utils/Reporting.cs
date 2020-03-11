using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using RegressionPackAPITests.Context;
using RegressionPackAPITests.POCO.Setup;
using System;
using System.IO;
using TechTalk.SpecFlow;

namespace RegressionPackAPITests.Utils
{
    [Binding]
    public sealed class Reporting
    {
        private static ExtentTest featureName;
        private static ExtentTest scenario;
        private static AventStack.ExtentReports.ExtentReports extent;
        public static string reportPath;
        private readonly ScenarioContext scenarioContext;
        private readonly FeatureContext featureContext;
        private readonly ApiContext apiContext;

        public Reporting(ScenarioContext scenarioContext, FeatureContext featureContext, ApiContext apiContext)
        {
            this.scenarioContext = scenarioContext;
            this.featureContext = featureContext;
            this.apiContext = apiContext;
        }

        [BeforeTestRun]
        public static void SetupReporter()
        {
            extent = new AventStack.ExtentReports.ExtentReports();
            var htmlReporter = new ExtentHtmlReporter(GetReportsPath());
            extent.AttachReporter(htmlReporter);
            extent.AddSystemInfo("Environment", ExecutionConfig.Environment);
        }

        [BeforeScenario]
        public void GetScenarioInfo()
        {
            featureName = extent.CreateTest<Feature>(featureContext.FeatureInfo.Title);
            scenario = featureName.CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title);
        }
        
        [AfterStep]
        public void GetStepsInfo()
        {
            var stepType = this.scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();
            var stepName = this.scenarioContext.StepContext.StepInfo.Text;

            ExtentTest node = null;

            switch (stepType)
            {
                case "Given":
                    node = scenario.CreateNode<Given>("Given: " + stepName);
                    break;
                case "When":
                    node = scenario.CreateNode<When>("When: " + stepName);
                    break;
                case "Then":
                    node = scenario.CreateNode<Then>("Then: " + stepName);
                    break;
                case "And":
                    node = scenario.CreateNode<And>("And: " + stepName);
                    break;
            }

            if (scenarioContext.TestError != null)
            {
                node.Fail(this.scenarioContext.TestError.Message)
                    .Fail(this.GetInnerExceptionMessage())
                    .Fail($"Endpoint: {ExecutionConfig.BaseUrl}{this.apiContext.endpoint}");
            }
        }

        [After]
        public static void FlushReport() => extent.Flush();

        private static string GetReportsPath()
        {
            return $"{Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName}\\Reports\\";
        }

        private string GetInnerExceptionMessage()
        {
            try
            {
                return this.scenarioContext.TestError.InnerException.Message;
            }
            catch (NullReferenceException)
            {
                return "No inner exception message.";
            }
        }
    }
}
