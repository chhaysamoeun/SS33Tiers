using System;
using SS33Tiers.Api.Models;

namespace SS33Tiers.Api.Services;

public interface IEmployeeService
{
    Task<List<Employee>> EmployeeList();
    Task<Employee> EmployeeList(Guid Id);
    Task<List<Employee>> Search(string name);
    Task<bool> Save(Employee employee);
    Task<bool> Update(Employee employee,Guid Id);
    Task<bool> Delete(Guid Id);
}

