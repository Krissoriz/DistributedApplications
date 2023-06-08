using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace GamesCatalog.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Catalog()
        {

            return View();
        }

        [HttpGet]
        public ActionResult FreeGames(string id)
        {
                var key = "452";
                string apiUrl = "https://www.freetogame.com/api/game?id=" + key;

                using (var client = new HttpClient())
                {
                    // Add an Accept header for JSON format.
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.BaseAddress = new Uri(apiUrl);
                    var response = client.GetAsync("").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        Console.Write("Success");
                        var body = response.Content.ReadAsStringAsync().Result;
                        JObject data = JObject.Parse(body);
                        ViewData["title"] = data["title"];
                        ViewData["releaseDate"] = data["release_date"];
                        ViewData["genre"] = data["genre"];
                        ViewData["status"] = data["status"];
                        ViewData["image"] = data["thumbnail"];
                        ViewData["description"] = data["short_description"];
                        ViewData["publisher"] = data["publisher"];
                        ViewData["os"] = data["minimum_system_requirements"]["os"];
                        ViewData["processor"] = data["minimum_system_requirements"]["processor"];
                        ViewData["memory"] = data["minimum_system_requirements"]["memory"];
                        ViewData["graphics"] = data["minimum_system_requirements"]["graphics"];
                }
                    else
                    {
                        Console.Write("Error");
                        ViewData["title"] = "There is an error!";
                    }
                }

                return View();

        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}