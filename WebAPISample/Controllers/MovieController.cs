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
        public IHttpActionResult Put(int id, [FromBody]Movie movie)
        {
            var newMovie = Update(id, movie);
            if (newMovie != null)
            {
                return NotFound();
            }
            return Ok(newMovie);
        }
        private Movie Update(int id, Movie movie)
        {
            var newMovie = context.Movies.SingleOrDefault(m => m.MovieId == id); 
            if (newMovie == null || movie == null)
            {
                return null;
            }
            try
            {
                newMovie.Title = movie.Title;
                newMovie.DirectorName = movie.DirectorName;
                newMovie.Genre = movie.Genre;
                context.SaveChanges();
                return newMovie;
            }
            catch (Exception)
            {

                throw new NotImplementedException("Error: Unable to update database");
            }
        }

        
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