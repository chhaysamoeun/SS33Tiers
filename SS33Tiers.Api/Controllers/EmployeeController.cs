using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SS33Tiers.Api.Models;
using SS33Tiers.Api.Services;

namespace SS33Tiers.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employee;
    public EmployeeController(IEmployeeService employee)
    {
        _employee = employee;
    }
    // GET: api/Employee
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Employee>>> Get()
        => await _employee.EmployeeList();
    // GET: api/Employee/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Employee>> Get(Guid id)
        => await _employee.EmployeeList(id);
    // GET: api/Employee/5
    [HttpGet("Search/{name}")]
    public async Task<ActionResult<List<Employee>>> Get(string name)
        => await _employee.Search(name);
    // POST: api/Employee
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] Employee employee)
    {
        if (ModelState.IsValid)
        {
            if(await _employee.Save(employee))
            {
                return Created("Employee", employee);
            }
        }
        return BadRequest(ModelState);
    }

    // PUT: api/Employee/5
    [HttpPut("{id}")]
    public async Task<ActionResult> Put(Guid id, [FromBody] Employee employee)
    {
        if(ModelState.IsValid || id == employee.EmployeeId)
        {
            if(await _employee.Update(employee, id))
            {
                return Ok();
            }
        }
        return BadRequest(ModelState);
    }

    // DELETE: api/Employee/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        if(await _employee.Delete(id))
        {
            return Ok();
        }
        return BadRequest();
    }
}
