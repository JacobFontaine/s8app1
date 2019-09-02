using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi.Models
{
    public class UsagerRest : IUsager
    {
        private List<Usager> usagers = new List<Usager>();

        public UsagerRest()
        {
           int[] completeSondageInit = { 0, 0 };
           Add(new Usager { Id = 0, UserName = "Jacob", Mdp = "mdp1", CompleteSondageId = completeSondageInit});
           Add(new Usager { Id = 1, UserName = "Marie", Mdp = "mdp2", CompleteSondageId = completeSondageInit});
        }
        public IEnumerable<Usager> GetAll()
        {
            return usagers;
        }

        public Usager Get(int id)
        {
            return usagers.Find(p => p.Id == id);
        }

        public Usager Add(Usager user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            usagers.Add(user);
            return user;
        }

        public bool Update(Usager user)
        {
            if (user == null)
            {
                return false;
            }
            int index = usagers.FindIndex(p => p.Id == user.Id);
            if (index == -1)
            {
                return false;
            }
            usagers.RemoveAt(index);
            usagers.Add(user);
            return true;
        }
    }
}
