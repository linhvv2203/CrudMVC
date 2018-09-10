using CrudMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudMvc.DAL
{
    public interface IMovieRepository : IDisposable
    {
        IEnumerable<Movy> GetMovies();
        Movy GetMovieByID(int MovieID);
        void InsertMovie(Movy movie);
        void DeleteMovie(int MovieID);
        void UpdateMovie(Movy movie);
        void Save();
    }
}