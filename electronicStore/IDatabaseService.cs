using electronicStore.models;

namespace electronicStore
{
    public interface IDatabaseService
    {
        Customer CreateCustomer(string name);
        string CreateTransaction(Ware ware, Customer customer, string uuid = "");
        void CreateWare(string navn, double pris);
        List<Ware> GetAllWares();
        void DeleteAllWares();
        double GetOmsaetning(DateTime from, DateTime to);
        double GetOmsaetningPrKunde(DateTime from, DateTime to, string kunde);
        Customer QueryCustomer(int id);
        Customer QueryCustomer(string name);
    }
}