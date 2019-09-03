using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RestApi.Models;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Text;

namespace RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsagerController : ControllerBase
    {
        static readonly IUsager usagers = new UsagerRest();

        [Authorize(Policy = "ApiKeyPolicy")]
        [HttpGet]
        public IEnumerable<Usager> GetAllUsager()
        {
            return usagers.GetAll();
        }

        [Authorize(Policy = "ApiKeyPolicy")]
        [HttpGet("{id}")]
        public Usager GetUsager(int id)
        {
            return usagers.Get(id);
        }

        [Authorize(Policy = "ApiKeyPolicy")]
        [HttpPut("{id}")]
        public HttpResponseMessage PutUsager(Usager item)
        {
            if (usagers.Update(item))
            {
                return new HttpResponseMessage(HttpStatusCode.Accepted);
            }
            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }

        [Authorize(Policy = "ApiKeyPolicy")]
        [HttpPost]
        public IActionResult Authenticate(string action)
        {
            string values = headerValue();
            string[] credentials = values.ToString().Split(':');
            string username = Base64Decode(credentials[0]);
            string password = Base64Decode(credentials[1]);

            IEnumerable<Usager> userList = usagers.GetAll();
            foreach (Usager usage in userList)
            {
                if (usage.UserName == username && usage.Mdp == password)
                {
                    return new OkObjectResult(usage);
                }
            }
            return new BadRequestResult();
        }
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public virtual string headerValue()
        {
            _ = Request.Headers.TryGetValue("Login", out Microsoft.Extensions.Primitives.StringValues values);
            return values;
        }
    }
}