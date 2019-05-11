using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using aspDOTNETCoreMVC.Models;
using System.Net.Http;
using Newtonsoft.Json;  // JSON serializer and deserializer... Ref: https://www.newtonsoft.com

namespace aspDOTNETCoreMVC.Controllers
{
    public class APIController : ControllerBase
    {

        public ContentResult DevelopersName()
        {
            return Content("Frank Martens");
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<Object>> GetFakeData()
        {
            // ripped the following from some answer on stackoverflow.com...
            //dynamic parsedObject = JsonConvert.DeserializeObject("{ test: { inner: \"text-value\" } }");
            //foreach (dynamic entry in parsedObject)
            //{
            //    string name = entry.Name; // "test"
            //    dynamic value = entry.Value; // { inner: "text-value" }
            //}

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var url = "https://randomuser.me/api/?noinfo";
                    var response = await client.GetAsync(url);

                    if (response != null)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("****************************************");
                        Console.WriteLine("Response: " + jsonString);
                        Console.WriteLine("************************************************************");
                        dynamic parsedObject = JsonConvert.DeserializeObject(jsonString);
                        Console.WriteLine("Deserialized Object: " + parsedObject);
                        Console.WriteLine("************************************************************");
                        return parsedObject;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return "Failed!";
            }
            return null;

        }

    }
}
