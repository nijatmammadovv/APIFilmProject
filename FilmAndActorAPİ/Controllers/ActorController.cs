using FilmAndActorAPİ.ActorDTOs;
using FilmAndActorAPİ.Data_Access_Layer;
using FilmAndActorAPİ.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAndActorAPİ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private AppDbContext _context { get; }
        public ActorController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Actors.ToListAsync());
        }
        [HttpGet("id")]
        public async Task<IActionResult> Get(int id)
        {
            Actor actor = await _context.Actors.Where(a => a.IsDeleted == false && a.Id == id).FirstOrDefaultAsync();
            if (actor == null) return BadRequest();
            return Ok(actor);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateActorDto createActorDto)
        {
            if (!ModelState.IsValid) return StatusCode(StatusCodes.Status400BadRequest);
            Actor actor = new Actor
            {
                Name = createActorDto.Name,
                Surname = createActorDto.Surname,
                ImageUrl = createActorDto.ImageUrl,
                IsDeleted = false
            };
            await _context.Actors.AddAsync(actor);
            await _context.SaveChangesAsync();
            return Ok(createActorDto);
        }
        [HttpPut("id")]
        public IActionResult Update(int id,Actor actor)
        {
            Actor actor1 = _context.Actors.Find(id);
            if (actor1 == null) return StatusCode(StatusCodes.Status400BadRequest);
            actor1.Name = actor.Name;
            actor1.Surname = actor.Surname;
            actor1.ImageUrl = actor.ImageUrl;
            _context.SaveChanges();
            return Ok(actor);
        }
        [HttpDelete("id")]
        public IActionResult Delete(int id)
        {
            Actor actor = _context.Actors.Find(id);
            if (actor is null) return StatusCode(StatusCodes.Status404NotFound);
            _context.Actors.Remove(actor);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
