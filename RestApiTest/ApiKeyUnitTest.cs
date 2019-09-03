using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestApi.Models;
using RestApi.Controllers;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace RestApiTest
{
    [TestClass]
    public class ApiKeyUnitTest
    {
        //static IEnumerable<string> keylist = new List<string>() { "key" };

        //ApiKeyRequirement requirement = new ApiKeyRequirement(keylist);

        //ClaimsPrincipal claim = new ClaimsPrincipal(
        //    new ClaimsIdentity(
        //        new Claim[] { new Claim(ClaimsIdentity.DefaultNameClaimType, "babob") },"Basic"
        //            ));

        //ControllerContext context = new ControllerContext();

    }
}
