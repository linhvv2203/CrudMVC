using CrudMvc.DAL;
using CrudMvc.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CrudMvc.Controllers
{
    public class MovieController : Controller
    {
        private IMovieRepository movieRepository;

        public MovieController()
        {
            this.movieRepository = new MovieRepository(new VideoEntities());
        }

        // GET: Movie
        public ActionResult Index()
        {

            List<Movy> movies = movieRepository.GetMovies().ToList();

            return View(movies);
            //using (VideoEntities db = new VideoEntities())
            //{
                
            //    List<Movy> lstMovy = db.Movies.ToList();

            //    return View(lstMovy);
            //}

        }

        // GET: Movie/Details/5
        public ActionResult Details(int id)
        {

            Movy movie = movieRepository.GetMovieByID(id);
            return View(movie);
            //using (VideoEntities db = new VideoEntities())
            //{
            //    Movy movi = db.Movies.Where(p => p.Id == id).FirstOrDefault();
            //    return View(movi);
            //}
        }

        // GET: Movie/Create
        public ActionResult Create()
        {

            return View();

        }

        // POST: Movie/Create
        [HttpPost]
        public ActionResult Create(Movy movie)
        {
            try
            {
                // TODO: Add insert logic here

                movieRepository.InsertMovie(movie);
                movieRepository.Save();
                return RedirectToAction("Index");
                //using (VideoEntities db = new VideoEntities())
                //{
                //    db.Movies.Add(movie);
                //    db.SaveChanges();
                //    return RedirectToAction("Index");
                //}
            }
            catch
            {
                return View();
            }
        }

        // GET: Movie/Edit/5
        public ActionResult Edit(int id)
        {
            Movy movie = movieRepository.GetMovieByID(id);
            return View(movie);     
            //using (VideoEntities db = new VideoEntities())
            //{
            //    Movy movi = db.Movies.Where(p => p.Id == id).FirstOrDefault();
            //    return View(movi);
            //}
        }

        // POST: Movie/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Movy movie)
        {
            try
            {
                // TODO: Add update logic here

                movieRepository.UpdateMovie(movie);
                movieRepository.Save();

                //using (VideoEntities db = new VideoEntities())
                //{
                //    db.Entry(movie).State = EntityState.Modified;
                //    db.SaveChanges();
                //}
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Movie/Delete/5
        public ActionResult Delete(int id)
        {
            return View(movieRepository.GetMovieByID(id));
            //using (VideoEntities db = new VideoEntities())
            //{
            //    return View(db.Movies.Where(p => p.Id == id).FirstOrDefault());
            //}
        }

        // POST: Movie/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Movy movie)
        {
            try
            {
                // TODO: Add delete logic here

                movieRepository.DeleteMovie(id);
                movieRepository.Save();

                //using (VideoEntities db = new VideoEntities())
                //{
                //    Movy movy = db.Movies.Where(p => p.Id == id).FirstOrDefault();
                //    db.Movies.Remove(movy);
                //    db.SaveChanges();
                //}

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
