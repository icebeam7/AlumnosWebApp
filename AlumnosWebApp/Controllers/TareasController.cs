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
using AlumnosWebApp.Models;

namespace AlumnosWebApp.Controllers
{
    public class TareasController : ApiController
    {
        private AlumnosWebAppContext db = new AlumnosWebAppContext();

        // GET: api/Tareas
        public IQueryable<Tarea> GetTareas()
        {
            return db.Tareas.OrderByDescending(x => x.FechaPublicacion);
        }

        // GET: api/Tareas/5
        [ResponseType(typeof(Tarea))]
        public IHttpActionResult GetTarea(int id)
        {
            Tarea tarea = db.Tareas.Find(id);
            if (tarea == null)
            {
                return NotFound();
            }

            return Ok(tarea);
        }

        // PUT: api/Tareas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTarea(int id, Tarea tarea)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tarea.Id)
            {
                return BadRequest();
            }

            db.Entry(tarea).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TareaExists(id))
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

        // POST: api/Tareas
        [ResponseType(typeof(Tarea))]
        public IHttpActionResult PostTarea(Tarea tarea)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tareas.Add(tarea);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tarea.Id }, tarea);
        }

        // DELETE: api/Tareas/5
        [ResponseType(typeof(Tarea))]
        public IHttpActionResult DeleteTarea(int id)
        {
            Tarea tarea = db.Tareas.Find(id);
            if (tarea == null)
            {
                return NotFound();
            }

            db.Tareas.Remove(tarea);
            db.SaveChanges();

            return Ok(tarea);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TareaExists(int id)
        {
            return db.Tareas.Count(e => e.Id == id) > 0;
        }
    }
}