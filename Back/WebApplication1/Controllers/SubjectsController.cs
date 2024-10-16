using Microsoft.AspNetCore.Mvc;
using WebApplication1.Model.Interfaces;
using WebApplication1.Model.Dto;
using WebApplication1.Model;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Repository.Context;

[ApiController]
[Route("[controller]")]
public class SubjectsController : ControllerBase
{
    private readonly ISubjectBusiness _subjectBusiness;
    private readonly ILogger<SubjectsController> _logger;
    private readonly StudentContext _context; // Declare o DbContext aqui

    // Adicione o StudentContext como parâmetro no construtor
    public SubjectsController(ISubjectBusiness subjectBusiness, ILogger<SubjectsController> logger, StudentContext context)
    {
        _subjectBusiness = subjectBusiness;
        _logger = logger;
        _context = context; // Inicialize o DbContext
    }

    [HttpPost]
    public IActionResult CreateSubject(SubjectDto subjectDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var subject = new Subject
            {
                Id = subjectDto.Id,
                SubjectName = subjectDto.SubjectName,
                StudentSubjects = null // Ignorar o campo StudentSubjects
            };

            var createdSubject = _subjectBusiness.Insert(subject);

            var createdSubjectDto = new SubjectDto
            {
                Id = createdSubject.Id,
                SubjectName = createdSubject.SubjectName
            };

            return CreatedAtAction(nameof(GetSubject), new { id = createdSubjectDto.Id }, createdSubjectDto);
        }
        catch (DbUpdateException dbEx)
        {
            var innerMessage = dbEx.InnerException?.Message ?? "No additional information";
            _logger.LogError(dbEx, "Database update error");
            return StatusCode(500, $"Database update error: {dbEx.Message}. Inner exception: {innerMessage}");
        }
        catch (Exception ex)
        {
            var innerMessage = ex.InnerException?.Message ?? "No additional information";
            _logger.LogError(ex, "An error occurred while creating a subject");
            return StatusCode(500, $"Internal server error: {ex.Message}. Inner exception: {innerMessage}");
        }
    }

    

    [HttpGet]
    public async Task<IActionResult> GetStudents()
    {
        try
        {
            var students = await _context.Students.ToListAsync();
            return Ok(students);
        }
        catch (Exception ex)
        {
            // Você pode adicionar logging aqui
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }


    [HttpGet("{id}")]
    public IActionResult GetSubject(int id)
    {
        try
        {
            var subject = _subjectBusiness.Get(id);
            if (subject == null)
            {
                return NotFound();
            }
            var subjectDto = new SubjectDto
            {
                Id = subject.Id,
                SubjectName = subject.SubjectName
            };
            return Ok(subjectDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving a subject");
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public IActionResult UpdateSubject(int id, SubjectDto subjectDto)
    {
        if (id != subjectDto.Id)
        {
            return BadRequest("The ID does not match.");
        }

        try
        {
            var subject = new Subject
            {
                Id = subjectDto.Id,
                SubjectName = subjectDto.SubjectName,
                StudentSubjects = null
            };

            _subjectBusiness.Update(subject);
            return NoContent();
        }
        catch (DbUpdateConcurrencyException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            var innerMessage = ex.InnerException?.Message ?? "No additional information";
            _logger.LogError(ex, "An error occurred while updating the subject");
            return StatusCode(500, $"Internal server error: {ex.Message}. Inner exception: {innerMessage}");
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteSubject(int id)
    {
        try
        {
            _subjectBusiness.Delete(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            var innerMessage = ex.InnerException?.Message ?? "No additional information";
            _logger.LogError(ex, "An error occurred while deleting the subject");
            return StatusCode(500, $"Internal server error: {ex.Message}. Inner exception: {innerMessage}");
        }
    }
}
