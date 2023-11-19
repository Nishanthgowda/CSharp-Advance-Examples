using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegatesExamples.Events
{
    public class EventHandlingExampleWithNoData
    {

        public void performEventMain()
        {
            Counter c = new Counter(new Random().Next(10));
            c.handleThresholdEvent += c_EventHandler;  //subscribe the event
            Console.WriteLine("press a key to incease count");
            while (Console.ReadKey(true).KeyChar == 'a')
            {
                Console.WriteLine("adding 1");
                c.add(1);

            }
            
        }
        public static void c_EventHandler(object sender, EventArgs e)
        {
            Console.WriteLine("Threshould was Reached");
            Environment.Exit(0);
        }
    }
    public class Counter
    {
        private int total;
        private int threshold;

        public Counter(int Threshold) {
            this.threshold = Threshold;
        }

        public void add(int val)
        {
            total += val;
            if(total > threshold)
            {
                handleThresholdEvent.Invoke(this,EventArgs.Empty); //event raised
            }
        }

        public event EventHandler handleThresholdEvent;
    }
}
