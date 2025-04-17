using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UniversitySchedule.Application.DTOs;
using UniversitySchedule.Application.Interfaces.Services;

namespace UniversitySchedule.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LessonsController : ControllerBase
    {
        private readonly ILessonService _lessonService;

        public LessonsController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LessonDto>>> GetAll()
        {
            var lessons = await _lessonService.GetAllLessonsAsync();
            return Ok(lessons);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LessonDto>> GetById(int id)
        {
            var lesson = await _lessonService.GetLessonByIdAsync(id);
            if (lesson == null)
                return NotFound();

            return Ok(lesson);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Application.DTOs.CreateLessonDto dto)
        {
            await _lessonService.CreateLessonAsync(dto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Application.DTOs.UpdateLessonDto dto)
        {
            if (id != dto.Id)
                return BadRequest("ID mismatch.");

            await _lessonService.UpdateLessonAsync(dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _lessonService.DeleteLessonAsync(id);
            return Ok();
        }
    }
}
