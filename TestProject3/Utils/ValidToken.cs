using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject3.Utils
{
    public class ValidToken
    {
        //Instancia privada de la misma clase (Privado s, solo accesible desde una propiedad)
        private static  ValidToken instance = null;
        //atributo que se modifica una vez que se tiene una instancia
        private string token;

        //Constructor
        private ValidToken()
        {
            //atributos o configuraciones de la instancia unica.
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
            token = jsonObject.GetValue("token").ToString();
        }
        //Propiedad de la clase para crear la instancia u otener la instancia creada    
        public static ValidToken Instance()
        {

            if (instance == null)
                instance = new ValidToken();

            return instance;

        }
        public void ClearToken()
        {
            instance = null;
            token=null;
        }
        public string GetToken()
        {

            return token;

        }
    }
}
