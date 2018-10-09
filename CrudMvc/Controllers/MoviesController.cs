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
using CrudMvc.DAL;
using System.Web;
using Newtonsoft.Json;

namespace CrudMvc.Controllers
{
    //[Authorize]
    public class MoviesController : ApiController
    {
        private readonly MovieRepository repo;

        public MoviesController()
        {
            repo = new MovieRepository(new VideoEntities());
        }

        // GET: api/Movies
        public IEnumerable<Movy> GetMovies([FromUri] PagingParameterModel pagingparametermodel)
        {
            var source = repo.getMoviesPaging();

            // Get's No of Rows Count   
            int count = source.Count();

            // Parameter is passed from Query string if it is null then it default Value will be pageNumber:1  
            int CurrentPage = pagingparametermodel.pageNumber;

            // Parameter is passed from Query string if it is null then it default Value will be pageSize:20  
            int PageSize = pagingparametermodel.pageSize;

            // Display TotalCount to Records to User  
            int TotalCount = count;

            // Calculating Totalpage by Dividing (No of Records / Pagesize)  
            int TotalPages = (int)Math.Ceiling(count / (double)PageSize);

            // Returns List of Customer after applying Paging   
            IQueryable<Movy> items = source.Skip((CurrentPage - 1) * PageSize).Take(PageSize);

            // if CurrentPage is greater than 1 means it has previousPage  
            var previousPage = CurrentPage > 1 ? "Yes" : "No";

            // if TotalPages is greater than CurrentPage means it has nextPage  
            var nextPage = CurrentPage < TotalPages ? "Yes" : "No";

            // Object which we are going to send in header   
            var paginationMetadata = new
            {
                totalCount = TotalCount,
                pageSize = PageSize,
                currentPage = CurrentPage,
                totalPages = TotalPages,
                previousPage,
                nextPage
            };

            // Setting Header  
            HttpContext.Current.Response.Headers.Add("Paging-Headers", JsonConvert.SerializeObject(paginationMetadata));
            // Returing List of Customers Collections  
            return items.ToList();

        }

        // GET: api/Movies/5
        [ResponseType(typeof(Movy))]
        public IHttpActionResult GetMovy(int id)
        {
            Movy movy = repo.GetMovieByID(id);
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

            repo.UpdateMovie(movy);

            try
            {
                repo.Save();
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

            repo.InsertMovie(movy);
            repo.Save();

            return CreatedAtRoute("DefaultApi", new { id = movy.Id }, movy);
        }

        // DELETE: api/Movies/5
        [ResponseType(typeof(Movy))]
        public IHttpActionResult DeleteMovy(int id)
        {
            Movy movy = repo.GetMovieByID(id);
            if (movy == null)
            {
                return NotFound();
            }

            repo.DeleteMovie(id);
            repo.Save();

            return Ok(movy);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MovyExists(int id)
        {
            //return db.Movies.Count(e => e.Id == id) > 0;
            return false;
        }
    }
}