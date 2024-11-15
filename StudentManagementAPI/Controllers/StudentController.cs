using DataBaseLayer.Contracts;
using DataBaseLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _repository; 
        public StudentController(IStudentRepository repository) 
        { 
            _repository = repository;
        }
        [HttpGet] 
        public ActionResult<IEnumerable<Student>> GetStudents() 
        {
            var students = _repository.GetAll(); 
            return Ok(students); 
        }
        // GET api/<StudentController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var students = _repository.GetById(id);
            return Ok(students);
        }

        // POST api/<StudentController>
        [HttpPost]
        public void Post([FromBody] Student student)
        {
            _repository.Add(student);

        }

        // PUT api/<StudentController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Student student)
        {
            _repository.Update(student);
        }

        // DELETE api/<StudentController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}
