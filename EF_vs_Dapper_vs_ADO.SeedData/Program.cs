using EF_vs_Dapper_vs_ADO.Core;
using EF_vs_Dapper_vs_ADO.EF;


using (var context = new AppDBContext())
{
    context.Departments.AddRange(GenerateListOfDepartments(100));
    context.SaveChanges();
    Console.WriteLine("100 Departments Successfully Inserted");

    context.Employees.AddRange(GenerateListOfEmployees(1000));
    context.SaveChanges();
    Console.WriteLine("1000 Employees Successfully Inserted");


}



List<Department> GenerateListOfDepartments(int length)
{
    List<Department> departmentList = new();
    for (int i = 0; i < length; i++)
    {
        Department department = new Department();
        department.Name = Faker.Company.Name();
        department.Location = $"{Faker.Address.Country}, {Faker.Address.City}";

        departmentList.Add(department);
    }
    return departmentList;
}
List<Employee> GenerateListOfEmployees(int length)
{
    List<Employee> employeeList = new();
    for (int i = 0; i < length; i++)
    {
        Employee employee  = new ();
        employee.Name = Faker.Company.Name();
        employee.Address = Faker.Address.StreetAddress();
        employee.BirthDate = DateTime.Now;
        employee.departmentId = Faker.RandomNumber.Next(1, 100);
        employeeList.Add(employee);
    }
    return employeeList;
}