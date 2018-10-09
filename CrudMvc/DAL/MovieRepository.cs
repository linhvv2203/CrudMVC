using CrudMvc.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CrudMvc.DAL
{
    public class MovieRepository : IMovieRepository
    {
        VideoEntities context;
        public MovieRepository(VideoEntities _context)
        {
            context = _context;
        }

        public IQueryable<Movy> getMoviesPaging()
        {
            return context.Movies.OrderBy(p=>p.Id);
        }
        public void DeleteMovie(int MovieID)
        {
            Movy movie = context.Movies.Where(p => p.Id == MovieID).FirstOrDefault();
            context.Movies.Remove(movie);
        }

        public Movy GetMovieByID(int MovieID)
        {
            return context.Movies.Find(MovieID);
        }

        public IEnumerable<Movy> GetMovies()
        {
            return context.Movies.ToList();
        }

        public void InsertMovie(Movy movie)
        {
            context.Movies.Add(movie);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void UpdateMovie(Movy movie)
        {
            context.Entry(movie).State = EntityState.Modified;
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if(!this.disposed)
            {
                if(disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}