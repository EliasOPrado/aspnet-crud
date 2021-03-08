using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using todolist.Infrainstructure;

namespace todolist.Controllers
{
    public class TodoController : Controller
    {
        private readonly TodoContext context;
        public String Index()
        {
            return "test";
        }
    }
}
