using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DelegatesExamples.Events
{
    public class EventHandlingExampleWithData
    {
        public void displayMainEvenHandlingWithData()
        {
            Counter2 counter2 = new Counter2(new Random().Next(10));
            counter2.counterReachedThreshold += c_EventhandlerWithData;
            Console.WriteLine("Enter \'a\' to increase counter");
            while(Console.ReadKey(true).KeyChar == 'a')
            {
                Console.WriteLine("Adding one");
                counter2.Add2(1);
            }
        }
        public static void c_EventhandlerWithData(object sender, EventHandlerData e)
        {
            Console.WriteLine($"The threshold: {e.Threshold} at the time: {e.TimeReached}");
            Environment.Exit(0);
        }
    }
    public class Counter2
    {
        private int total;
        private int threshold;

        public Counter2(int Threshold)
        {
                this.threshold = Threshold;
        }

        public void Add2(int val2)
        {
            total += val2;
            if(total > threshold)
            {
                //               counterReachedThreshold(this,new EventHandlerData { Threshold = threshold,TimeReached= DateTime.Now });
                //     or
                EventHandlerData eventHandlerData = new EventHandlerData();
                eventHandlerData.Threshold = threshold;
                eventHandlerData.TimeReached = DateTime.Now;
                OnThresholdReached(eventHandlerData);       //event is raised
            }
        }

        public virtual void OnThresholdReached(EventHandlerData e)
        {
            EventHandler<EventHandlerData> OnCounterThresholdReached = counterReachedThreshold;
            if(OnCounterThresholdReached != null)
            {
                OnCounterThresholdReached.Invoke(this, e);      //this represents the current object
            }
        }

        public event EventHandler<EventHandlerData> counterReachedThreshold;
    }

    public class EventHandlerData : EventArgs 
    {
        public int Threshold { get; set; }

        public DateTime TimeReached { get; set; }
    }
}
