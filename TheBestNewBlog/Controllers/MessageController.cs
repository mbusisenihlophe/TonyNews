using NewBlogAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace TheBestNewBlog.Controllers
{
    public class MessageController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            List<MessageModel> news = new List<MessageModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:55747/api/Message");

                HttpResponseMessage response = client.GetAsync(client.BaseAddress).Result;

                if (response.IsSuccessStatusCode)
                {
                    news = (new JavaScriptSerializer()).Deserialize<List<MessageModel>>(response.Content.ReadAsStringAsync().Result);
                }

            }
            return View(news);
        }


        public ActionResult Create()
        {
            return View();
        }

        

        [HttpPost]
        public ActionResult Create(FormCollection collection, MessageModel model)
        {
            try
            {
                

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:55747/api/Message");
                    
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