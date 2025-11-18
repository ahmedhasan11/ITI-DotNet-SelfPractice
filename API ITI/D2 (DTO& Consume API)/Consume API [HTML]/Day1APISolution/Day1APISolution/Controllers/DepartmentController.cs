using Day1APISolution.DTO;
using Day1APISolution.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;
using System.Transactions;

namespace Day1APISolution.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DepartmentController : ControllerBase
	{
		private readonly ITIContext _context;
		public DepartmentController(ITIContext context)
		{
			_context = context;
		}
		[HttpGet]
		public IActionResult GetAllDepartments()
		{
			List<Department> departments=_context.Departments.ToList();
			//now we need to retur a HTTP response
			return Ok(departments);
		}
		[HttpGet("Count")]
		public ActionResult<List<DeptWithEmpCountDTO>> GetDeptDetails(DeptWithEmpCountDTO deptWithEmpCountDTO)
		{
			List<Department> departments = _context.Departments.Include(d=>d.Emps).ToList();
			List<DeptWithEmpCountDTO> DTOList = new List<DeptWithEmpCountDTO>();

			foreach (Department department in departments)
			{
				DeptWithEmpCountDTO dto= new DeptWithEmpCountDTO();
				dto.Name = department.Name;
				dto.ID = department.ID;
				dto.EmpCount=department.Emps.Count();
				DTOList.Add(dto);				
			}
			//return Ok(DTOList); -->works when you return IActionResult
			return DTOList;
		}
		[HttpPost]
		public IActionResult AddDepartment(Department department)
		{
			_context.Departments.Add(department);
			_context.SaveChanges();
			//instead of return Ok , there is a status code of 201 that says created
			//gie me the name of the aciton and i will return its url
			//and if the action needs a parameter , pass it as anonymous obj
			return CreatedAtAction("GetDepartmentByID", new { id=department.ID}, department);
		}
		[HttpGet("{id}")]
		public IActionResult GetDepartmentByID(int id )
		{
			Department dept=_context.Departments.FirstOrDefault(d => d.ID == id);
			return Ok(dept);
		}

		[HttpPut("{id}")]
		public IActionResult UpdateDepartment(int id , Department department)
		{
			var exisitingDept = _context.Departments.FirstOrDefault(d => d.ID == id);
			if (exisitingDept!=null)
			{
				exisitingDept.Name = department.Name;
				exisitingDept.ManagerName = department.ManagerName;
				_context.SaveChanges();
				return NoContent();
			}
			else 
			{
				return NotFound("department not valid");
			}
		}
	}
}
