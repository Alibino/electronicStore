using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace electronicStore.models
{
    public class Ware
    {
        public int Id { get; set; }
        public required string WareName { get; set; }
        public double Price { get; set; }
    }
}
