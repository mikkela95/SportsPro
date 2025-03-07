// Controllers/HomeController.cs
using Microsoft.AspNetCore.Mvc;
using SimpleMvcApp.Models;
using System.Collections.Generic;

namespace SimpleMvcApp.Controllers
{  [Route("demo")]
public class DemoController : Controller
{
    [HttpGet("")]
    public IActionResult Index()
    {
        var people = new List<Person>
        {
            new Person { Id = 1, Name = "Alice" },
            new Person { Id = 2, Name = "Bob" },
            new Person { Id = 3, Name = "Charlie" }
        };

        return View(people);
    }
}

}
