using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Net;
using System.Text.Json.Nodes;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.CommonModels;

namespace TestProject3.StepDefinitions
{
    [Binding]
    public class GetTokenStepDefinitions
    {
        RestClient client = new RestClient("http://demostore.gatling.io/api/");
        RestRequest request = new RestRequest("authenticate", Method.Post);
        RestResponse response;
        string jsonBody;

        [Given(@"I have a valid username and password")]
        public void GivenIHaveAValidUsernameAndPassword()
        {
            jsonBody = @"{
    ""username"": ""admin"",
    ""password"": ""admin""
}";

        }

        [When(@"I send a POST request")]
        public void WhenISendAPOSTRequest()
        {
            request.AddJsonBody(jsonBody);
            response = client.Execute(request);
        }

        [Then(@"I expect a valid token response")]
        public void ThenIExpectAValidTokenResponse()
        {
            var jsonObject = JObject.Parse(response.Content);
            var token = jsonObject.GetValue("token").ToString();
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(token, !Is.Empty, "Name inexsiten");
            Console.WriteLine(token);
        }

        [Given(@"I have an invalid username and password")]
        public void GivenIHaveAnInvalidUsernameAndPassword()
        {
            jsonBody = @"{
        ""username"": ""wrongAdmin"",
        ""password"": ""admin""
        }";

        }

        [Then(@"I expect an forbiden error response")]
        public void ThenIExpectAForbiddenHttpResponse()
        {
            var jsonObject = JObject.Parse(response.Content);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));

        }
    }
}


