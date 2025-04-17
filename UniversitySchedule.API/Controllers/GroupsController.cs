using Microsoft.AspNetCore.Mvc;
using UniversitySchedule.Application.Interfaces.Repositories;
using UniversitySchedule.Domain.Entities;

namespace UniversitySchedule.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GroupsController : ControllerBase
{
    private readonly IGroupRepository _groupRepo;

    public GroupsController(IGroupRepository groupRepo)
    {
        _groupRepo = groupRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _groupRepo.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var group = await _groupRepo.GetByIdAsync(id);
        return group == null ? NotFound() : Ok(group);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Group group)
    {
        await _groupRepo.AddAsync(group);
        return CreatedAtAction(nameof(Get), new { id = group.Id }, group); 
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Group group)
    {
        if (id != group.Id) return BadRequest();
        await _groupRepo.UpdateAsync(group);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _groupRepo.DeleteAsync(id);
        return NoContent();
    }
}
