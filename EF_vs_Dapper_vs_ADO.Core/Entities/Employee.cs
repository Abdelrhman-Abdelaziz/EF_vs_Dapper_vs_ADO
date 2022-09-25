using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;


namespace EF_vs_Dapper_vs_ADO.Core
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string? Address { get; set; }
        [Display(Name = "Department")]
        public int departmentId { get; set; }
        [ValidateNever]
        public virtual Department? Department { get; set; }
    }
}
