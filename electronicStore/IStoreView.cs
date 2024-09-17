using electronicStore.models;

namespace electronicStore
{
    public interface IStoreView
    {
        void CurrentOrNewCustomerMessage();
        void EnterCustomer();
        void ErrorDato();
        void ErrorNumber(int numberOfChoices);
        void FraDato();
        void ListOfSoldWares(List<Ware> wares);
        void NoOmsaetningMessage(DateTime from, DateTime to);
        void omsaetningInPeriod(DateTime from, DateTime to, double sum);
        void omsaetningInPeriodCustomer(DateTime from, DateTime to, double sum);
        void OverViewAndEndMessage(double sum, List<Ware> wares);
        void SellingToCustomer(string name);
        void ShowWares(List<Ware> wares);
        void sold(string name, string wareName);
        void TilDato();
        void Welcome();
    }
}