﻿using Microsoft.AspNetCore.Mvc;
using MusicLibraryWebAPI.Data;
using MusicLibraryWebAPI.Migrations;
using MusicLibraryWebAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicLibraryWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SongsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/<SongsController>
        [HttpGet]
        public IActionResult Get()
        {
            var songs = _context.Songs.ToList();
            return StatusCode(200, songs);
        }

        // GET api/<SongsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var song = _context.Songs.Find(id);
            if (song == null)
            {
                return NotFound();
            }
            return StatusCode(200, song);
        }

        // POST api/<SongsController>
        [HttpPost]
        public IActionResult Post([FromBody] Song song)
        {
            _context.Songs.Add(song);
            _context.SaveChanges();
            return StatusCode(201, song);
        }

        // PUT api/<SongsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Song updateSong)
        {
            var currentSong = _context.Songs.Find(id);

            if (currentSong == null)
            {
                return NotFound();
            }
            currentSong.Title = updateSong.Title;
            currentSong.Artist = updateSong.Artist;
            currentSong.Album = updateSong.Album;
            currentSong.ReleaseDate = updateSong.ReleaseDate;
            currentSong.Genre = updateSong.Genre;

            _context.Songs.Update(currentSong);
            _context.SaveChanges();
            return StatusCode(200, currentSong);

      
        }
        // DELETE api/<SongsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var song = _context.Songs.Find(id);
            _context.Songs.Remove(song);
            _context.SaveChanges();
            return NoContent();
        }


        // PUT api/ Like Song
        [HttpPut("{id}/like")]
        public IActionResult LikeSong(int id)
        {
            var song = _context.Songs.Find(id);

            if (song == null)
            {
                return NotFound("Song ID does not exist.");
            }
            song.Likes++;
            _context.SaveChanges();
            return Ok(new { NumberOfLikes = song.Likes });

        }
    }
}
