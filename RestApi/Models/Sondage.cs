using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi.Models
{
    public class Sondage
    {
        public string Id { get; set; }
        public string[] Questions { get; set; }
    }
}
