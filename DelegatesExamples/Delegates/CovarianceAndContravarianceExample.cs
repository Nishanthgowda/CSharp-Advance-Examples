using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegatesExamples.Delegates
{
    //covariance
    delegate Car Carfactorydel(int id, string name);

    //contravariance

    delegate void LogICECarDetails(Car car);
    delegate void LogEVCarDetails(Car car);

    public class CovarianceAndContravarianceExample
    {

        public void displayContravarianceExample()
        {

            LogICECarDetails logICECarDetails = CarLog;
            LogEVCarDetails logEVCarDetails = CarLog;

            logICECarDetails(new EVcar { Id = 1, Name = "Audi R8" });
            logEVCarDetails(new ICEcar { Id = 2, Name = "Tesla Model-3" });


        }
        public static void CarLog(Car car)
        {
            if (car is ICEcar)
            {
                using (StreamWriter sw = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "abc.txt"), true))
                {
                    sw.WriteLine($"Object Type: {car.GetType()}");
                    sw.WriteLine(car.GetcarDetails());
                }
            }
            else if (car is EVcar)
            {
                Console.WriteLine($"Object Type: {car.GetType()}");
                Console.WriteLine(car.GetcarDetails());
            }
            else
            {
                throw new ArgumentException("Invalid Object Details");
            }
        }
        public void displayCovarianceExample()
        {
            Carfactorydel carfactorydel = CarFactory.ReturnICECar;

            Car iceCar = carfactorydel(1, "Audi R8");

            Console.WriteLine($"Object Type: {iceCar.GetType()}");
            Console.WriteLine(iceCar.GetcarDetails());

            Console.WriteLine();

            Car evCar = carfactorydel(2, "Tesla Model 3");

            Console.WriteLine($"Object Type: {evCar.GetType()}");
            Console.WriteLine(evCar.GetcarDetails());

        }
    }
    public abstract class Car
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual string GetcarDetails()
        {
            return $"Car Id : {Id} \nCar Name: {Name}";
        }
    }

    public static class CarFactory
    {
        public static ICEcar ReturnICECar(int id, string name)
        {
            return new ICEcar { Id = id, Name = name };
        }
        public static EVcar ReturnEVCar(int id, string name)
        {
            return new EVcar { Id = id, Name = name };
        }
    }
    public class ICEcar : Car
    {
        public override string GetcarDetails()
        {
            return $"{base.GetcarDetails()} -> Internal Cumbustion Engine";
        }
    }
    public class EVcar : Car
    {
        public override string GetcarDetails()
        {
            return $"{base.GetcarDetails()} -> Electric";
        }
    }
}
