using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace EF_vs_Dapper_vs_ADO.Core
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string Location { get; set; }
        [ValidateNever]
        public virtual ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
