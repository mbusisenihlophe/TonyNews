using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TheBestNewBlog.Models;
using System.Web.Http.Cors;

namespace TheBestNewBlog.Controllers
{
    [EnableCors(origins: "http://localhost:52292", headers: "*", methods: "*")]
    public class NewsController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            List<NewsModel> news = new List<NewsModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:55747/api/News");

                HttpResponseMessage response = client.GetAsync(client.BaseAddress).Result;

                if (response.IsSuccessStatusCode)
                {
                    news = (new JavaScriptSerializer()).Deserialize<List<NewsModel>>(response.Content.ReadAsStringAsync().Result);
                }

            }
            return View(news);
        }

        [HttpGet]
        public JsonResult getNewsById(int id)
        {

            NewsModel news = new NewsModel();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:55747/api/News/"+id);

                HttpResponseMessage response = client.GetAsync(client.BaseAddress).Result;

                if (response.IsSuccessStatusCode)
                {
                    news = (new JavaScriptSerializer()).Deserialize<NewsModel>(response.Content.ReadAsStringAsync().Result);
                }

            }

            return Json(news, JsonRequestBehavior.AllowGet);
        }


        //public ActionResult showNews()
        //{
        //    return View(new NewsModel());
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult showNews(int id)
        {

            NewsModel news = new NewsModel();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:55747/api/News/" + id);

                HttpResponseMessage response = client.GetAsync(client.BaseAddress).Result;

                if (response.IsSuccessStatusCode)
                {
                    news = (new JavaScriptSerializer()).Deserialize<NewsModel>(response.Content.ReadAsStringAsync().Result);
                }

            }

            return View(news);
        }


        public ActionResult Create()
        {
            return View();
        }

        

        [HttpPost]
        public ActionResult Create(FormCollection collection, NewsModel model)
        {
            try
            {


                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:55747/api/News");

                    model.Status = 1;

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync(client.BaseAddress + string.Format("/Create"), model);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }

                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");



                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }



    }
}