using Backend2.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend2.Controllers;

public sealed class CatalogController(IProductService service) : Controller
{
  private readonly IProductService _service = service;

  [HttpGet("/catalog")]
  public IActionResult Index()
  {
    var products = _service.GetAll();

    return View(products);
  }
}