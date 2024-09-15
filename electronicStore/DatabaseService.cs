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

        public void CreateCustomer(string name)
        {
            using (_dbContext)
            {
                _dbContext.Add(new Customer(name));
                _dbContext.SaveChanges();
            }
        }

        public Customer QueryCustomer(string name)
        {
            using (_dbContext)
            {
               return _dbContext.Customers.FirstOrDefault(c => c.Name.Contains(name));
            }
        }

        public Customer QueryCustomer(int id)
        {
            using (_dbContext)
            {
                return _dbContext.Customers.FirstOrDefault(c => c.Id == id);
            }
        }

        public List<Ware> GetAllWares()
        {
            using (_dbContext)
            {
                return [.. _dbContext.Wares];
            }
        }

        public double GetOmsaetning(DateTime from, DateTime to)
        {
            using (_dbContext)
            {
                List<Transaction> trans = [.. _dbContext.Transactions.Where(t => from <= t.Created && t.Created <= to).Include(t => t.Ware)];

                return trans.Count > 0 ? (trans.Sum(t => t.Ware.Price)) : 0;
            }
        }

        public double GetOmsaetningPrKunde(DateTime from, DateTime to, string kunde)
        {
            bool containsInt = kunde.Any(char.IsDigit);
            List<Transaction> trans;

            using (_dbContext)
            {
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
        }
    }
}
