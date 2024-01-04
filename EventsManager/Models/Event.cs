using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SkiaSharp;

namespace EventsManager.Models
{
    public class Event
    {
        public string EventTitle { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string EventsPlace { get; set; }
        public string EventsDescription { get; set; }
        public SKBitmap EventCover { get; set; }


        public Event(string eventTitle, string category, DateTime date, TimeSpan time, string eventsPlace, string eventsDescription, SKBitmap eventCover)
        {
            EventTitle = eventTitle;
            Category = category;
            Date = date;
            Time = time;
            EventsPlace = eventsPlace;
            EventsDescription = eventsDescription;
            EventCover = eventCover;
        }

    }
}