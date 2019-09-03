using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace app
{
    class Program
    {
        public class Sondage
        {
            public string Id { get; set; }
            public string[] Questions { get; set; }
            public string[] Reponses { get; set; }

        }
        public class Reponse
        {
            public int SondageId { get; set; }
            public int QuestionId { get; set; }
            public char Result { get; set; }
        }

        public class Usager
        {
            public int Id { get; set; }
            public string UserName { get; set; }
            public string Mdp { get; set; }
            public int[] CompleteSondageId { get; set; }
        }

        static char ReadConsoleAnswer()
        {
            char value = Console.ReadKey().KeyChar;
            while (value != 'a' && value != 'b' && value != 'c' && value != 'd')
            {
                Console.WriteLine("\nVeuillez choisir une reponse valide (a,b,c ou d)");
                value = Console.ReadKey().KeyChar;
            }
            return value;
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        static Usager ConnexionUsager(HttpClient client, string usagerUrl)
        {
            Console.WriteLine("Entrez votre usager:");

            string username = Console.ReadLine();

            Console.WriteLine("Entrez votre mot de passe:");

            string mdp = Console.ReadLine();

            string login = Base64Encode(username)+":"+ Base64Encode(mdp);

            client.DefaultRequestHeaders.Add("Login", login);

            Usager user = new Usager { UserName = username, Mdp = mdp };

            string action = "login";

            HttpResponseMessage response = client.PostAsJsonAsync(new Uri(usagerUrl), action).Result;

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Connexion réussie");
            }
            else
            {
                while (response.IsSuccessStatusCode == false)
                {
                    Console.WriteLine("Mauvais identifiant ou mot de passe");

                    Console.WriteLine("Entrez votre usager:");

                    username = Console.ReadLine();

                    Console.WriteLine("Entrez votre mot de passe:");

                    mdp = Console.ReadLine();

                    user.UserName = username;
                    user.Mdp = mdp;
                    login = Base64Encode(username) + ":" + Base64Encode(mdp);
                    client.DefaultRequestHeaders.Remove("Login");
                    client.DefaultRequestHeaders.Add("Login", login);
                    response = client.PostAsJsonAsync(new Uri(usagerUrl), action).Result;
                }
                Console.WriteLine("Connexion réussie");
            }

            string responseBody = response.Content.ReadAsStringAsync().Result;

            Usager usager = JsonConvert.DeserializeObject<Usager>(responseBody);

            return usager;
        }
        static void Main(string[] args)
        {

            HttpClient client = new HttpClient();
            
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("apikey");
            string sondageUrl = "https://localhost:44301/api/sondage";
            string reponseUrl = "https://localhost:44301/api/reponse";
            string usagerUrl = "https://localhost:44301/api/usager";

            Usager user = ConnexionUsager(client, usagerUrl);

            HttpResponseMessage response = client.GetAsync(new Uri(sondageUrl)).Result;

            string responseBody = response.Content.ReadAsStringAsync().Result;

            List<Sondage> sondages = JsonConvert.DeserializeObject<List<Sondage>>(responseBody);

            Console.WriteLine("Bienvenue dans le systeme de sondage. Veuillez entrer le sondage que vous voulez faire (0 ou 1, q pour quitter):");

            char sondageValue = Console.ReadKey().KeyChar;
            if (sondageValue == 'q')
            {
                Environment.Exit(1);
            }
            while (sondageValue != '0' && sondageValue != '1')
            {
                Console.WriteLine("\nVeuillez choisir un sondage valide (0 ou 1, q pour quitter)");
                sondageValue = Console.ReadKey().KeyChar;
                if (sondageValue == 'q')
                {
                    Environment.Exit(1);
                }
            }
            int numeroSondage = Convert.ToInt32(sondageValue.ToString());

            while (user.CompleteSondageId[numeroSondage] == 1)
            {
                Console.WriteLine("\nVous avez deja repondu à ce sondage, veuillez en choisir un autre\n");
                sondageValue = Console.ReadKey().KeyChar;
                while (sondageValue != '0' && sondageValue != '1')
                {
                    Console.WriteLine("\nVeuillez choisir un sondage valide (0 ou 1, q pour quitter)\n");
                    sondageValue = Console.ReadKey().KeyChar;
                    if(sondageValue == 'q')
                    {
                        Environment.Exit(1);
                    }
                }
                numeroSondage = Convert.ToInt32(sondageValue.ToString());

            }

            Console.WriteLine("\nVous avez choisi le "+sondages[numeroSondage].Id);

            Reponse userReponseQuestion1 = new Reponse() { SondageId = numeroSondage, QuestionId = 1 };
            Reponse userReponseQuestion2 = new Reponse() { SondageId = numeroSondage, QuestionId = 2 };
            Reponse userReponseQuestion3 = new Reponse() { SondageId = numeroSondage, QuestionId = 3 };
            Reponse userReponseQuestion4 = new Reponse() { SondageId = numeroSondage, QuestionId = 4 };

            Console.WriteLine("\n"+sondages[numeroSondage].Questions[0]);
            userReponseQuestion1.Result = ReadConsoleAnswer();

            Console.WriteLine("\n" + sondages[numeroSondage].Questions[1]);

            char value = Console.ReadKey().KeyChar;
            while (value != 'a' && value != 'b' && value != 'c')
            {
                Console.WriteLine("\nVeuillez choisir une reponse valide (a,b,ou c)");
                value = Console.ReadKey().KeyChar;
            }

            userReponseQuestion2.Result = value;

            Console.WriteLine("\n" + sondages[numeroSondage].Questions[2]);          
            userReponseQuestion3.Result = ReadConsoleAnswer();

            Console.WriteLine("\n" + sondages[numeroSondage].Questions[3]);         
            userReponseQuestion4.Result = ReadConsoleAnswer();

            response = client.PostAsJsonAsync(new Uri(reponseUrl), userReponseQuestion1).Result;
            response = client.PostAsJsonAsync(new Uri(reponseUrl), userReponseQuestion2).Result;
            response = client.PostAsJsonAsync(new Uri(reponseUrl), userReponseQuestion3).Result;
            response = client.PostAsJsonAsync(new Uri(reponseUrl), userReponseQuestion4).Result;

            user.CompleteSondageId[numeroSondage] = 1;

            response = client.PutAsJsonAsync(new Uri(usagerUrl+"/"+ user.Id), user).Result;

            Console.WriteLine("\nMerci d'avoir repondu au sondage/n");
        }
    }
}
