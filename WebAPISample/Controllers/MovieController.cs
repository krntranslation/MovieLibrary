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

        public IHttpActionResult Post([FromBody]Movie value)
        {
            // Create movie in db logic
            var movieInDb = context.Movies.SingleOrDefault(m => m.MovieId == value.MovieId);
            if (movieInDb == null)
            {
                try
                {
                    var newMovie = context.Movies.Add(value);
                    context.SaveChanges();
                    return Content(HttpStatusCode.Created, newMovie);
                }
                catch (Exception)
                {
                    return InternalServerError(new Exception("ERROR: Unable to create new row in database from supplied data"));
                }

            }
            else
            {
                return Ok(Update(movieInDb.MovieId, value));
            }
        }

        public IHttpActionResult Put(int id, [FromBody]Movie value)
        {
            var newMovie = Update(id, value);
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
                newMovie.Director = movie.Director;
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