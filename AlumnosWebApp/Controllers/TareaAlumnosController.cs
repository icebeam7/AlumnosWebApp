using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using AlumnosWebApp.Models;
using System.Collections.Generic;

namespace AlumnosWebApp.Controllers
{
    public class TareaAlumnosController : ApiController
    {
        private AlumnosWebAppContext db = new AlumnosWebAppContext();

        // GET: api/TareaAlumnos
        public IQueryable<TareaAlumno> GetTareaAlumnoes()
        {
            var data = db.TareaAlumnoes;
            db.Configuration.LazyLoadingEnabled = false;
            return data;
        }

        [Route("api/TareaAlumnos/GetTareasRealizadasByAlumno/{idAlumno}/{evaluado}")]
        public List<TareaAlumno> GetTareasRealizadasByAlumno(int idAlumno, bool evaluado)
        {
            var data = db.TareaAlumnoes.Where(x => x.IdAlumno == idAlumno && x.Evaluado == evaluado).ToList();
            db.Configuration.LazyLoadingEnabled = false;
            return data;
        }

        [Route("api/TareaAlumnos/GetTareaAlumnosByEval/{evaluado}")]
        public List<TareaAlumno> GetTareaAlumnosByEval(bool evaluado)
        {
            var data = db.TareaAlumnoes.Where(x => x.Evaluado == evaluado).ToList();
            db.Configuration.LazyLoadingEnabled = false;
            return data;
        }

        // GET: api/TareaAlumnos/5/6
        [ResponseType(typeof(TareaAlumno))]
        [Route("api/TareaAlumnos/{idTarea}/{idAlumno}")]
        public IHttpActionResult GetTareaAlumno(int idTarea, int idAlumno)
        {
            try
            {
                TareaAlumno tareaAlumno = db.TareaAlumnoes.Where(x => x.IdTarea == idTarea && x.IdAlumno == idAlumno).FirstOrDefault();
                if (tareaAlumno == null)
                {
                    return NotFound();
                }

                return Ok(tareaAlumno);
            }
            catch (Exception ex)
            {
                return Ok(new TareaAlumno() { Mensaje = ex.Message });
            }

        }

        // PUT: api/TareaAlumnos/5/6
        [ResponseType(typeof(void))]
        [Route("api/TareaAlumnos/{idTarea}/{idAlumno}")]
        public IHttpActionResult PutTareaAlumno(int idTarea, int idAlumno, TareaAlumno tareaAlumno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (idTarea != tareaAlumno.IdTarea || idAlumno != tareaAlumno.IdAlumno)
            {
                return BadRequest();
            }

            db.Entry(tareaAlumno).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TareaAlumnoExists(idTarea, idAlumno))
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

        // POST: api/TareaAlumnos
        [ResponseType(typeof(TareaAlumno))]
        public IHttpActionResult PostTareaAlumno(TareaAlumno tareaAlumno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TareaAlumnoes.Add(tareaAlumno);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (TareaAlumnoExists(tareaAlumno.IdTarea, tareaAlumno.IdAlumno))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tareaAlumno.IdTarea }, tareaAlumno);
        }

        // DELETE: api/TareaAlumnos/5/6
        [ResponseType(typeof(TareaAlumno))]
        [Route("api/TareaAlumnos/{idTarea}/{idAlumno}")]
        public IHttpActionResult DeleteTareaAlumno(int idTarea, int idAlumno)
        {
            TareaAlumno tareaAlumno = db.TareaAlumnoes.Where(x => x.IdTarea == idTarea && x.IdAlumno == idAlumno).FirstOrDefault();

            if (tareaAlumno == null)
            {
                return NotFound();
            }

            db.TareaAlumnoes.Remove(tareaAlumno);
            db.SaveChanges();

            return Ok(tareaAlumno);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TareaAlumnoExists(int idTarea, int idAlumno)
        {
            return db.TareaAlumnoes.Count(e => e.IdTarea == idTarea && e.IdAlumno == idAlumno) > 0;
        }
    }
}