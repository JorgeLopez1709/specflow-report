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
        int productId;


        [Given(@"I have an id with a valid value")]
        public void GivenIHaveAnIdWithValue()
        {
            productId = 17;

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

        [Then(@"I get a list of products in the Body Response")]
        public void ThenIGetAListOfProductsInTheBodyResponse()
        {
            var result = response.Content;
            var jsonResponse = JArray.Parse(result);
            TestContext.WriteLine("Number of products Obtained: " + jsonResponse.Count);
            Assert.IsTrue(jsonResponse.Count > 0, "No products found in the response body");
        }

        [Then(@"I get the product according to the ID requested product in the Body Response")]
        public void ThenIGetTheProductAccordingToTheIDRequestedProductInTheBodyResponse()
        {
            var result = response.Content;
            var jsonResponse = JObject.Parse(result);
            var obtainedProductId = jsonResponse["id"].ToString();
            TestContext.WriteLine("Product ID requested: "+ productId);
            TestContext.WriteLine("Product ID Obtained: " + obtainedProductId);
            Assert.AreEqual(productId.ToString(), obtainedProductId, "The obtained product ID doesn't match the requested product ID");
            
        }

    }
}
