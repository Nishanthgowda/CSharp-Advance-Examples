using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegatesExamples.Events
{
    public class MultiEventsCall
    {
        static EmplyoeeEvent eEvent = new EmplyoeeEvent(5);
        IEmployeeActions eactions = new EmployeeActions(eEvent);
       public void displayMultiEventsCallResut()
        {
            eactions.subscribeEvents();
            eactions.readEmployeeRecords();
        }

    }

    public class EmployeeRecords
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }
    }
    public class EmplyoeeEvent : IEmployeeActionsAndMultiEventDelegats
    {
        public int counter = 0;

        public EventHandlerList eventHandlerList = new EventHandlerList();

        public object _writeOnConsoleEvent = new object();
        public object _writeOnFileEvent = new object();

        public EmplyoeeEvent(int count)
        {
                this.counter = count;
        }

        event EventHandler<EmplyoeeNoOf> IEmployeeActionsAndMultiEventDelegats.WriteOnConsoleEvent
        {
            add
            {
                eventHandlerList.AddHandler(_writeOnConsoleEvent, value);
            }

            remove
            {
                eventHandlerList.RemoveHandler(_writeOnConsoleEvent, value);
            }
        }

        event EventHandler<EmplyoeeNoOf> IEmployeeActionsAndMultiEventDelegats.WriteOnFileEvent
        {
            add
            {
                eventHandlerList.AddHandler(_writeOnFileEvent, value);
            }

            remove
            {
                eventHandlerList.RemoveHandler(_writeOnFileEvent, value);
            }
        }
        public void OnReachedConsoleLimit(EmplyoeeNoOf enoOf)
        {
            EventHandler<EmplyoeeNoOf> eventHandler = (EventHandler<EmplyoeeNoOf>)eventHandlerList[_writeOnConsoleEvent];
            if(eventHandler != null)
            {
                eventHandler.Invoke(this, enoOf);
            }
        }
        public void OnReachedFileLimit(EmplyoeeNoOf enoOf)
        {
            EventHandler<EmplyoeeNoOf> eventHandler = (EventHandler<EmplyoeeNoOf>)eventHandlerList[_writeOnFileEvent];
            if (eventHandler != null)
            {
                eventHandler.Invoke(this, enoOf);
            }
        }

        public void Condition(List<EmployeeRecords> emp) {
            if (emp.Count > counter)
            {
                EmplyoeeNoOf enoOf = new EmplyoeeNoOf();
                enoOf.Count = emp.Count;
                OnReachedFileLimit(enoOf);
            }
            else
            {
                EmplyoeeNoOf enoOf = new EmplyoeeNoOf();
                enoOf.Count = emp.Count;
                OnReachedConsoleLimit(enoOf);
            }
        }

    }
    public class EmployeeActions : IEmployeeActions
    {
        public IEmployeeActionsAndMultiEventDelegats _employeeEvents = null;
        List<EmployeeRecords> employeeRecords = new List<EmployeeRecords>();
        public EmployeeActions(EmplyoeeEvent emplyoeeEvents)
        {
            this._employeeEvents = emplyoeeEvents;
        }
        public void subscribeEvents()
        {
            _employeeEvents.WriteOnConsoleEvent += EmployeeEvents_WriteOnConsoleEvent;
            _employeeEvents.WriteOnFileEvent += EmployeeEvents_WriteOnFileEvent;
        }

        private void EmployeeEvents_WriteOnFileEvent(object? sender, EmplyoeeNoOf e)
            {
            using (StreamWriter file = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "pqr.txt"), true))
            {
                foreach(var employee in employeeRecords)
                {
                    file.WriteLine(employee.ToString());
                }
               
            }
        }

        private void EmployeeEvents_WriteOnConsoleEvent(object? sender, EmplyoeeNoOf e)
        {
            displayEmployeesRecords();
        }

        public void displayEmployeesRecords()
        {
            Console.WriteLine("- Emplyee Details are -");
            foreach(var employee in employeeRecords)
            {
                Console.WriteLine();
                Console.WriteLine($"Employee ID: {employee.Id}");
                Console.WriteLine($"Employee ID: {employee.Name}");
                Console.WriteLine($"Employee ID: {employee.Salary}");
            }
        }

        public void readEmployeeRecords()
        {

                Console.WriteLine("Do you want enter employee record : (Y/N) ");
                while (Console.ReadKey().KeyChar == 'y' || Console.ReadKey().KeyChar == 'Y')
                {
                Console.WriteLine();
                Console.Write("Enter Id: ");
                    int id = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter Name: ");
                    string name = Console.ReadLine();
                    Console.Write("Enter Salary: ");
                    decimal salary = Convert.ToDecimal(Console.ReadLine());

                    employeeRecords.Add(new EmployeeRecords { Id = id, Name = name, Salary = salary });
                    Console.WriteLine("--------------------------------------------------------------");
                }           
            _employeeEvents.Condition(employeeRecords);
        }
       
    }
    public interface IEmployeeActions
    {
        void readEmployeeRecords();
        void displayEmployeesRecords();
        void subscribeEvents();
    }
    public interface IEmployeeActionsAndMultiEventDelegats
    {
        public event EventHandler<EmplyoeeNoOf> WriteOnConsoleEvent;
        event EventHandler<EmplyoeeNoOf> WriteOnFileEvent;
        void Condition(List<EmployeeRecords> employeeRecords);
    }
    public class EmplyoeeNoOf : EventArgs
    {
        public int Count { get; set; }
        
    }
}
