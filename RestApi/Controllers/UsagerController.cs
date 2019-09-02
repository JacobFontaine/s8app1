using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RestApi.Models;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;

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
        public IActionResult LoginUsager(Usager user)
        {
            IEnumerable<Usager> userList = usagers.GetAll();
            foreach (Usager usage in userList)
            {
                if(usage.UserName == user.UserName && usage.Mdp == user.Mdp)
                {
                    return new OkObjectResult(usage);
                }
            }

            return new BadRequestResult();          
        }
    }
}