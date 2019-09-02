using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using RestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SondageController : ControllerBase
    {
        static string[] question1 = {
                "1. À quelle tranche d'âge appartenez-vous? a:0-25 ans, b:25-50 ans, c:50-75 ans, d:75 ans et plus",
                "2. Êtes-vous une femme ou un homme? a:Femme, b:Homme, c:Je ne veux pas répondre",
                "3. Quel journal lisez-vous à la maison? a:La Presse, b:Le Journal de Montréal, c:The Gazette, d:Le Devoir",
                "4. Combien de temps accordez-vous à la lecture de votre journal quotidiennement? a:Moins de 10 minutes; b:Entre 10 et 30 minutes, c:Entre 30 et 60 minutes, d:60 minutes ou plus" };

        static string[] question2 = {
                "1. À quelle tranche d'âge appartenez-vous? a:0-25 ans, b:25-50 ans, c:50-75 ans, d:75 ans et +",
                "2. Êtes-vous une femme ou un homme? a:Femme, b:Homme, c:Je ne veux pas répondre",
                "3. Combien de tasses de café buvez-vous chaque jour? a:Je ne bois pas de café, b:Entre 1 et 5 tasses, c:Entre 6 et 10 tasses, d:10 tasses ou plus",
                "4. Combien de consommations alcoolisées buvez-vous chaque jour? a:0, b:1, c:2 ou 3, d:3 ou plus" };

        Sondage[] sondages = new Sondage[]
        {
            new Sondage{Id= "Sondage 1", Questions = question1},
            new Sondage{Id= "Sondage 2", Questions = question2}
        };

        [Authorize(Policy = "ApiKeyPolicy")]
        public IEnumerable<Sondage> GetAllSondage()
        {
            return sondages;
        }
    }
}