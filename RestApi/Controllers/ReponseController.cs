using System;
using System.Net.Http;
using RestApi.Models;
using System.Net;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReponseController : ControllerBase
    {
        static readonly Reponse userReponses = new Reponse();

        [Authorize(Policy = "ApiKeyPolicy")]
        public IEnumerable<Reponse> GetAllReponse()
        {
            return userReponses.GetAll();
        }

        [Authorize(Policy = "ApiKeyPolicy")]
        [HttpPost]
        public HttpResponseMessage PostReponse(Reponse reponse)
        {
            if(userReponses.Add(reponse))
            {
               return new HttpResponseMessage(HttpStatusCode.Created);
            }
            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }
    }
}