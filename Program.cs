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
        public Department Department { get; set; }

        public Employee(int employeeId, string firstName, string lastName, double salary, Department department)
        {
            this.EmployeeId = employeeId;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Salary = salary;
            this.Department = department;
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
        private int _employeeCounter = 0;
        public void AddEmployee(Employee employee)
        {
            try
            {
                var validatedEmployee = Employees.Find(e => e.EmployeeId == employee.EmployeeId);
                if (validatedEmployee is not null) throw new Exception("ID already exists.");
                Employees.Add(employee);
            }
            catch (Exception ex) {
                Console.WriteLine("ID already exists.");
                return;
            }
            _employeeCounter++;
        }

        public void AssignEmployeeToDepartment(Employee employee, Department department)
        {

            employee.Department = department;
        }

        public void RemoveEmployee(int employeeId) 
        {
            var emp = Employees.SingleOrDefault(e => e.EmployeeId == employeeId);
            if (emp != null) Employees.Remove(emp);
            Console.WriteLine($"{emp.FirstName} {emp.LastName} with employee ID: {emp.EmployeeId} has been removed from the system.");
        }

        public void DisplayDetails()
        {
            try
            {
                Console.WriteLine($"Employee count: {Employees.Count}");
                Console.WriteLine("ID\tFirst Name\tLast Name\tSalary\tDepartment");

                foreach (var e in Employees)
                {
                    Console.WriteLine($"{e.EmployeeId}\t{e.FirstName}\t\t{e.LastName}\t\t{e.Salary}\t{e.Department}");
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

            EmployeeManager em = new EmployeeManager();
            Employee emp1 = new Employee(243224, "Data", "Test", 15000, Department.Finance);
            em.AddEmployee(new Employee(123, "Test", "Data", 19000, Department.IT));
            em.AddEmployee(new Employee(5432, "Data", "Test", 15000, Department.HR));

            em.AddEmployee(emp1);
            em.DisplayDetails();

            em.AddEmployee(new Employee(123, "Test", "Data", 19000, Department.IT));
            em.RemoveEmployee(5432);
            em.DisplayDetails();

            em.AssignEmployeeToDepartment(emp1, Department.HR);
            em.DisplayDetails();
            Console.ReadLine();
        }
    }
}
