using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SS33Tiers.Api.Models;

public class Employee
{
	[Key]
	public Guid EmployeeId { get; set; }
	[Required]
	[MaxLength(50)]
	public string EmployeeName { get; set; }
	[Required]
	[MaxLength(10)]
	public string Gender { get; set; }
	public DateTime DateOfBirth { get; set; }
	[MaxLength(50)]
	[Phone]
	public string PhoneNumber { get; set; }
	[MaxLength(500)]
	public string Address { get; set; }
	[ForeignKey("Position")]
	public Guid PositionId { get; set; }
    [ForeignKey("Department")]
    public Guid DepartmentId { get; set; }

	public Department Department { get; set; }
	public Position Position { get; set; }
}

