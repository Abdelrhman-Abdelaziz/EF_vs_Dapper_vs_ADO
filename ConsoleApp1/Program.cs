using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;



static IEnumerable<string> GenerateListOfProperties(IEnumerable<PropertyInfo> listOfProperties)
{
    return from property in listOfProperties
           let attributes = property.GetCustomAttributes(typeof(DescriptionAttribute), false)
           where attributes.Length <= 0 || (attributes[0] as DescriptionAttribute)?.Description != "ignore"
           select property.Name;

}


Employee emp = new Employee();
var properties = GenerateListOfProperties(emp.GetType().GetProperties());

foreach (var item in properties)
{
    Console.WriteLine(item);
}










public class Employee
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    public DateTime BirthDate { get; set; }
    public string? Address { get; set; }
    public int departmentId { get; set; }
    // ignore
    public virtual Department? Department { get; set; }
}
public class Department
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Location { get; set; }
    public virtual ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
}