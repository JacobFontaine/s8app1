using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi.Models
{
    public class Reponse
    {
        public int SondageId { get; set; }
        public int QuestionId { get; set; }
        public char Result { get; set; }

        private List<Reponse> reponses = new List<Reponse>();

        public Reponse()
        {
        }

        public IEnumerable<Reponse> GetAll()
        {
            return reponses;
        }

        public bool Add(Reponse reponse)
        {
            if (reponse == null)
            {
                return false;
            }
            reponses.Add(reponse);
            return true;
        }

    }
}
