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
    public class MessageController : ApiController
    {

        NewsDBEntities entities = new NewsDBEntities();

        [AcceptVerbs("GET")]
        [System.Web.Http.Route("api/Message")]
        public IHttpActionResult Get()
        {
            var jsonString = JsonConvert.SerializeObject(entities.Messages.ToList(), Formatting.None, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.None, ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

            return new RawJsonActionResult(jsonString);
        }


        [AcceptVerbs("GET")]
        [System.Web.Http.Route("api/Message/{id}")]
        public IHttpActionResult Get(int id)
        {
            var jsonString = JsonConvert.SerializeObject(entities.Messages.FirstOrDefault(e => e.Id == id), Formatting.None, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.None, ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

            return new RawJsonActionResult(jsonString);
        }


        [AcceptVerbs("POST")]
        [System.Web.Http.Route("api/Message/Create")]
        public HttpResponseMessage Create([FromBody]Message model)
        {

            try
            {
                entities.Messages.Add(model);
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
