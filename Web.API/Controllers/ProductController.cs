using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using Web.API.Contexts;
using Web.API.Models;

namespace Web.API.Controllers
{
    [RoutePrefix("api/products")]
    public class ProductController : ApiController
    {
        private ProductContext db = new ProductContext();

        public ProductController()
        {
        }

        // GET: api/products
        [HttpGet]
        [Route("")]
        [Authorize(Roles = "Admin, User")]
        public IHttpActionResult GetProducts()
        {
            try
            {
                return Json(db.Products.ToList());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting products: {ex.Message}");
                return InternalServerError(ex);
            }
        }

        // GET: api/products/5
        [HttpGet]
        [Route("{id:int}")]
        [ResponseType(typeof(Product))]
        [Authorize(Roles = "Admin, User")]
        public IHttpActionResult GetProduct(int id)
        {
            try
            {
                Product product = db.Products.Find(id);
                if (product == null)
                {
                    return NotFound();
                }
                return Json(product);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting product with ID {id}: {ex.Message}");
                return InternalServerError(ex);
            }
        }

        // PUT: api/products/5
        [HttpPut]
        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult PutProduct(int id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!ProductExists(id))
            {
                return BadRequest();
            }

            db.Entry(product).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
                return Ok(product);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    Console.WriteLine($"Concurrency error updating product with ID {id}: {ex.Message}");
                    return InternalServerError(ex);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating product with ID {id}: {ex.Message}");
                return InternalServerError(ex);
            }
        }

        // POST: api/products
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(Product))]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult PostProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                db.Products.Add(product);
                db.SaveChanges();

                return CreatedAtRoute("", new { id = product.ID }, product);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding new product: {ex.Message}");
                return InternalServerError(ex);
            }
        }

        // DELETE: api/products/5
        [HttpDelete]
        [Route("{id:int}")]
        [ResponseType(typeof(Product))]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult DeleteProduct(int id)
        {
            try
            {
                Product product = db.Products.Find(id);
                if (product == null)
                {
                    return NotFound();
                }

                db.Products.Remove(product);
                db.SaveChanges();

                return Ok($"Successfully deleting product with ID {id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting product with ID {id}: {ex.Message}");
                return InternalServerError(ex);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int id)
        {
            return db.Products.Count(e => e.ID == id) > 0;
        }
    }
}