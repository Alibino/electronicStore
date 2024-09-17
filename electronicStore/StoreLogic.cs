using electronicStore.models;
using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Gaming.Input.ForceFeedback;
using Windows.UI.Xaml.Documents;

namespace electronicStore
{
    public class StoreLogic : IStoreLogic
    {
        private Boolean exit = false;
        IDatabaseService databaseService;
        IStoreView storeView;
        public StoreLogic()
        {
            DatabaseServiceFactory dbFactory = new DatabaseServiceFactory();
            StoreViewFactory storeViewFactory = new StoreViewFactory();
            databaseService = dbFactory.generator().Create();
            storeView = storeViewFactory.generator().Create();
        }
        public void InitializeWelcome()
        {
            while (!exit)
            {
                storeView.Welcome();
                VilIDagSwitch(getInput(4));
            }
        }

        //dette ville ikke være i produktion men for test grunde ligger vi det her
        public void createBaseWares()
        {
            databaseService.DeleteAllWares();
            databaseService.CreateWare("iPhone", 9999);
            databaseService.CreateWare("iPad", 4999);
            databaseService.CreateWare("Oplader", 149);
        }

        public int getInput(int numberOfChoices)
        {
            int valg = 0;
            while (true)
            {
                try
                {
                    valg = int.Parse(Console.ReadLine());
                    if(valg > numberOfChoices)
                    {
                        throw new Exception();
                    }
                    break;
                }
                catch (Exception)
                {
                    storeView.ErrorNumber(numberOfChoices);
                }
            }
            return valg;

        }

        public void VilIDagSwitch(int choice)
        {
            switch (choice)
            {
                case 1: Transaction(); break;
                case 2: PeriodevalgKundeOmsaetning(); break;
                case 3: PeriodevalgOmsaetning(); break;
                case 4: exit = true; break;
                default: break;
            }
        }

        public void PeriodevalgOmsaetning()
        {
            DateTime from;
            DateTime to;
            storeView.FraDato();
            while (true)
            {
                try
                {
                    from = DateTime.Parse(Console.ReadLine(), new CultureInfo("da-DK"));
                    break;
                }
                catch (Exception)
                {
                    storeView.ErrorDato();
                }
            }

            storeView.TilDato();
            while (true)
            {
                try
                {
                    to = DateTime.Parse(Console.ReadLine(), new CultureInfo("da-DK")).AddHours(24);
                    break;
                }
                catch (Exception)
                {
                    storeView.ErrorDato();
                }
            }

            double value = databaseService.GetOmsaetning(from, to);
            if (value > 0)
            {
                storeView.omsaetningInPeriod(from, to, value);
            }
            else
            {
                storeView.NoOmsaetningMessage(from, to);
            }
        }
        public void PeriodevalgKundeOmsaetning()
        {
            DateTime from;
            DateTime to;
            storeView.FraDato();
            while (true)
            {
                try
                {
                    from = DateTime.Parse(Console.ReadLine(), new CultureInfo("da-DK"));
                    break;
                }
                catch (Exception)
                {
                    storeView.ErrorDato();
                }
            }

            storeView.TilDato();
            while (true)
            {
                try
                {
                    to = DateTime.Parse(Console.ReadLine(), new CultureInfo("da-DK")).AddHours(24);
                    break;
                }
                catch (Exception)
                {
                    storeView.ErrorDato();
                }
            }

            storeView.EnterCustomer();
            string kunde = Console.ReadLine();

            double value = databaseService.GetOmsaetningPrKunde(from, to, kunde);
            if (value > 0)
            {
                storeView.omsaetningInPeriodCustomer(from, to, value);
            }
            else
            {
                storeView.NoOmsaetningMessage(from, to);
            }
        }

        public void Transaction()
        {
            List<Ware> soldWares = new List<Ware>();
            Customer customer = null;
            while (customer == null)
            {
                storeView.CurrentOrNewCustomerMessage();
                int valg = getInput(2);
                switch (valg)
                {
                    case 1: customer = CustomerExists(); break;
                    case 2: customer = CreateCustomer(); break;
                    default: break;
                }
            }
            storeView.SellingToCustomer(customer.Name);
            Boolean buying = true;
            while (buying)
            {
                List<Ware> wares = databaseService.GetAllWares();

                storeView.ShowWares(wares);

                int wareValg = getInput(wares.Count);

                Ware choosen = databaseService.GetAllWares()[wareValg - 1];

                soldWares.Add(choosen);

                storeView.sold(customer.Name, choosen.WareName);

                double sum = soldWares.Sum(w => w.Price);

                storeView.OverViewAndEndMessage(sum, soldWares);
                switch (getInput(2))
                {
                    case 1: break;
                    case 2: buying = false; break;
                    default: break;
                }
            }
            SellingWares(soldWares, customer);
            //tilføj loop til at sælge flere på en gang, hvis dem til sidst
        }

        public void SellingWares(List<Ware> soldWares, Customer customer)
        {
            string transId = Guid.NewGuid().ToString();
            foreach (Ware w in soldWares)
            {
                databaseService.CreateTransaction(w, customer, transId);
            }
        }

        public Customer CreateCustomer()
        {
            Console.WriteLine("indtast navn til ny kunde");
            string navn = Console.ReadLine();
            return databaseService.CreateCustomer(navn);

        }

        public Customer CustomerExists()
        {
            Console.WriteLine("indtast kundens navn");
            string navn = Console.ReadLine();
            Customer customer = databaseService.QueryCustomer(navn);
            if (customer == null)
            {
                Console.WriteLine($"kunden ved navn {navn} kunne ikke findes, returnere til tidligere valg");
                return null;
            }

            return customer;

        }
    }
}
