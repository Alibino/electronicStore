using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace electronicStore
{
    public interface IDatabaseServiceGenerator
    {
        IDatabaseService Create();
    }

    public class DatabaseServiceGenerator : IDatabaseServiceGenerator
    {
        public IDatabaseService Create()
        {
            return new DatabaseService();
        }
    }
    public class DatabaseServiceFactory
    {
        public IDatabaseServiceGenerator generator()
        {
            return new DatabaseServiceGenerator();
        }
    }
}
