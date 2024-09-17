using electronicStore.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace electronicStore
{
    public class StoreView : IStoreView
    {
        public void Welcome()
        {
            Console.WriteLine("Velkommen ElektroJohn hvad vil du i dag ?");
            Console.WriteLine("1. Lave salg");
            Console.WriteLine("2. Se gennemsnitsomsætning pr. kunde for periode");
            Console.WriteLine("3. Se samlet omsætning for periode");
            Console.WriteLine("4. færdig for i dag");
            Console.WriteLine("Indtast valg");
        }

        public void ErrorNumber(int numberOfChoices)
        {
            Console.WriteLine($"indtastet er ikke et nummer eller over {numberOfChoices}, vælg venligst et gyldigt nummer");
        }

        public void ErrorDato()
        {
            Console.WriteLine("ikke rigtigt datoformat eller invalid dato, prøv igen");
        }

        public void TilDato()
        {

            Console.WriteLine("indtast til dato: i format dd/mm/yyyy");
        }

        public void FraDato()
        {

            Console.WriteLine("indtast fra dato: i format dd/mm/yyyy");
        }

        public void omsaetningInPeriod(DateTime from, DateTime to, double sum)
        {
            Console.WriteLine($"omsætning i periode {from} - {to} er {sum}");
        }

        public void omsaetningInPeriodCustomer(DateTime from, DateTime to, double sum)
        {
            Console.WriteLine($"omsætning per kunde i gennemsnit i periode {from} - {to} er {sum}");
        }

        public void NoOmsaetningMessage(DateTime from, DateTime to)
        {
            Console.WriteLine($"der var ingen omsætning i periode {from} - {to}");
        }

        public void EnterCustomer()
        {
            Console.WriteLine("indtast kundenavn eller id");
        }

        public void CurrentOrNewCustomerMessage()
        {
            Console.WriteLine("nuværende kunde eller ny ?");
            Console.WriteLine("1. nuværende");
            Console.WriteLine("2. ny");
        }

        public void SellingToCustomer(String name)
        {
            Console.WriteLine($"Sælger nu til kunde: {name}");
            Console.WriteLine("hvad bliver der solgt ?");
        }

        public void ShowWares(List<Ware> wares)
        {
            int n = 1;
            foreach (Ware w in wares)
            {
                Console.WriteLine($"{n}. {w.WareName} pris {w.Price}");
                n++;
            }
        }

        public void sold(String name, String wareName)
        {
            Console.WriteLine($"solgt {wareName} til {name}");
        }

        public void OverViewAndEndMessage(double sum, List<Ware> wares)
        {
            Console.WriteLine("nuværende kurv:");
            ListOfSoldWares(wares);
            Console.WriteLine($"total sum: {sum} kr.");
            Console.WriteLine("flere vare i dette indkøb ?");
            Console.WriteLine("1. ja");
            Console.WriteLine("2. nej");
        }

        public void ListOfSoldWares(List<Ware> wares)
        {
            foreach (Ware w in wares)
            {
                Console.WriteLine($"{w.WareName} {w.Price}");
            }
        }
    }
}
