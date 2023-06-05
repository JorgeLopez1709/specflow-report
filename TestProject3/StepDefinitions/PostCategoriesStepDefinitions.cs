using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Net;
using TechTalk.SpecFlow;
using TestProject3.Utils;

namespace TestProject3.StepDefinitions
{
    [Binding]
    public class PostCategoriesStepDefinitions
    {
        RestClient client = new RestClient("http://demostore.gatling.io/api/");
        RestRequest request = new RestRequest("category", Method.Post);
        RestResponse response;

        [Given(@"I have a valid token authorization")]
        public void GivenIHaveAValidTokenAuthorization()
        {
            request.AddHeader("Authorization", "Bearer " + ValidToken.Instance().GetToken());
        }

        [Given(@"I have a valid Category Json body")]
        public void GivenIHaveAValidCategoryJsonBody()
        {
            var jsonBody = @"{
                              ""name"": ""Category created for testing Purposes using the POST method""
                              }";
            request.AddJsonBody(jsonBody);
        }

        [When(@"I send a post request")]
        public void WhenISendAPostRequest()
        {
            response = client.Execute(request);
        }

        [Then(@"I expect a valid Post Http code response")]
        public void ThenIExpectAValidPostHttpCodeResponse()
        {
            var result = response;
            TestContext.WriteLine("Updated product, Body Response:\n ****************");
            TestContext.WriteLine(JToken.Parse(result.Content));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}
