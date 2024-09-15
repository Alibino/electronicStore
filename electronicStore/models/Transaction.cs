using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace electronicStore.models
{
    public class Transaction
    {
        public int Id { get; set; }
        public required DateTime Created { get; set; }
        public required string TransactionUUID { get; set; }
        public required int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public required int WareId { get; set; }
        public Ware Ware { get; set; }
    }
}
