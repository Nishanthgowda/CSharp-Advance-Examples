using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegatesExamples.Events
{
    public class ThermostatEventsApp
    {
        public void Mainrun()
        {
            Console.WriteLine("Press any key to start device...");
            Console.ReadKey();

            IDevice device = new Device();

            device.RunDevice();

            Console.ReadKey();
        }
    }
    public class Device : IDevice
    {
        const double Warning_Level = 27;
        const double Emergency_Level = 75;

        public double WarningTemperatureLevel => Warning_Level;

        public double EmergencyTemperatureLevel => Emergency_Level;

        public void HandleEmergency()
        {
            Console.WriteLine();
            Console.WriteLine("Sending out notifications to emergency services personal...");
            ShutDownDevice();
            Console.WriteLine();
        }

        private void ShutDownDevice()
        {
            Console.WriteLine("Shutting down device...");
        }

        public void RunDevice()
        {
            Console.WriteLine("Device is running...");

            ICoolingMechanism coolingMechanism = new CoolingMechanism();
            IHeat heatSensor = new HeatSensor(Warning_Level, Emergency_Level);
            IThermoStat thermostat = new ThermoStat(this, coolingMechanism,heatSensor);

            thermostat.RunThermoStat();

        }
    }



    public class ThermoStat : IThermoStat
    {
        private IDevice _device = null;
        private ICoolingMechanism _coolingMechanism = null;
        private IHeat _heat = null;

        private const double warningLevelTemprature = 27;
        private const double emegencyLevelTemprature = 75;

        public ThermoStat(IDevice device, ICoolingMechanism coolingMechanism, IHeat heat)
        {
            _coolingMechanism = coolingMechanism;
            _heat = heat;
            _device = device;
        }
        private void WireUpEventsToEvenHandler()
        {
            _heat.TempratureReachesWarninglevelEventHandler += Heat_TempratureReachesWarninglevelEventHandler;
            _heat.TempratureReachesBelowWaningrlevelEventHandler += Heat_TempratureReachesBelowWaningrlevelEventHandler;
            _heat.TempratureReachesEmergencylevelEventHandler += Heat_TempratureReachesEmergencylevelEventHandler;
        }

        private void Heat_TempratureReachesEmergencylevelEventHandler(object? sender, TempratureEventArgs e)
        {

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine();
            Console.WriteLine($"Emergency Alert!! (Emergency level is {_device.EmergencyTemperatureLevel} and above)");
            _device.HandleEmergency();

            Console.ResetColor();
        }

        private void Heat_TempratureReachesBelowWaningrlevelEventHandler(object? sender, TempratureEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine();
            Console.WriteLine($"Information Alert!! Temperature falls below warning level (Warning level is between {_device.WarningTemperatureLevel} and {_device.EmergencyTemperatureLevel})");
            _coolingMechanism.Off();
            Console.ResetColor();
        }

        private void Heat_TempratureReachesWarninglevelEventHandler(object? sender, TempratureEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine();
            Console.WriteLine($"Warning Alert!! (Warning level is between {_device.WarningTemperatureLevel} and {_device.EmergencyTemperatureLevel})");
            _coolingMechanism.On();
            Console.ResetColor();
        }

        public void RunThermoStat()
        {
            Console.WriteLine("ThermoStat is running.....");
            WireUpEventsToEvenHandler();
            _heat.RunHeatSensor();
        }
    }
    public interface IThermoStat
    {
        void RunThermoStat();
    }

    public interface IDevice
    {
        double WarningTemperatureLevel { get; }
        double EmergencyTemperatureLevel { get; }
        void RunDevice();
        void HandleEmergency();
    }

    public interface ICoolingMechanism
    {
        void On();
        void Off();
    }
    public class CoolingMechanism : ICoolingMechanism
    {
        public void Off()
        {
            Console.WriteLine();
            Console.WriteLine("Switching Cooling Mechanism Off...");
            Console.WriteLine();
        }

        public void On()
        {
            Console.WriteLine();
            Console.WriteLine("Switching Cooling Mechanism On...");
            Console.WriteLine();
        }
    }

    public class HeatSensor : IHeat
    {
        double _warningLevel = 0;
        double _emergencyLevel = 0;

        bool _hasReachedWarningTemprature = false;

        // Define the delegate collection.
        protected EventHandlerList listEventDelegates = new EventHandlerList();

        // Define a unique key for each event.
        static readonly object _tempratureReachesEmergencylevelKey = new object();
        static readonly object _tempratureReachesWarninglevelKey = new object();
        static readonly object _tempratureReachesBelowWarninglevelKey = new object();

        public double[] _temperatureData = null;
        


        public HeatSensor(double warningLevel,double emergencyLevel)
        {
            _emergencyLevel = emergencyLevel;
            _warningLevel = warningLevel;
            seedData();
        }

        public void seedData()
        {
            _temperatureData = new double[] { 90.9, 78, 32, 23, 24, 12, 56, 76, 34, 56.3, 18.9, 36.7, 40.2, 25.7 };
        }

       
        event EventHandler<TempratureEventArgs> IHeat.TempratureReachesEmergencylevelEventHandler
        {
            // add and remove accessors are similar to get and set properties.
            // contains a code that is fired when client code subscribed to relevant event.
            add
            {       //subscribe
                listEventDelegates.AddHandler(_tempratureReachesEmergencylevelKey,value);
            }

            // contains a code that is fired when client code unsubscribed to relevant event.
            remove
            {      //unsubscribe
                listEventDelegates.RemoveHandler(_tempratureReachesEmergencylevelKey, value);
            }
        }

        event EventHandler<TempratureEventArgs> IHeat.TempratureReachesWarninglevelEventHandler
        {
            add
            {
                listEventDelegates.AddHandler(_tempratureReachesWarninglevelKey,value);
            }

            remove
            {
                listEventDelegates.RemoveHandler(_tempratureReachesWarninglevelKey, value);
            }
        }

        event EventHandler<TempratureEventArgs> IHeat.TempratureReachesBelowWaningrlevelEventHandler
        {
            add
            {
                listEventDelegates.AddHandler(_tempratureReachesBelowWarninglevelKey,value);
            }

            remove
            {
                listEventDelegates.RemoveHandler(_tempratureReachesBelowWarninglevelKey, value);
            }
        }

        // encapsulates functionalities for raising relevant event by creating 3 methods responsible for raising events

        public void OnTempratureReachesEmergencylevel(TempratureEventArgs e)
        {
            EventHandler<TempratureEventArgs> handler = (EventHandler<TempratureEventArgs>)listEventDelegates[_tempratureReachesEmergencylevelKey];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public void OnTempratureReachesWarninglevel(TempratureEventArgs e)
        {
            EventHandler<TempratureEventArgs> handler = (EventHandler<TempratureEventArgs>)listEventDelegates[_tempratureReachesWarninglevelKey];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public void OnTempratureReachesBelowWarninglevel(TempratureEventArgs e)
        {
            EventHandler<TempratureEventArgs> handler = (EventHandler<TempratureEventArgs>)listEventDelegates[_tempratureReachesBelowWarninglevelKey];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private void MonitorTemprature()
        {
            foreach(double temp in _temperatureData)
            {
                Console.ResetColor();
                Console.WriteLine($"DateTime: {DateTime.Now}, Temperature: {temp}");

                if(temp >= _emergencyLevel)
                {
                    TempratureEventArgs e = new TempratureEventArgs { Temprature = temp,CurrentDateTime= DateTime.Now };
                    OnTempratureReachesEmergencylevel(e);
                }
                else if(temp >= _warningLevel)
                {
                    _hasReachedWarningTemprature = true;
                    TempratureEventArgs e = new TempratureEventArgs { Temprature = temp, CurrentDateTime = DateTime.Now };
                    OnTempratureReachesWarninglevel(e);
                }
                else if(temp< _warningLevel && _hasReachedWarningTemprature)
                {
                    _hasReachedWarningTemprature = false;
                    TempratureEventArgs e = new TempratureEventArgs { Temprature = temp, CurrentDateTime = DateTime.Now };
                    OnTempratureReachesBelowWarninglevel(e);
                }
            }
            System.Threading.Thread.Sleep(3000);
        }


        public void RunHeatSensor()
        {
            Console.WriteLine("Heat Sensor is running.....");
            MonitorTemprature();    
        }
    }

    public interface IHeat
    {
        event EventHandler<TempratureEventArgs> TempratureReachesEmergencylevelEventHandler;
        event EventHandler<TempratureEventArgs> TempratureReachesWarninglevelEventHandler;
        event EventHandler<TempratureEventArgs> TempratureReachesBelowWaningrlevelEventHandler;
        void RunHeatSensor();
    }
    public class TempratureEventArgs:EventArgs
    {
        //to store event information
        public double Temprature { get; set; }    //current temprature at the time event raised
        public DateTime CurrentDateTime { get; set; }   //event occurred time
    }
}
