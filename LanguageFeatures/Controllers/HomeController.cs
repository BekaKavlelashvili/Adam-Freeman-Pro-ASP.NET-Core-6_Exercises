namespace LanguageFeatures.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            //Product?[] products = Product.GetProducts();

            return View("Index", new string[] { "Bob", "Joe", "Alice" });
        }
    }
}
