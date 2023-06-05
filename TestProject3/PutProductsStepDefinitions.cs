using RestSharp;
using System;
using System.Net;
using TechTalk.SpecFlow;

namespace TestProject3
{
    [Binding]
    public class PutProductsStepDefinitions
    {
        RestClient client = new RestClient("http://demostore.gatling.io/api/");

        RestRequest request = new RestRequest("product/{productid}", Method.Put);

        RestResponse response;
        [Given(@"I have an id with a valid value (.*)")]
        public void GivenIHaveAnIdWithAValidValue(int p0)
        {
            request.AddUrlSegment("productid", p0);
        }

        [When(@"I send a put request")]
        public void WhenISendAPutRequest()
        {
            response = client.Execute(request);
        }

        [Then(@"I expected a valid HTTP code response")]
        public void ThenIExpectedAValidHTTPCodeResponse()
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}
