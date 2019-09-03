using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestApi.Models;
using RestApi.Controllers;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;

namespace RestApiTest
{
    [TestClass]
    public class UsagerUnitTest
    {
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddUserNull()
        {
            IUsager user = new UsagerRest();
            user.Add(null);
        }

        [TestMethod]
        public void GetAllUsagerSucess()
        {
            var controller = new UsagerController();

            List<Usager> usagers = new List<Usager>();

            int[] completeSondageInit = { 0, 0 };
            usagers.Add(new Usager { Id = 0, UserName = "Jacob", Mdp = "mdp1", CompleteSondageId = completeSondageInit });
            usagers.Add(new Usager { Id = 1, UserName = "Marie", Mdp = "mdp2", CompleteSondageId = completeSondageInit });

            List<Usager> result = controller.GetAllUsager() as List<Usager>;

            Assert.AreEqual(usagers[0].Id, result[0].Id);
            Assert.AreEqual(usagers[0].Mdp, result[0].Mdp);
            Assert.AreEqual(usagers[0].CompleteSondageId[0], result[0].CompleteSondageId[0]);
            Assert.AreEqual(usagers[0].CompleteSondageId[1], result[0].CompleteSondageId[1]);
            Assert.AreEqual(usagers[0].UserName, result[0].UserName);

            Assert.AreEqual(usagers[1].Id, result[1].Id);
            Assert.AreEqual(usagers[1].Mdp, result[1].Mdp);
            Assert.AreEqual(usagers[1].CompleteSondageId[0], result[1].CompleteSondageId[0]);
            Assert.AreEqual(usagers[1].CompleteSondageId[1], result[1].CompleteSondageId[1]);
            Assert.AreEqual(usagers[1].UserName, result[1].UserName);
        }

        [TestMethod]
        public void GetUsagerSucess()
        {
            var controller = new UsagerController();

            List<Usager> usagers = new List<Usager>();

            int[] completeSondageInit = { 0, 0 };
            usagers.Add(new Usager { Id = 0, UserName = "Jacob", Mdp = "mdp1", CompleteSondageId = completeSondageInit });
            usagers.Add(new Usager { Id = 1, UserName = "Marie", Mdp = "mdp2", CompleteSondageId = completeSondageInit });

            Usager result = controller.GetUsager(0) as Usager;

            Assert.AreEqual(usagers[0].Id, result.Id);
            Assert.AreEqual(usagers[0].Mdp, result.Mdp);
            Assert.AreEqual(usagers[0].CompleteSondageId[0], result.CompleteSondageId[0]);
            Assert.AreEqual(usagers[0].CompleteSondageId[1], result.CompleteSondageId[1]);
            Assert.AreEqual(usagers[0].UserName, result.UserName);
        }

        [TestMethod]
        public void GetUsagerFailure()
        {
            var controller = new UsagerController();

            List<Usager> usagers = new List<Usager>();

            int[] completeSondageInit = { 0, 0 };
            usagers.Add(new Usager { Id = 0, UserName = "Jacob", Mdp = "mdp1", CompleteSondageId = completeSondageInit });
            usagers.Add(new Usager { Id = 1, UserName = "Marie", Mdp = "mdp2", CompleteSondageId = completeSondageInit });

            Usager result = controller.GetUsager(2) as Usager;

            Assert.AreEqual(null, result);
        }


        [TestMethod]
        public void PutUsagerSucess()
        {
            var controller = new UsagerController();

            int[] completeSondageInit = { 0, 0 };
            Usager user = new Usager { Id = 0, UserName = "Jacob", Mdp = "mdp1", CompleteSondageId = completeSondageInit };
            Usager result = controller.GetUsager(0) as Usager;

            Assert.AreEqual(user.Id, result.Id);
            Assert.AreEqual(user.Mdp, result.Mdp);
            Assert.AreEqual(user.CompleteSondageId[0], result.CompleteSondageId[0]);
            Assert.AreEqual(user.CompleteSondageId[1], result.CompleteSondageId[1]);
            Assert.AreEqual(user.UserName, result.UserName);

            user.CompleteSondageId[0] = 1;
            HttpResponseMessage reponse = new HttpResponseMessage(HttpStatusCode.Accepted);
            Assert.AreEqual(reponse.StatusCode, controller.PutUsager(user).StatusCode);

            result = controller.GetUsager(0) as Usager;

            Assert.AreEqual(user.Id, result.Id);
            Assert.AreEqual(user.Mdp, result.Mdp);
            Assert.AreEqual(user.CompleteSondageId[0], result.CompleteSondageId[0]);
            Assert.AreEqual(user.CompleteSondageId[1], result.CompleteSondageId[1]);
            Assert.AreEqual(user.UserName, result.UserName);
        }

        [TestMethod]
        public void PutUsagerFailureBadIndex()
        {
            var controller = new UsagerController();

            int[] completeSondageInit = { 0, 0 };
            Usager user = new Usager { Id = 3, UserName = "Jacob", Mdp = "mdp1", CompleteSondageId = completeSondageInit };

            HttpResponseMessage reponse = new HttpResponseMessage(HttpStatusCode.BadRequest);
            Assert.AreEqual(reponse.StatusCode, controller.PutUsager(user).StatusCode);
        }

        [TestMethod]
        public void PutUsagerFailureNull()
        {
            var controller = new UsagerController();

            HttpResponseMessage reponse = new HttpResponseMessage(HttpStatusCode.BadRequest);
            Assert.AreEqual(reponse.StatusCode, controller.PutUsager(null).StatusCode);
        }

        [TestMethod]
        public void LoginUsagerSucess()
        {
            int[] completeSondageInit = { 0, 0 };
            Usager expectedUser = new Usager { Id = 0, UserName = "Jacob", Mdp = "mdp1", CompleteSondageId = completeSondageInit };

            string login = Base64Encode("Jacob") + ":" + Base64Encode("mdp1");

            Mock<UsagerController> chk = new Mock<UsagerController>();
            chk.Setup(x => x.headerValue()).Returns(login);

            OkObjectResult expectedReponse = new OkObjectResult(expectedUser);
            OkObjectResult reponse = chk.Object.Authenticate("login") as OkObjectResult;
            Assert.AreEqual(expectedReponse.StatusCode, reponse.StatusCode);
        }

        [TestMethod]
        public void LoginUsagerFailureBadUsername()
        {
            string login = Base64Encode("Macob") + ":" + Base64Encode("mdp1");

            Mock<UsagerController> chk = new Mock<UsagerController>();
            chk.Setup(x => x.headerValue()).Returns(login);

            BadRequestResult expectedReponse = new BadRequestResult();
            BadRequestResult reponse = chk.Object.Authenticate("login") as BadRequestResult;
            Assert.AreEqual(expectedReponse.StatusCode, reponse.StatusCode);
        }

        [TestMethod]
        public void LoginUsagerFailureBadPassword()
        {
            string login = Base64Encode("Jacob") + ":" + Base64Encode("mdp2");

            Mock<UsagerController> chk = new Mock<UsagerController>();
            chk.Setup(x => x.headerValue()).Returns(login);

            BadRequestResult expectedReponse = new BadRequestResult();
            BadRequestResult reponse = chk.Object.Authenticate("login") as BadRequestResult;
            Assert.AreEqual(expectedReponse.StatusCode, reponse.StatusCode);
        }
    }
}
