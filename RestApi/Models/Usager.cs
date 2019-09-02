using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi.Models
{
    public class Usager
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Mdp { get; set; }
        public int[] CompleteSondageId { get; set; }
    }
}
