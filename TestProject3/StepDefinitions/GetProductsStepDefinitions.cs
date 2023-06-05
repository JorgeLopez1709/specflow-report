using Gherkin;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Net;
using TechTalk.SpecFlow;

namespace TestProject3.StepDefinitions
{
    [Binding]
    public class GetProductsStepDefinitions
    {
        RestClient client = new RestClient("http://demostore.gatling.io/api/");

        RestRequest request;

        RestResponse response;

        [Given(@"I have an id with a valid value")]
        public void GivenIHaveAnIdWithValue()
        {
            int productId = 17;

            request = new RestRequest("product/" + productId, Method.Get);
        }

        [Given(@"I have the valid products endpoint")]
        public void GivenIHaveTheProductsEnpoint()
        {

            request = new RestRequest("product", Method.Get);
        }
        [When(@"I send a get request")]
        public void WhenISendIGetRequest()
        {
            response = client.Execute(request);
        }

        [Then(@"I expect a valid Get Http code response")]
        public void ThenIExpectedAValidCodeResponse()
        {
            var result = response;
            TestContext.WriteLine("Body Response:\n ****************");
            TestContext.WriteLine(JToken.Parse(result.Content));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            //string result = JObject.Parse(response.Content).ToString();

        }


    }
}
