using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestApi.Models;
using RestApi.Controllers;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;

namespace RestApiTest
{
    [TestClass]
    public class ReponseUnitTest
    {
        [TestMethod]
        public void GetAllReponseSucess()
        {
            var controller = new ReponseController();
            Reponse dummyReponse = new Reponse { QuestionId = 1, SondageId = 1, Result = 'a' };
            controller.PostReponse(dummyReponse);

            List<Reponse> result = controller.GetAllReponse() as List<Reponse>;

            Assert.AreEqual(dummyReponse.QuestionId, result[0].QuestionId);
            Assert.AreEqual(dummyReponse.Result, result[0].Result);
            Assert.AreEqual(dummyReponse.SondageId, result[0].SondageId);
        }

        [TestMethod]
        public void PostReponseSucess()
        {
            var controller = new ReponseController();
            Reponse dummyReponse = new Reponse { QuestionId = 1, SondageId = 1, Result = 'a' };
            HttpResponseMessage reponse = new HttpResponseMessage(HttpStatusCode.Created);
            Assert.AreEqual(reponse.StatusCode, controller.PostReponse(dummyReponse).StatusCode);
        }

        [TestMethod]
        public void PostReponseFailure()
        {
            var controller = new ReponseController();
            Reponse dummyReponse = null;
            HttpResponseMessage reponse = new HttpResponseMessage(HttpStatusCode.BadRequest);
            Assert.AreEqual(reponse.StatusCode, controller.PostReponse(dummyReponse).StatusCode);
        }
    }
}
