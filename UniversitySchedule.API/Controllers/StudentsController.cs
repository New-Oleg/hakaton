using Microsoft.AspNetCore.Mvc;
using UniversitySchedule.Application.Interfaces.Repositories;
using UniversitySchedule.Domain.Entities;

namespace UniversitySchedule.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly IStudentRepository _studentRepo;

    public StudentsController(IStudentRepository studentRepo)
    {
        _studentRepo = studentRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _studentRepo.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var student = await _studentRepo.GetByIdAsync(id);
        return student == null ? NotFound() : Ok(student);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Student student)
    {
        await _studentRepo.AddAsync(student);
        return CreatedAtAction(nameof(Get), new { id = student.Id }, student);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Student student)
    {
        if (id != student.Id) return BadRequest();
        await _studentRepo.UpdateAsync(student);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _studentRepo.DeleteAsync(id);
        return NoContent();
    }
}
