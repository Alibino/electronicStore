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
        public DateTime Created { get; set; }
        public required string TransactionUUID { get; set; }
        public int CustomerId { get; set; }
        public required Customer Customer { get; set; }
        public int WareId { get; set; }
        public required Ware Ware { get; set; }
    }
}
