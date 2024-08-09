using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoathAPI.DataAccess;
using MoathAPI.DTOs;
using MoathAPI.Models;

namespace MoathAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public StudentsController(ApplicationDbContext dbContext, IMapper mapper)
        {
            this._context = dbContext;
            this._mapper = mapper;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] AddDTO addDTO)
        {
            var student =  _mapper.Map<Student>(addDTO);

            await _context.Students.AddAsync(student);

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateDTO updateDTO)
        {
            var student = await _context.Students.AsNoTracking().FirstOrDefaultAsync(s => s.Id == updateDTO.Id);

            if(student is null)
            {
                return NotFound();
            }

            var UpdateStudent = _mapper.Map<Student>(updateDTO);

            _context.Students.Update(UpdateStudent);

            await _context.SaveChangesAsync();

            var updatedto = _mapper.Map<UpdateDTO>(UpdateStudent);

            return Ok(updatedto);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetByID([FromQuery] int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student is null)
            {
                return NotFound("Student Not Found");
            }

            var dto = _mapper.Map<GetDTOByID>(student);
            return Ok(dto);
        }

        [HttpGet("getAll")]
        public async Task<ActionResult<IEnumerable<GetAllDTO>>> GetAll()
        {
            var students = await _context.Students.ToListAsync();
            return Ok(students);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == id);

            if (student is null)
            {
                return NotFound("Student Not Found");
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return Ok("The student is deleted");
        }
    }
}
