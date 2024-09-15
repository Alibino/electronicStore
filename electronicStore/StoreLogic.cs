using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                case 1: Console.WriteLine("1"); break;
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
            Console.WriteLine("nuværende kunde eller ny ?");
            Console.WriteLine("1. nuværende");
            Console.WriteLine("2. ny");
            int valg = getInput();
            switch (valg)
            {
                case 1: CustomerExists(); break;
                case 2: CreateCustomer(); break;
                default: break;
            }
        }
    }
}
