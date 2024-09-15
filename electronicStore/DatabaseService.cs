using electronicStore.models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace electronicStore
{
    public class DatabaseService
    {
        private StoreContext _dbContext;
        public DatabaseService()
        {
            _dbContext = new StoreContext();
            _dbContext.Database.EnsureCreated();
        }

        public Customer CreateCustomer(string name)
        {
            Customer customer = new Customer(name);
                _dbContext.Add(customer);
                _dbContext.SaveChanges();
            return customer;
            
        }

        public Customer QueryCustomer(string name)
        {
            return _dbContext.Customers.FirstOrDefault(c => c.Name.Contains(name));
        }

        public Customer QueryCustomer(int id)
        {
                return _dbContext.Customers.FirstOrDefault(c => c.Id == id);
        }

        public List<Ware> GetAllWares()
        {
                return [.. _dbContext.Wares];
        }

        public double GetOmsaetning(DateTime from, DateTime to)
        {
                List<Transaction> trans = [.. _dbContext.Transactions.Where(t => from <= t.Created && t.Created <= to).Include(t => t.Ware)];

                return trans.Count > 0 ? (trans.Sum(t => t.Ware.Price)) : 0;
        }

        public double GetOmsaetningPrKunde(DateTime from, DateTime to, string kunde)
        {
            bool containsInt = kunde.Any(char.IsDigit);
            List<Transaction> trans;

                if (containsInt)
                {
                    trans = [.. _dbContext.Transactions.Where(t => from <= t.Created && t.Created <= to && t.CustomerId == int.Parse(kunde)).Include(t => t.Ware)];
                }
                else
                {
                    trans = [.. _dbContext.Transactions.Where(t => from <= t.Created && t.Created <= to && t.Customer.Name.Contains(kunde)).Include(t => t.Ware)];
                }


                return trans.Count > 0 ? (trans.Sum(t => t.Ware.Price)) / trans.Count : 0;
        }

        public string CreateTransaction(Ware ware, Customer customer, string uuid = "")
        {
            uuid = uuid == "" ? Guid.NewGuid().ToString() : uuid;
            Transaction transaction = new()
            {
                CustomerId = customer.Id,
                WareId = ware.Id,
                TransactionUUID = uuid,
                Created = DateTime.Now
            };
            _dbContext.Transactions.Add(transaction);
            return transaction.TransactionUUID;
        }
    }
}
