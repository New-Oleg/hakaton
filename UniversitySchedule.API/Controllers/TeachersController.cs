using Microsoft.AspNetCore.Mvc;
using UniversitySchedule.Application.Interfaces.Repositories;
using UniversitySchedule.Domain.Entities;

namespace UniversitySchedule.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeachersController : ControllerBase
{
    private readonly ITeacherRepository _teacherRepo;

    public TeachersController(ITeacherRepository teacherRepo)
    {
        _teacherRepo = teacherRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _teacherRepo.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var teacher = await _teacherRepo.GetByIdAsync(id);
        return teacher == null ? NotFound() : Ok(teacher);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Teacher teacher)
    {
        await _teacherRepo.AddAsync(teacher);
        return CreatedAtAction(nameof(Get), new { id = teacher.Id }, teacher);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Teacher teacher)
    {
        if (id != teacher.Id) return BadRequest();
        await _teacherRepo.UpdateAsync(teacher);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _teacherRepo.DeleteAsync(id);
        return NoContent();
    }
}
