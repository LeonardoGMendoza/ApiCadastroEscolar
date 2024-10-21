using Microsoft.AspNetCore.Mvc;
using WebApplication1.Model;
using WebApplication1.Model.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Business;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentBusiness _studentBusiness;

        public StudentsController(IStudentBusiness studentBusiness)
        {
            _studentBusiness = studentBusiness;
        }

        // POST: /students
        [HttpPost]
        public async Task<ActionResult<Student>> CreateStudentAsync(Student student)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var createdStudent = await _studentBusiness.InsertAsync(student);
                return CreatedAtAction(nameof(GetStudent), new { id = createdStudent.Id }, createdStudent);
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
                var students = await _studentBusiness.GetAllAsync();
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
                var student = await _studentBusiness.GetAsync(id);
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

                var updated = await _studentBusiness.UpdateAsync(student);
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


        // Delete: /students/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            try
            {
                var deleted = await _studentBusiness.DeleteAsync(id);
                if (!deleted)
                {
                    return NotFound(); // Retornar 404 se o aluno não existir
                }

                return NoContent(); // Retornar 204 se a exclusão for bem-sucedida
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (DbUpdateException dbEx) // Captura erros relacionados ao EF
            {
                // Aqui você pode logar o erro ou retornar a mensagem de erro interna
                return StatusCode(500, $"Erro ao salvar as alterações: {dbEx.InnerException?.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        // DELETE: /students/{id}
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteStudent(int id)
        //{
        //    try
        //    {
        //        var deleted = await _studentBusiness.DeleteAsync(id);
        //        if (!deleted)
        //        {
        //            return NotFound(); // Retornar 404 se o aluno não existir
        //        }

        //        return NoContent(); // Retornar 204 se a exclusão for bem-sucedida
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
        //    }
        //}


        private async Task<bool> StudentExists(int id)
        {
            return await _studentBusiness.GetAsync(id) != null;
        }

        [HttpGet("calcularsituacao/{id}")]
        public async Task<IActionResult> CalcularSituacao(int id)
        {
            try
            {
                var result = await _studentBusiness.CalcularSituacaoAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }
    }
}
