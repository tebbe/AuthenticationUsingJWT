using AuthenticationUsingJWT.Interface;
using AuthenticationUsingJWT.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AuthenticationUsingJWT.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IJwtAuth _jwtAuth;

        private readonly List<Employee> employeeList = new List<Employee>()
        {
            new Employee{Id=1,Name="Asaad"},
            new Employee{Id=2,Name="Basir"},
            new Employee{Id=3,Name="Collol"},
            new Employee{Id=4,Name="Dina"},

        };

        public EmployeeController(IJwtAuth jwtAuth)
        {
            _jwtAuth = jwtAuth;
        }
        // GET: api/<EmployeeController>
        [HttpGet]
        [Route("GetEmployee")]
        public IEnumerable<Employee> AllEmployee()
        {
            return employeeList;
        }

        // GET api/<EmployeeController>/5
        [HttpGet]
        [Route("EmployeeById/{id}")]
        public Employee EmployeeById(int id)
        {
            return employeeList.Find(m => m.Id == id);
        }

        // POST api/<EmployeeController>
        [AllowAnonymous]
        [HttpPost("authentication")]
        public IActionResult Authentication([FromBody] UserCredential userCredential)
        {
            var token = _jwtAuth.Authentication(userCredential.UserName, userCredential.Password);
            if (token == null)
                return Unauthorized();
            return Ok(token);
        }


        //// PUT api/<EmployeeController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<EmployeeController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
