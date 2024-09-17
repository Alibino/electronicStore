using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace electronicStore
{
    public interface IStoreViewGenerator
    {
        IStoreView Create();
    }

    public class StoreViewGenerator : IStoreViewGenerator
    {
        public IStoreView Create()
        {
            return new StoreView();
        }
    }
    public class StoreViewFactory
    {
        public IStoreViewGenerator generator()
        {
            return new StoreViewGenerator();
        }
    }
}
