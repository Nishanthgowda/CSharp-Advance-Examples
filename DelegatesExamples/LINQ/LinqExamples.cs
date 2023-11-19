using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DelegatesExamples.LINQ
{
    public class LinqExamples
    {
        public static void LINQOperations()
        {
            List<Employee> employees = GetEmployees();
            List<Department> departments = GetDepartments();

            //Method Syntax
            //var result = employees.Where(e=>e.IsManager == true);
            //foreach (var employee in result)
            //{
            //    Console.WriteLine($"FullName-> {employee.FirstName + " " + employee.LastName} AnnualSalary-> {employee.AnnualSalary}");
            //}

            //var result = employees.Join(departments, 
            //    emp => emp.DepartmentId, 
            //    dep => dep.Id, 
            //    (e,d) => new { name  = e.FirstName + " "+e.LastName, annualSalary = e.AnnualSalary , department = d.ShortName, departmentDes = d.LongName,departmentId = d.Id  
            //    }).OrderBy(o => o.departmentId).ThenByDescending(o => o.annualSalary);


            //foreach (var employee in result)
            //{
            //    Console.WriteLine($"Name: {employee.name}\nAnnualSalary: {employee.annualSalary}\nDepartment: {employee.department}\nDepartment Description: {employee.departmentDes}\nDepartment ID: {employee.departmentId}");
            //    Console.WriteLine();
            //}

            //Query Syntax

            //var result = from emp in employees
            //             join dep in departments
            //             on emp.DepartmentId equals dep.Id
            //             where emp.AnnualSalary > 40000
            //             select new {
            //                 name = emp.FirstName + " " + emp.LastName,
            //                 annualSalary = emp.AnnualSalary,
            //                 department = dep.ShortName,
            //                 departmentDes = dep.LongName,
            //                 departmentId = dep.Id
            //             };
            //foreach (var employee in result)
            //{
            //    Console.WriteLine($"Name: {employee.name}\nAnnualSalary: {employee.annualSalary}\nDepartment: {employee.department}\nDepartment Description: {employee.departmentDes}\nDepartment ID: {employee.departmentId}");
            //    Console.WriteLine();
            //}

            //Method Syntax

            //var result = employees.GroupJoin(departments,
            //    emp => emp.DepartmentId,
            //    dep => dep.Id,
            //    (e, department) => new
            //    {
            //        name = e.FirstName + " " + e.LastName,
            //        annualSalary = e.AnnualSalary,
            //        departmentDes = department,

            //    }).OrderBy(o => o.annualSalary);

            //Console.WriteLine($"Name\t\tDepartmentName");
            //foreach (var employee in result)
            //{
            //    Console.Write($"{employee.name}");
            //    foreach (var emp in employee.departmentDes)
            //    {

            //        Console.WriteLine($"\t\t{emp.LongName}");
            //       // Console.WriteLine();
            //    }

            //}

            //Query Syntax ->  "into" performs GroupJoin operation [left outer join] 

            //var result = from emp in employees
            //             join dep in departments
            //             on emp.DepartmentId equals dep.Id
            //             into departmentList
            //             select new
            //             {
            //                 department = departmentList,
            //                 Name = emp.FirstName+" "+emp.LastName,
            //                 AnnualSal = emp.AnnualSalary
            //             };
            //Console.WriteLine("Employee Name\t\tAnnual Salary\t\tDepartment Name\t\tDepartment ID");
            //foreach (var employee in result)
            //{
            //    Console.Write($"{employee.Name}\t\t{employee.AnnualSal}");
            //    foreach (var dep in employee.department)
            //    {
            //        Console.WriteLine($"\t\t{dep.LongName}\t\t\t{dep.Id}");
            //    }

            //}

            //group by clause Sytax Mehod
            //var result = employees.Join(departments,
            //    emp => emp.DepartmentId,
            //    dep => dep.Id,
            //    (e, d) => new
            //    {
            //        name = e.FirstName + " " + e.LastName,
            //        annualSalary = e.AnnualSalary,
            //        department = d.ShortName,
            //        departmentDes = d.LongName,
            //        departmentId = d.Id
            //    }).GroupBy(d => d.departmentId);


            //foreach (var employee in result)
            //{
            //    Console.WriteLine($"Grouping Key -> {employee.Key}");
            //    foreach (var emp in employee)
            //    {
            //        Console.WriteLine($"Name: {emp.name}\nAnnualSalary: {emp.annualSalary}\nDepartment: {emp.department}\nDepartment Description: {emp.departmentDes}\nDepartment ID: {emp.departmentId}");
            //        Console.WriteLine();
            //    }

            //}

            //Query Syntax

            //var result = from emp in employees
            //             join dep in departments
            //             on emp.DepartmentId equals dep.Id
            //             group emp by emp.DepartmentId;

            //foreach (var employee in result)
            //{
            //    Console.WriteLine($"Grouping Key -> {employee.Key}");
            //    foreach (var emp in employee)
            //    {
            //        Console.WriteLine($"Name: {emp.FirstName}\nAnnualSalary: {emp.AnnualSalary}\nDepartment: {departments.FirstOrDefault(d => d.Id == emp.DepartmentId).LongName}");
            //        Console.WriteLine();
            //    }

            //}

            //ToLookup() is also perform same operation as GroupBy but tolookup is immidiate execution and groupby is deffered execution
            //var result = employees.Join(departments,
            //    emp => emp.DepartmentId,
            //    dep => dep.Id,
            //    (e, d) => new
            //    {
            //        name = e.FirstName + " " + e.LastName,
            //        annualSalary = e.AnnualSalary,
            //        department = d.ShortName,
            //        departmentDes = d.LongName,
            //        departmentId = d.Id
            //    }).OrderBy(d => d.departmentId).ToLookup(d => d.departmentId);


            //foreach (var employee in result)
            //{
            //    Console.WriteLine($"Grouping Key -> {employee.Key}");
            //    foreach (var emp in employee)
            //    {
            //        Console.WriteLine($"Name: {emp.name}\nAnnualSalary: {emp.annualSalary}\nDepartment: {emp.department}\nDepartment Description: {emp.departmentDes}\nDepartment ID: {emp.departmentId}");
            //        Console.WriteLine();
            //    }
            //}


            // any() all() and contains() Quantify Operators

            var Asalary = 30000.0m;

            //var result = (from emp in employees
            //              join dep in departments
            //              on emp.DepartmentId equals dep.Id
            //              select new
            //              {
            //                  name = emp.FirstName + " " + emp.LastName,
            //                  annualSalary = emp.AnnualSalary,
            //                  department = dep.ShortName,
            //                  departmentDes = dep.LongName,
            //                  departmentId = dep.Id
            //              }).Any(e => e.annualSalary > Asalary);
            //if (result)
            //{
            //    Console.WriteLine("some of them having salary greater than 30000rs");
            //}
            //else
            //{
            //    Console.WriteLine("no one having salary greater than 30000rs");
            //}

            //all
            //var result = (from emp in employees
            //              join dep in departments
            //              on emp.DepartmentId equals dep.Id
            //              select new
            //              {
            //                  name = emp.FirstName + " " + emp.LastName,
            //                  annualSalary = emp.AnnualSalary,
            //                  department = dep.ShortName,
            //                  departmentDes = dep.LongName,
            //                  departmentId = dep.Id
            //              }).All(e => e.annualSalary > Asalary);
            //if (result)
            //{
            //    Console.WriteLine("All of them having salary greater than 30000rs");
            //}
            //else
            //{
            //    Console.WriteLine("Not all of them having salary greater than 30000rs");
            //}

            //contains

            //var employeeSearch = new Employee
            //{
            //    Id = 5,
            //    FirstName = "Steve",
            //    LastName = "Jobs",
            //    AnnualSalary = 100000.2m,
            //    IsManager = true,
            //    DepartmentId = 4
            //};
            //var result = (from emp in employees
            //              select emp).Contains(employeeSearch,new comparer());
            //if (result)
            //{
            //    Console.WriteLine("Prsent");
            //}
            //else
            //{
            //    Console.WriteLine("Not Present");
            //}

            //Filter operators OfType used when we have diferent kinds of objects in collection or data source to filter according to types 

            var result = GetHeterogeneousDataCollection();

            //var res = from r in result.OfType<int>()
            //          select r;
            //var res = from r in result.OfType<string>()
            //          select r;
            var res = from r in result.OfType<Employee>()
                      select r;

            foreach ( var r in res )
            {
                Console.WriteLine( r.FirstName +" "+ r.LastName );
            }


        }

        //here contains doesn't know what comapre when passed object in the argument in order to know what comapre we should extend IEqualityComaprer interface to override methods 
        // in which contains overloded method objects of extended class of interface IEqualityComaprer for comparision conditions

        public class comparer : IEqualityComparer<Employee>
        {
            public bool Equals(Employee? x, Employee? y)
            {
                if(x.Id == y.Id)
                {
                    return true;
                }
                return false;
            }

            public int GetHashCode([DisallowNull] Employee obj)
            {
                //used for the purpose of uniquly identifying object through hashcode
                return obj.Id.GetHashCode();
            }
        }

        public static List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();

            Employee employee = new Employee
            {
                Id = 1,
                FirstName = "Bob",
                LastName = "Jones",
                AnnualSalary = 60000.3m,
                IsManager = true,
                DepartmentId = 1
            };
            employees.Add(employee);
            employee = new Employee
            {
                Id = 2,
                FirstName = "Sarah",
                LastName = "Jameson",
                AnnualSalary = 80000.1m,
                IsManager = true,
                DepartmentId = 2
            };
            employees.Add(employee);
            employee = new Employee
            {
                Id = 3,
                FirstName = "Douglas",
                LastName = "Roberts",
                AnnualSalary = 40000.2m,
                IsManager = false,
                DepartmentId = 2
            };
            employees.Add(employee);
            employee = new Employee
            {
                Id = 4,
                FirstName = "Jane",
                LastName = "Stevens",
                AnnualSalary = 20000.2m,
                IsManager = false,
                DepartmentId = 3
            };
            employees.Add(employee);
            employee = new Employee
            {
                Id = 5,
                FirstName = "Steve",
                LastName = "Jobs",
                AnnualSalary = 100000.2m,
                IsManager = true,
                DepartmentId = 4
            };
            employees.Add(employee);

            return employees;
        }

        public static List<Department> GetDepartments()
        {
            List<Department> departments = new List<Department>();

            Department department = new Department
            {
                Id = 1,
                ShortName = "HR",
                LongName = "Human Resources"
            };
            departments.Add(department);
            department = new Department
            {
                Id = 2,
                ShortName = "FN",
                LongName = "Finance"
            };
            departments.Add(department);
            department = new Department
            {
                Id = 3,
                ShortName = "TE",
                LongName = "Technology"
            };
            departments.Add(department);
            department = new Department
            {
                Id = 4,
                ShortName = "NE",
                LongName = "Network"
            };
            departments.Add(department);

            return departments;
        }
        public static ArrayList GetHeterogeneousDataCollection()
        {
            ArrayList arrayList = new ArrayList();

            arrayList.Add(100);
            arrayList.Add("Bob Jones");
            arrayList.Add(2000);
            arrayList.Add(3000);
            arrayList.Add("Bill Henderson");
            arrayList.Add(new Employee { Id = 6, FirstName = "Jennifer", LastName = "Dale", AnnualSalary = 90000, IsManager = true, DepartmentId = 1 });
            arrayList.Add(new Employee { Id = 7, FirstName = "Dane", LastName = "Hughes", AnnualSalary = 60000, IsManager = false, DepartmentId = 2 });
            arrayList.Add(new Department { Id = 4, ShortName = "MKT", LongName = "Marketing" });
            arrayList.Add(new Department { Id = 5, ShortName = "R&D", LongName = "Research and Development" });
            arrayList.Add(new Department { Id = 6, ShortName = "PRD", LongName = "Production" });

            return arrayList;
        }

    }
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal AnnualSalary { get; set; }
        public bool IsManager { get; set; }
        public int DepartmentId { get; set; }
    }
    public class Department
    {
        public int Id { get; set; }
        public string ShortName { get; set; }
        public string LongName { get; set; }
    }


}
