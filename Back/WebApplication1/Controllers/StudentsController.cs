using Microsoft.AspNetCore.Mvc;
using WebApplication1.Model;
using WebApplication1.Model.Interfaces;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {

        //Propiedade
        private readonly IStudentBusiness _studentBusiness;


        //Construtor
        public StudentsController(IStudentBusiness studentBusiness)
        {
            _studentBusiness = studentBusiness;
        }


        // POST: /students
        [HttpPost]
        public async Task<ActionResult<Student>> CreateStudent(Student student)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var createdStudent = _studentBusiness.Insert(student);

                return Ok(createdStudent);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }


        // GET: /students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            try
            {

                var students = _studentBusiness.GetAll();
                return Ok(students);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }


        // GET: /students/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            try
            {
                var student = _studentBusiness.Get(id);

                if (student == null)
                {
                    return NotFound();
                }

                return student;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }





        // PUT: /students/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, Student student)
        {
            try
            {
                if (id != student.Id)
                {
                    return BadRequest("O ID fornecido não corresponde ao ID do estudante.");
                }

                var updated = _studentBusiness.Update(student);

                if (!updated)
                {
                    return NotFound();
                }

                return NoContent(); // Retorna status HTTP 204 (No Content)
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        // DELETE: /students/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            try
            {
                var deleted = _studentBusiness.Delete(id);

                if (!deleted)
                {
                    return NotFound();
                }

                return NoContent(); // Retorna status HTTP 204 (No Content)
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }


        private bool StudentExists(int id)
        {
            return _studentBusiness.Get(id) != null;
        }


        [HttpGet("calcularsituacao/{id}")]
        public async Task<IActionResult> CalcularSituacao(int id)
        {
            try
            {
                var result = _studentBusiness.CalcularSituacao(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }
    }
}
