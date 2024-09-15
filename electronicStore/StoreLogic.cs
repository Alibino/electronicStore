using electronicStore.models;
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
    public class StoreLogic
    {
        DatabaseService databaseService;
        public StoreLogic()
        {
            databaseService = new DatabaseService();
        }
        public void InitializeWelcome()
        {
            Console.WriteLine("Velkommen ElektroJohn hvad vil du i dag ?");
            Console.WriteLine("1. Lave salg");
            Console.WriteLine("2. Se gennemsnitsomsætning pr. kunde for periode");
            Console.WriteLine("3. Se samlet omsætning for periode");
            Console.WriteLine("Indtast valg");
            VilIDagSwitch(getInput());
        }

        public int getInput()
        {
            int valg = 0;
            while (true)
            {
                try
                {
                    valg = int.Parse(Console.ReadLine());
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("indtastet er ikke et nummer, vælg venligst et nummer");
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
                default: break;
            }
        }

        public void PeriodevalgOmsaetning()
        {
            DateTime from;
            DateTime to;
            Console.WriteLine("indtast fra dato: i format dd/mm/yyyy");
            while (true)
            {
                try
                {
                    from = DateTime.Parse(Console.ReadLine(), new CultureInfo("da-DK"));
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("ikke rigtigt datoformat eller invalid dato, prøv igen");
                }
            }

            Console.WriteLine("indtast til dato: i format dd/mm/yyyy");
            while (true)
            {
                try
                {
                    to = DateTime.Parse(Console.ReadLine(), new CultureInfo("da-DK"));
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("ikke rigtigt datoformat eller invalid dato, prøv igen");
                }
            }

            double value = databaseService.GetOmsaetning(from, to);
            if (value > 0)
            {
                Console.WriteLine($"omsætning i periode {from} - {to} er {value}");
            }
            else
            {
                Console.WriteLine($"der var ingen omsætning i periode {from} - {to}");
            }
        }
        public void PeriodevalgKundeOmsaetning()
        {
            DateTime from;
            DateTime to;
            Console.WriteLine("indtast fra dato: i format dd/mm/yyyy");
            while (true)
            {
                try
                {
                    from = DateTime.Parse(Console.ReadLine(), new CultureInfo("da-DK"));
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("ikke rigtigt datoformat eller invalid dato, prøv igen");
                }
            }

            Console.WriteLine("indtast til dato: i format dd/mm/yyyy");
            while (true)
            {
                try
                {
                    to = DateTime.Parse(Console.ReadLine(), new CultureInfo("da-DK"));
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("ikke rigtigt datoformat eller invalid dato, prøv igen");
                }
            }

            Console.WriteLine("indtast kundenavn eller id");
            string kunde = Console.ReadLine();

            double value = databaseService.GetOmsaetningPrKunde(from, to, kunde);
            if (value > 0)
            {
                Console.WriteLine($"omsætning per kunde i gennemsnit i periode {from} - {to} er {value}");
            }
            else
            {
                Console.WriteLine($"der var ingen omsætning i periode {from} - {to}");
            }
        }

        public void Transaction()
        {
            List<Ware> soldWares = new List<Ware>();
            Customer customer = null;
            while (customer == null)
            {
                Console.WriteLine("nuværende kunde eller ny ?");
                Console.WriteLine("1. nuværende");
                Console.WriteLine("2. ny");
                int valg = getInput();
                switch (valg)
                {
                    case 1: customer = CustomerExists(); break;
                    case 2: customer = CreateCustomer(); break;
                    default: break;
                }
            }
            Console.WriteLine($"Sælger nu til kunde {customer.Name}");
            Console.WriteLine("hvad bliver der solgt ?");
            int n = 1;
            foreach(Ware w in databaseService.GetAllWares())
            {
                Console.WriteLine($"{n}. {w.WareName} pris {w.Price}");
                n++;
            }
            int wareValg = getInput();
            Ware choosen = databaseService.GetAllWares()[wareValg - 1];
            soldWares.Add(choosen);
            Console.WriteLine($"solgt {choosen.WareName} til {customer.Name}");
            //tilføj loop til at sælge flere på en gang, hvis dem til sidst
            databaseService.CreateTransaction(choosen, customer);
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
