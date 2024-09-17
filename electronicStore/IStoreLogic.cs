using electronicStore.models;

namespace electronicStore
{
    public interface IStoreLogic
    {
        void createBaseWares();
        Customer CreateCustomer();
        Customer CustomerExists();
        int getInput(int numberOfChoices);

        void SellingWares(List<Ware> soldWares, Customer customer);
        void InitializeWelcome();
        void PeriodevalgKundeOmsaetning();
        void PeriodevalgOmsaetning();
        void Transaction();
        void VilIDagSwitch(int choice);
    }
}