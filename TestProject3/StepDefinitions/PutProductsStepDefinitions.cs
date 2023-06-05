using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Net;
using TechTalk.SpecFlow;
using TestProject3.Utils;

namespace TestProject3.StepDefinitions
{
    [Binding]
    public class PutProductsStepDefinitions
    {
        RestClient client = new RestClient("http://demostore.gatling.io/api/");
        RestRequest request = new RestRequest("product/{productid}", Method.Put);
        RestResponse response;


        [Given(@"I have a valid Id")]
        public void GivenIHaveAnIdWithAValidValue()
        {
            request.AddUrlSegment("productid", 17);
        }


        [Given(@"I have a valid Json body")]
        public void GivenIHaveAValidJsonBody()
        {
            var jsonBody = @"{
    ""name"": ""Purple Glasses UPDATED for testing purposes"",
  ""description"": ""Purple Glasses"",
  ""image"": ""purple-glasses.jpg"",
  ""price"": ""19.99"",
  ""categoryId"": 7
}";
            request.AddJsonBody(jsonBody);
        }

        [When(@"I send a put request")]
        public void WhenISendAPutRequest()
        {
            request.AddHeader("Authorization", "Bearer " + ValidToken.Instance().GetToken());
            response = client.Execute(request);

        }
        [Then(@"I expect a valid Put Http code response")]
        public void ThenIExpectedAValidCodeResponse()
        {
            var result = response;
            TestContext.WriteLine("Updated product, Body Response:\n ****************");
            TestContext.WriteLine(JToken.Parse(result.Content));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }


    }
}
