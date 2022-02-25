using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NewBlogAPI.Models;
using DataContext;
using Newtonsoft.Json;

namespace NewBlogAPI.Controllers
{
    public class NewsController : ApiController
    {
        NewsDBEntities entities = new NewsDBEntities();


        [AcceptVerbs("GET")]
        [System.Web.Http.Route("api/News")]
        public IHttpActionResult Get()
        {   
            var jsonString =  JsonConvert.SerializeObject(entities.Blogs.ToList(), Formatting.None, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.None, ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

            return new RawJsonActionResult(jsonString);             
        }

        [AcceptVerbs("GET")]
        [System.Web.Http.Route("api/News/{id}")]
        public IHttpActionResult Get(int id)
        {                           
            var jsonString = JsonConvert.SerializeObject(entities.Blogs.FirstOrDefault(e => e.Id == id), Formatting.None, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.None, ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

            return new RawJsonActionResult(jsonString);           
        }

        [AcceptVerbs("POST")]
        [System.Web.Http.Route("api/News/Create")]
        public HttpResponseMessage Create([FromBody]Blog model)
        {

            try
            {
                entities.Blogs.Add(model);
                entities.SaveChanges();
                
                return Request.CreateResponse(HttpStatusCode.Created, model);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

    }
}
