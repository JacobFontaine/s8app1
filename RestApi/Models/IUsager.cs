using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi.Models
{
    public interface IUsager
    {
        IEnumerable<Usager> GetAll();
        Usager Get(int id);
        Usager Add(Usager user);
        bool Update(Usager user);
    }
}
