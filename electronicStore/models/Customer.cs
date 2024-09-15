using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace electronicStore.models
{
    public class Customer
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        [SetsRequiredMembers]
        public Customer(string name) 
        {
            this.Name = name;
        }
    }
}
