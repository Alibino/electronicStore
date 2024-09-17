using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace electronicStore
{
    public interface IStoreLogicGenerator
    {
        IStoreLogic Create();
    }

    public class StoreLogicGenerator : IStoreLogicGenerator
    {
        public IStoreLogic Create()
        {
            return new StoreLogic();
        }
    }
    public class StoreLogicFactory
    {
        public IStoreLogicGenerator generator()
        {
            return new StoreLogicGenerator();
        }
    }
}
