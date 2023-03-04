using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
    public class HomeController : Controller
    {
        private IStoreRepository _repository;
        public int PageSize = 4;

        public HomeController(IStoreRepository repository)
        {
            _repository = repository;
        }

        public ViewResult Index(string? category, int productPage = 1) => View(new ProductsListViewModel
        {
            Products = _repository.Products
            .Where(x=> x.Category == null || x.Category == category)
            .OrderBy(x => x.ProductId)
            .Skip((productPage - 1) * PageSize)
            .Take(PageSize),
            PagingInfo = new PagingInfo()
            {
                CurrentPage = productPage,
                ItemsPerPage = PageSize,
                TotalItems = category == null ? _repository.Products.Count() : _repository.Products.Where(x=> x.Category == category).Count(),
            },
            CurrentCategory = category
        });
    }
}
