using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using todolist.Infrainstructure;
using todolist.Models;
using Microsoft;
using Microsoft.EntityFrameworkCore;

namespace todolist.Controllers
{
    public class TodoController : Controller
    {
        private readonly TodoContext context;

        public TodoController(TodoContext context)
        {
            this.context = context;
        }

        // GET /
        public async Task<ActionResult> Index()
        {
            IQueryable<TodoList> items = from i in context.TodoList orderby i.Id select i;

            List<TodoList> todoList = await items.ToListAsync();

            return View(todoList);
        }

        // GET Todo/create/
        public IActionResult Create() => View();

        // POST Todo/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TodoList item)
        {
            if (ModelState.IsValid)
            {
                context.Add(item);
                await context.SaveChangesAsync();

                TempData["Success"] = "The item has been added!!";

                return RedirectToAction("index");
            }

            return View(item);
        }

        // GET Todo/edit/5
        public async Task<ActionResult> Edit(int id)
        {
            TodoList item = await context.TodoList.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST Todo/edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(TodoList item)
        {
            if (ModelState.IsValid)
            {
                context.Update(item);
                await context.SaveChangesAsync();

                TempData["Success"] = "The item has been edited!";

                return RedirectToAction("index");
            }

            return View(item);
        }

        // GET Todo/delete/5
        public async Task<ActionResult> Delete(int id)
        {
            TodoList item = await context.TodoList.FindAsync(id);

            if (item == null)
            {
                TempData["Error"] = "The item does not exist!";
            }
            else
            {
                context.TodoList.Remove(item);
                await context.SaveChangesAsync();
                TempData["Success"] = "The item has been deleted !";
            }
            return View(item);

        }
    }
}
