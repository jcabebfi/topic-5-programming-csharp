namespace topic_5_programming_csharp
{
    public class Person
    {
        public string? FirstName;
        public string? LastName;
    }
    public class Employee : Person
    {
        public int EmployeeId { get; set; }
        public double Salary { get; set; }
        private static int _employeeCounter = 0;
        public Department Department { get; set; }

        public Employee(int employeeId, string firstName, string lastName, double salary)
        {
            this.EmployeeId = employeeId;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Salary = salary;
            _employeeCounter++;
        }

        ~Employee() 
        {
            Console.WriteLine("Employee destroyed.");
        }
    }

    public enum Department {  IT, HR, Finance, Accounting };

    interface IManager
    {
        void AssignEmployeeToDepartment(Employee employee, Department department);
    }

    public class EmployeeManager : IManager
    {
        public List<Employee> Employees = new List<Employee>();
        public void AddEmployee(Employee employee)
        {
            var validatedEmployee = Employees.Find(employee => employee.EmployeeId == employee.EmployeeId);
            Console.WriteLine(validatedEmployee);
            if (validatedEmployee != null) return;

            Employees.Add(employee);
        }

        public void AssignEmployeeToDepartment(Employee employee, Department department)
        {
            employee.Department = department;
        }

        public void RemoveEmployee(int employeeId) 
        {
            var emp = Employees.SingleOrDefault(e => e.EmployeeId == employeeId);
            if (emp != null) Employees.Remove(emp);
        }

        public void DisplayDetails()
        {
            try
            {
                Console.WriteLine($"Employee count: {Employees.Count}");
                Console.WriteLine("ID\tFirst Name\tLast Name\tSalary");
                for (int i = 0; i < Employees.Count; i++)
                {
                    //                Console.WriteLine($@"
                    //ID: {employee.EmployeeId}
                    //Name {employee.FirstName} {employee.LastName}
                    //Salary: {employee.Salary}
                    //Department: {employee.Department}
                    //");

                    Console.WriteLine($"{Employees[i].EmployeeId}\t{Employees[i].FirstName}\t\t{Employees[i].LastName}\t\t{Employees[i].Salary}\n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public List<Employee> GetList()
        {
            return Employees;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            string? input = "";
            var choice = 0;
            int id = 0;
            bool loopBreak = true;

            EmployeeManager em = new EmployeeManager();
            em.AddEmployee(new Employee(123, "Test", "Data", 19000));
            em.AddEmployee(new Employee(5432, "Data", "Test", 15000));
            em.AddEmployee(new Employee(243224, "Data", "Test", 15000));

            do
            {

                Console.WriteLine("Employee Management System");
                Console.WriteLine($@"
[1] Add employee
[2] Display employees
[3] Remove employee
[4] Assign employee to department
[5] Exit
");
                Console.WriteLine("Enter the number of your choice:");
                choice = Int32.Parse(Console.ReadLine()); ;

                switch (choice)
                {
                    case 1:
                        string? fn = "", ln = "";
                        double salary = 0;

                        Console.WriteLine("Add employee");
                        Console.WriteLine("Enter id:");
                        input = Console.ReadLine();
                        id = (input is not null) ? Int32.Parse(input) : 0;

                        Console.WriteLine("Enter first name:");
                        input = Console.ReadLine();
                        fn = (input is not null) ? input : "";

                        Console.WriteLine("Enter last name:");
                        input = Console.ReadLine();
                        ln = (input is not null) ? input : "";

                        Console.WriteLine("Enter salary:");
                        input = Console.ReadLine();
                        if (input is not null) salary = Double.Parse(input);

                        Employee _employee = new Employee(id, fn, ln, salary);
                        em.Employees.Add(_employee);
                        loopBreak = false;
                        break;
                    case 2:
                        em.DisplayDetails();
                        break;
                    case 3:
                        Console.WriteLine("Remove employee. \n Please enter employee ID:");
                        string? _empID = Console.ReadLine();
                        if (_empID is not null) id = Int32.Parse(_empID);
                        em.RemoveEmployee(id);
                        loopBreak = false;
                        break;
                    case 4:
                        Console.WriteLine("Assign employee to department.\nProvide an employee id:");
                        input = Console.ReadLine();
                        int _employeeId = Int32.Parse(input);
                        var employee = em.Employees.Find(x => x.EmployeeId == _employeeId);

                        string? departmentChoice = "";

                        if (input == "y" || input == "Y")
                        {
                            int counter = 1;
                            Console.WriteLine("List of departments:");
                            foreach (var d in Enum.GetNames(typeof(Department)))
                            {
                                Console.WriteLine($"{counter}. {d}");
                                counter++;
                            }
                            Console.WriteLine("Type the department of your choice.");
                            departmentChoice = Console.ReadLine();
                            departmentChoice = (departmentChoice is not null) ? departmentChoice : "";
                            switch (departmentChoice.ToLower())
                            {
                                case "it":
                                    em.AssignEmployeeToDepartment(employee, Department.IT);
                                    break;
                                case "finance":
                                    em.AssignEmployeeToDepartment(employee, Department.Finance);
                                    break;
                                case "hr":
                                    em.AssignEmployeeToDepartment(employee, Department.HR);
                                    break;
                                case "accounting":
                                    em.AssignEmployeeToDepartment(employee, Department.Accounting);
                                    break;
                                default:
                                    Console.WriteLine("Invalid department.");
                                    break;
                            }
                            Console.WriteLine($"Employee assigned to {departmentChoice}");
                        }
                        loopBreak = false;
                        break;
                    case 5:
                        Console.WriteLine("Exiting..");
                        //Environment.Exit(0);
                        loopBreak = false;
                        break;
                    default:
                        Console.WriteLine("Please select a valid number.");
                        loopBreak = false;
                        break;
                } 
            } while (loopBreak);
        }
    }
}
