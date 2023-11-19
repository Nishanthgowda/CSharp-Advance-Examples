using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DelegatesExamples.Delegates
{
    public class FuncActionPredicateExample
    {
        delegate TResult Func2<out TResult>();
        delegate TResult Func2<in T1, out TResult>(T1 args);
        delegate TResult Func2<in T1, in T2, out TResult>(T1 args1, T2 args2);
        delegate TResult Func2<in T1, in T2, in T3, out TResult>(T1 args1, T2 args2, T3 args3);

        public void displayFuncResults()
        {
            MathClass math = new MathClass();

            //  Func<int, int, int> cal = math.Sum;
            // first two types represents parameters types and the last one in Func represents return type of methods

            //Console.WriteLine("Enter a and b: ");
            //int a = Convert.ToInt32(Console.ReadLine());
            //int b = Convert.ToInt32(Console.ReadLine());

            //int c = Convert.ToInt32(Console.ReadLine());
            // calling different ways

            // Func<int, int, int> cal = (a, b) => a + b;
            // Func<int, int, int> cal = delegate (int a, int b) { return a + b; };

            //using generic Func
            // Func2<int, int, int, string> cal = delegate (int a, int b, int c) { return (a + b + c).ToString(); };
            /* var res = cal(a,b);
             Console.WriteLine("The sum: " + res);*/

            Console.WriteLine("Enter salary and bonus: ");
            decimal salary = Convert.ToDecimal(Console.ReadLine());
            decimal bonus = Convert.ToDecimal(Console.ReadLine());

            Func<decimal, decimal, decimal> TotalSalary = (salary, bonus) => salary + salary * bonus / 100;

            Console.WriteLine($"Total Employee salary : {TotalSalary(salary, bonus)}");



        }
        public void displayActionResults()
        {
            /*
                MathClass m = new MathClass();
                Action<int> action = m.SquareRoot;
                action(81); 
              */


            Console.WriteLine("Enter Employee Id,FirstName, LastName, AnnualSalary, Gender and IsManager : ");
            int Id = Convert.ToInt32(Console.ReadLine());
            string Firstname = Console.ReadLine();
            string Lastname = Console.ReadLine();
            decimal AnnualSalary = Convert.ToDecimal(Console.ReadLine());
            char Gender = Convert.ToChar(Console.ReadLine());
            bool IsManager = Convert.ToBoolean(Console.ReadLine());

            Action<int, string, string, decimal, char, bool> displayNoOfEmployeeRecords = (id, firstName, lastName, annualSalary, gender, manager) =>
            {
                Console.WriteLine($"Id: {id}\nFirstName: {firstName}\nLastName: {lastName}\nAnnual Salary: {annualSalary}\nGender: {gender}\nIs he/she Manager(ture/false): {manager}");
            };
            Console.WriteLine("\nEmployee Details : ");
            displayNoOfEmployeeRecords(Id, Firstname, Lastname, AnnualSalary, Gender, IsManager);
        }

        public void displayPredicateResults()
        {
            //predicate can be used as filter which return true or false value
            List<Employee> employees = new List<Employee>();

            employees.Add(new Employee { Id = 1, Firstname = "Nishanth", Lastname = "Gowda", AnnualSalary = 4000000, Gender = 'M', IsManager = true });
            employees.Add(new Employee { Id = 2, Firstname = "Naveen", Lastname = "Kumar", AnnualSalary = 7000000, Gender = 'M', IsManager = true });
            employees.Add(new Employee { Id = 3, Firstname = "White", Lastname = "Yaksha", AnnualSalary = 4000000, Gender = 'M', IsManager = false });
            employees.Add(new Employee { Id = 4, Firstname = "Hinata", Lastname = "Hyuga", AnnualSalary = 3500000, Gender = 'F', IsManager = true });

            //filter examples
            //    List<Employee> fltrResult = filteredEmployees(employees, e => e.IsManager == true);
            // List<Employee> fltrResult = filteredEmployees(employees, e => e.Gender == 'F');
            //List<Employee> fltrResult = filteredEmployees(employees, e => e.AnnualSalary <= 4000000);

            //Extension Method
            //List<Employee> fltrResult = employees.filteredEmployees(e => e.AnnualSalary <= 4000000);

            //Linq

            List<Employee> fltrResult = employees.Where(e => e.AnnualSalary <= 4000000).ToList();

            foreach (Employee employee in fltrResult)
            {
                Console.WriteLine($"\nId: {employee.Id}\nFirstName: {employee.Firstname}\nLastName: {employee.Lastname}\nAnnual Salary: {employee.AnnualSalary}\nGender: {employee.Gender}\nIs he/she Manager(ture/false): {employee.IsManager}");
            }

        }
        /* public static List<Employee> filteredEmployees(List<Employee> employees, Predicate<Employee> predicate)
         {
             List<Employee> filteredEmp = new List<Employee>();
             foreach (Employee employee in employees)
             {
                 if (predicate(employee))
                 {
                     filteredEmp.Add(employee);
                 }
             }
             return filteredEmp;
         }*/

    }
    public static class extension
    {
        //creating extenxion method
        public static List<Employee> filteredEmployees(this List<Employee> employees, Predicate<Employee> predicate)
        {
            List<Employee> filteredEmp = new List<Employee>();
            foreach (Employee employee in employees)
            {
                if (predicate(employee))
                {
                    filteredEmp.Add(employee);
                }
            }
            return filteredEmp;
        }
    }
    public class Employee
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public decimal AnnualSalary { get; set; }
        public char Gender { get; set; }
        public bool IsManager { get; set; }
    }
    public class MathClass
    {
        public int Sum(int a, int b)
        {
            return a + b;
        }
        public void SquareRoot(int num)
        {
            Console.WriteLine(Math.Sqrt(num).ToString());
        }
    }
}
