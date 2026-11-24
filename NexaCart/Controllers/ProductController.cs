using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NexaCart.Data_DB;
using NexaCart.Models;

namespace NexaCart.Controllers
{
    public class ProductController: Controller
    {
        private readonly NexaCartDBContext _Context;

        public ProductController (NexaCartDBContext context)
        {
            _Context = context;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _Context.products.Include(p => p.Category).ToListAsync();

            return View(products);
        }

        #region Details Product

        public async Task<IActionResult> Details(int? id)
        {
            if(id == null) return NotFound();
            

            var Product=await _Context.products.Include(p=>p.Category).FirstOrDefaultAsync(p=>p.Id==id);
            if(Product == null) return NotFound();

                return View(Product);
        }

        #endregion

        #region Create Product
        //Get
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories=await _Context.categories.ToListAsync();//Drop Down

            return View();

        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if( ModelState.IsValid)
            {
                _Context.products.Add(product);
                await _Context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = await _Context.categories.ToListAsync();
            return View(product);
        }

        #endregion



        #region Edit Product

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var product = await _Context.products.FindAsync(id);

            if (product == null)
                return NotFound();

            ViewBag.Categories = await _Context.categories
                .ToListAsync();

            return View(product);
        }


        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                _Context.products.Update(product);

                await _Context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = await _Context.categories
                .ToListAsync();

            return View(product);
        }

        #endregion

        #region Delete Product
        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var product = await _Context.products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return NotFound();

            return View(product);
        }


        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _Context.products
                .FindAsync(id);

            if (product != null)
            {
                _Context.products.Remove(product);

                await _Context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        #endregion


    }
}
