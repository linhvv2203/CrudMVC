using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using CrudMvc.Models;

namespace CrudMvc.Controllers
{
    public class MoviesController : ApiController
    {
        private VideoEntities db = new VideoEntities();

        // GET: api/Movies
        public IQueryable<Movy> GetMovies()
        {
            return db.Movies;
        }

        // GET: api/Movies/5
        [ResponseType(typeof(Movy))]
        public IHttpActionResult GetMovy(int id)
        {
            Movy movy = db.Movies.Find(id);
            if (movy == null)
            {
                return NotFound();
            }

            return Ok(movy);
        }

        // PUT: api/Movies/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMovy(int id, Movy movy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != movy.Id)
            {
                return BadRequest();
            }

            db.Entry(movy).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Movies
        [ResponseType(typeof(Movy))]
        public IHttpActionResult PostMovy(Movy movy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Movies.Add(movy);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = movy.Id }, movy);
        }

        // DELETE: api/Movies/5
        [ResponseType(typeof(Movy))]
        public IHttpActionResult DeleteMovy(int id)
        {
            Movy movy = db.Movies.Find(id);
            if (movy == null)
            {
                return NotFound();
            }

            db.Movies.Remove(movy);
            db.SaveChanges();

            return Ok(movy);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MovyExists(int id)
        {
            return db.Movies.Count(e => e.Id == id) > 0;
        }
    }
}