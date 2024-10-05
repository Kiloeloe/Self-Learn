using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCPractice2.Data;
using MVCPractice2.Models;

namespace MVCPractice2.Controllers
{
    public class ItemsController : Controller
    {
        private readonly MVCPractice2Context _context;

        public ItemsController(MVCPractice2Context context)
        {
            _context = context;
        }

        //using async is recommended because it allows us to wait untill all the 
        //necessary data is gathered before starting a method
        //requires the use of EF Core Framework

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //listing by joining the models
            var item = await _context.Items.Include(i => i.SerialNumber)
                .Include(c => c.Category)
                //Joining the UserItems table from the Items table first, then the User table
                .Include(ui => ui.UserItems)
                .ThenInclude(u => u.User)
                .ToListAsync();
            return View(item);
        }

        public IActionResult Create()
        {
            //listing the categories as a dictionary by Id and Name. also using the Render Library
            ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

    /*    [HttpPost]
        //using binding so model recognizes that its a part of the item
        public async Task<IActionResult> Create([Bind("Id,Name, Price, CategoryId")] Item item)
        {
            //checking if the form is correct
            if (ModelState.IsValid)
            {
                //using dbcontext to add iem to database
                _context.Items.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            //if the form/model is invalid
            return View(item);
        }*/

        //other method that uses the entire Model to make it more obscure
        [HttpPost]
        public async Task<IActionResult> Create(Item item)
        {
            if (ModelState.IsValid)
            {
                _context.Items.Add(item);
                await _context.SaveChangesAsync(); 
                return RedirectToAction("Index");   
            }
            return View(item);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            //declaring the dictionary of category
            ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Name");
            //using await to wait for the result, and search asynchronously
            var item = await _context.Items.FindAsync(id);
            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id, Name, Price, CategoryId")] Item item)
        {
            if (ModelState.IsValid)
            {
                //simply update the item
                _context.Update(item);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(item);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            //first approach using FirstOrDefault with extra parameters
            /*var item = await _context.Items.FirstOrDefault(i => i.Id == id);*/

            //second approach using FindAsync specifically designed to search primary keys
            var item = await _context.Items.FindAsync(id);
            
            return View(item);  
        }

        //you can rename the action for the FE to reuse it or just redirect to the
        //name before
        [HttpPost, ActionName("Delete")]
        //method parameters and name cannot be the same, so we change this one's name
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if(item != null)
            {
                _context.Remove(item);
                await _context.SaveChangesAsync();  
            }
            return RedirectToAction("Index");
        }
    }
}
