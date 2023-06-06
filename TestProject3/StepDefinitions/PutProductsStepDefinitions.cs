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
  ""price"": ""199999.99"",
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
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Then(@"I expect the details of the product were successfully updated")]
        public void ThenIExpectProductDetailsUpdated()
        {
            string sentNewProductName = "Purple Glasses UPDATED for testing purposes";
            string sentNewProductPrice = "199999.99";
            var result = response.Content;
            var jsonResponse = JObject.Parse(result);
            string responseProductName = jsonResponse["name"].ToString();
            string responseProductPrice = jsonResponse["price"].ToString();
            Assert.AreEqual(sentNewProductName, responseProductName, "The updated Product name is not the same as the sent new Product name");
            Assert.AreEqual(sentNewProductPrice, responseProductPrice, "The updated Product price is not the same as the sent new Product price");
        }



    }
}
