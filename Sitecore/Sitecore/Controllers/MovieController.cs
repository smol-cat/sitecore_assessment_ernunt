using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sitecore.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Sitecore.Controllers
{
    public class MovieController : Controller
    {
        private string getContent(string link) {
            using (var wc = new System.Net.WebClient())
                return wc.DownloadString(link);
        }

        private async Task<HttpResponseMessage> postContent(string link, List<KeyValuePair<string, string>> content) {
            HttpClient client = new HttpClient();
            var url_content = new FormUrlEncodedContent(content);
            var response = await client.PostAsync(link, url_content).ConfigureAwait(false);
            return response;
        }
        // GET: Movie
        public ActionResult Index()
        {
            string contents = getContent("https://virtserver.swaggerhub.com/BartvdPost/NetNix/0.2.0/soon/");
            JArray rss = JArray.Parse(contents);
            IEnumerable<MovieModel> movies = rss.OrderBy(c => DateTime.Parse((string)c["releaseDate"])).Select(c => 
                new MovieModel((string)c["id"], 
                                (string)c["name"], 
                                DateTime.Parse((string)c["releaseDate"]).ToString("yyyy-MM-dd"), 
                                (string)c["director"]["name"], 
                                (string)c["director"]["id"])).Take(5);
            return View(movies);
        }

        // GET: Movie/Details/<id>
        public ActionResult Details(string id, string directorId)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            string contents = getContent("https://virtserver.swaggerhub.com/BartvdPost/NetNix/0.2.0/movie/" + id);
            string likeContents = getContent("https://virtserver.swaggerhub.com/BartvdPost/NetNix/0.2.0/like/" + id);
            JObject rss = JObject.Parse(contents);
            JObject likeRss = JObject.Parse(likeContents);
            DetailedMovieModel model = new DetailedMovieModel((string)rss["id"], 
                (string)rss["name"], 
                DateTime.Parse((string)rss["releaseDate"]).ToString("yyyy-MM-dd"), 
                (string)rss["shortDescription"], 
                (string)rss["description"], 
                (string)rss["image:"],   // <---- nice JSON
                (string)rss["director"]["name"], 
                DateTime.Parse((string)rss["director"]["dateOfBirth"]).ToString("yyyy-MM-dd"),
                directorId,
                (int)likeRss["like"]);
            return View(model);
        }

        // GET: Movie/DirectorDetails/<id>
        public ActionResult DirectorDetails(string id) 
        {
            if(id == null) {
                return RedirectToAction("Index");
            }
            string contents = getContent("https://virtserver.swaggerhub.com/BartvdPost/NetNix/0.2.0/director/" + id);
            JObject rss = JObject.Parse(contents);
            DirectorModel director = new DirectorModel((string)rss["name"], DateTime.Parse((string)rss["dateOfBirth"]).ToString("yyyy-MM-dd"));
            return View(director);
        }

        // POST: Movie/Like/<id>
        [HttpPost]
        public async Task<ActionResult> Like(string id) {
            string link = "https://virtserver.swaggerhub.com/BartvdPost/NetNix/0.2.0/like";
            List<KeyValuePair<string, string>> info = new List<KeyValuePair<string, string>>();
            info.Add(new KeyValuePair<string, string>("username", "user"));
            info.Add(new KeyValuePair<string, string>("movieId", id));
            var response = await postContent(link, info);
          
            return RedirectToAction("Details", new { id });
        }
    }
}
