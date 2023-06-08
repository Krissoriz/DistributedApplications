using System;
using System.Net;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;

namespace GamesCatalog.Controllers
{
    public class FreeGamesController : Controller
    {
        // GET: FreeGames
        public ActionResult FreeGames(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                string apiUrl = "https://www.freetogame.com/api/game?id=" + id;

                using (WebClient client = new WebClient())
                {
                    try
                    {
                        string json = client.DownloadString(apiUrl);

                        JObject data = JObject.Parse(json);

                        if (data != null)
                        {
                            ViewData["result"] = "There is some info about this game!";
                            ViewData["name"] = data["title"];
                            ViewData["genre"] = data["genre"];
                            ViewData["dev"] = data["developer"];
                        }
                        else
                        {
                            ViewData["result"] = "There is no free game available, sorry :(";
                        }

                        ViewData["body"] = json;
                    }
                    catch (WebException ex)
                    {
                        ViewData["result"] = "An error occurred while accessing the API: " + ex.Message;
                    }
                }
            }
            else
            {
                ViewData["result"] = "Please provide a valid game ID.";
            }

            return View();
        }
    }
}
