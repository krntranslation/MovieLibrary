using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPISample.Models;

namespace WebAPISample.Controllers
{
    public class MovieController : ApiController
    {
        ApplicationDbContext context;
        public MovieController()
        {
            context = new ApplicationDbContext();
        }
        // GET api/values
        public IHttpActionResult Get()
        {
            var movies = context.Movies.ToList();
            return Ok(movies);
        }
        public IHttpActionResult Get(int id)
        {
            var movie = context.Movies.Find(id);
            if (movie == null)
            {
                return NotFound();
            }
            return Ok(movie);
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody]Movie movie)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            context.Movies.Add(movie);
            context.SaveChanges();
            return Ok(movie);
        }

        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]Movie movie)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var newMovie = context.Movies.Where(m => m.MovieId == id).FirstOrDefault();
            if (newMovie != null)
            {
                newMovie.MovieId = movie.MovieId;
                newMovie.Title = movie.Title;
                newMovie.DirectorName = movie.DirectorName;
                newMovie.Genre = movie.Genre;
                context.SaveChanges();
            }
            else
            {
                return NotFound();
            }
            return Ok(newMovie);
        }
        [HttpPut]
        public IHttpActionResult Delete(int id)
        {
            if (!ModelState.IsValid)         
                return BadRequest();

                var deletedMovie = context.Movies.Where(x => x.MovieId == id).FirstOrDefault();
                context.Movies.Remove(deletedMovie);
                context.SaveChanges();

                return Ok();
            
        }
    }

}