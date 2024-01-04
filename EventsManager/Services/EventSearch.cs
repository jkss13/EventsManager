using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using EventsManager.Models;

namespace EventsManager.Services
{
    public class EventSearch
    {
    
        private readonly EventRegistration eventRegistration;

        public EventSearch(EventRegistration eventRegistration)
        {
            this.eventRegistration = eventRegistration;
        }

        public void FindEvent() {
            Console.WriteLine("** LOCALIZAR EVENTOS **");

            string category = string.Empty;
            string location;
            DateTime date;

            while (string.IsNullOrEmpty(category))
            {
                Console.Write("Categoria (deixe em branco para ignorar): ");
                category = Console.ReadLine() ?? "";
            }

            Console.Write("Localidade (deixe em branco para ignorar): ");
            location = Console.ReadLine() ?? "";

            Console.Write("Data (deixe em branco para ignorar): ");
            string dateInput = Console.ReadLine() ?? "";
            DateTime.TryParseExact(dateInput, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date);

            List<Event> filteredEvents = FilterEvents(category, location, date);

            if (filteredEvents.Count > 0)
            {
                Console.WriteLine("\nEventos encontrados:");
                foreach (var evt in filteredEvents)
                {
                    Console.WriteLine($"Categoria: {evt.Category}, Título: {evt.EventTitle}, Local: {evt.EventsPlace}, Data: {evt.Date.ToShortDateString()}");
                }
            }
            else
            {
                Console.WriteLine("\nNenhum evento encontrado com os critérios selecionados.");
            }
        
        }
            private List<Event> FilterEvents(string category, string location, DateTime date)
        {
            List<Event> allEvents = eventRegistration.GetEvents();
            List<Event> filteredEvents = new List<Event>();

            foreach (var evt in allEvents)
            {

                bool matchesCategory = string.IsNullOrEmpty(category) || evt.Category.Equals(category, StringComparison.OrdinalIgnoreCase);
                bool matchesLocation = string.IsNullOrEmpty(location) || evt.EventsPlace.Equals(location, StringComparison.OrdinalIgnoreCase);
                bool matchesDate = date == DateTime.MinValue || evt.Date.Date == date.Date;

                if (matchesCategory && matchesLocation && matchesDate)
                {
                    filteredEvents.Add(evt);
                }
            }

            return filteredEvents;
        }
    }

}