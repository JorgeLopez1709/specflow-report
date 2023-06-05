using Newtonsoft.Json.Linq;
using RestSharp;
using System.Drawing.Printing;
using System.Net;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.CommonModels;

namespace TestProject3
{
    [Binding]
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }
        public string GetAuthToken()
        {
            var client = new RestClient("http://demostore.gatling.io/api/");
            var request = new RestRequest("authenticate", Method.Post);
            var jsonBody = @"{
    ""username"": ""admin"",
    ""password"": ""admin""
}";
            request.AddJsonBody(jsonBody);

            var response = client.Execute(request);
            var content = response.Content;
            var jsonObject = JObject.Parse(content);
            var token = jsonObject.GetValue("token").ToString();
            Console.WriteLine(token);
            return token;
        }
        
        [Test]
        public void Test1()
        {
            var client = new RestClient("http://demostore.gatling.io/api/");
            var request = new RestRequest ("product/{productid}", Method.Get);
            request.AddUrlSegment("productid","17");
            var response = client.ExecuteGet(request);
            var content = response.Content;
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var jsonObject = JObject.Parse(content);
            //var result = jsonObject.SelectToken("name").ToString();
            var result = jsonObject.ToString();
            Assert.That(result, !Is.Empty, "Name inexsiten");
            Console.WriteLine(result);
            Assert.IsNotNull(response);
        }
        [Test]
        public void Test2()
        {
            var client = new RestClient("http://demostore.gatling.io/api/");
            var request = new RestRequest("product/{productid}", Method.Post);
            request.AddUrlSegment("productid", "17");
            var response = client.Execute(request);
            var content = response.Content;
            //Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var jsonObject = JObject.Parse(content);
            var result = jsonObject.ToString();
            Assert.That(result, !Is.Empty, "Name inexsiten");
            Console.WriteLine(result);
            Assert.IsNotNull(response);
        }
        [Test]
        public void Test3()
        {
            
            var token = GetAuthToken();

            var client = new RestClient("http://demostore.gatling.io/api/");
            var request = new RestRequest("product/{productid}", Method.Put);
            request.AddUrlSegment("productid", "17");

            request.AddHeader("Authorization", "Bearer " + token);
            var jsonBody = @"{
    ""name"": ""Purple Glasses12"",
  ""description"": ""Purple Glasses"",
  ""image"": ""purple-glasses.jpg"",
  ""price"": ""19.99"",
  ""categoryId"": 7
}";
            request.AddJsonBody(jsonBody);
            var response = client.Execute(request);
            var result = JObject.Parse(response.Content).ToString();
            Console.WriteLine(result);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            // Resto del código...
            Test1();
        }
        
    }
}