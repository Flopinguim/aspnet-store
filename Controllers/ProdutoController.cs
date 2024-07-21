using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace aspnet_store.Controllers
{
    public class ProdutoController : Controller
    {
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

//       [HttpPost]
//       public IActionResult Add()
//       {
//
//       }
    }
}
