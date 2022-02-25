using DataContext;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NewBlogAPI.Controllers
{
    public class CategoryController : ApiController
    {

        NewsDBEntities entities = new NewsDBEntities();

        [AcceptVerbs("GET")]
        [System.Web.Http.Route("api/Category")]
        public IHttpActionResult Get()
        {
            var jsonString = JsonConvert.SerializeObject(entities.Categories.ToList(), Formatting.None, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.None, ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

            return new RawJsonActionResult(jsonString);
        }


        [AcceptVerbs("GET")]
        [System.Web.Http.Route("api/Category/{id}")]
        public IHttpActionResult Get(int id)
        {
            var jsonString = JsonConvert.SerializeObject(entities.Categories.FirstOrDefault(e => e.Id == id), Formatting.None, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.None, ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

            return new RawJsonActionResult(jsonString);
        }


        [AcceptVerbs("POST")]
        [System.Web.Http.Route("api/Category/Create")]
        public HttpResponseMessage Create([FromBody]Category model)
        {

            try
            {
                entities.Categories.Add(model);
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
