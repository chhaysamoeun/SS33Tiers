using System;
using Microsoft.EntityFrameworkCore;
using SS33Tiers.Api.Data;
using SS33Tiers.Api.Models;

namespace SS33Tiers.Api.Services;

public class EmployeeService:IEmployeeService,IDisposable
{
	private readonly AppDbContext _context;
	public EmployeeService(AppDbContext context)
	{
        _context = context;
	}

    public async Task<bool> Delete(Guid Id)
    {
        try
        {
            //find record
            var emp = await _context.Employee.FindAsync(Id);
            if (emp is null) return false;
            _context.Employee.Remove(emp);
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public void Dispose()
        => _context?.Dispose();

    public async Task<List<Employee>> EmployeeList()
        => await _context.Employee.Include("Position").Include("Department").ToListAsync();

    public async Task<Employee> EmployeeList(Guid Id)
        => await _context.Employee.FindAsync(Id);

    public async Task<bool> Save(Employee employee)
    {
        try
        {
            employee.EmployeeId = Guid.NewGuid();
            _context.Employee.Add(employee);
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<List<Employee>> Search(string name)
        => await _context.Employee
        .Where(x => x.EmployeeName.Contains(name)).Include("Position").Include("Department").ToListAsync();

    public async Task<bool> Update(Employee employee, Guid Id)
    {
        try
        {
            //find record
            var emp = await _context.Employee.FindAsync(Id);
            if (emp is null) return false;
            _context.Employee.Attach(emp);
            emp.Address = employee.Address;
            emp.DateOfBirth = employee.DateOfBirth;
            emp.DepartmentId = employee.DepartmentId;
            emp.EmployeeName = employee.EmployeeName;
            emp.Gender = employee.Gender;
            emp.PhoneNumber = employee.PhoneNumber;
            emp.PositionId = employee.PositionId;
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }
}

