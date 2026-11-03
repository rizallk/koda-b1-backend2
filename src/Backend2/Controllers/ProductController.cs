using Backend2.Data;
using Backend2.DTOs;
using Backend2.Models;
using Backend2.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend2.Controllers;

[ApiController]
[Route("api/v1/products")]

public sealed class ProductController(IProductService service) : ControllerBase
{
  private readonly IProductService _service = service;

  [HttpGet]
  [ProducesResponseType(typeof(IReadOnlyList<ProductResponse>), StatusCodes.Status200OK)]
  public ActionResult<IReadOnlyList<Product>> GetAll()
  {
    return Ok(_service.GetAll());
  }

  [HttpGet("{productCode}")]
  [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
  public ActionResult<IReadOnlyList<Product>> GetByCode([FromRoute] string productCode)
  {
    var product = _service.GetByCode(productCode);

    return product is null ? Problem(statusCode: StatusCodes.Status404NotFound, title: "Product not found") : Ok(product);
  }

  [HttpPost]
  [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status201Created)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
  public ActionResult<ProductResponse> CreateProduct([FromBody] ProductRequest req)
  {
    try
    {
      var product = _service.Create(req);

      if (product is null)
      {
        return Problem(statusCode: StatusCodes.Status409Conflict, title: "Product code already exists");
      }

      return CreatedAtAction(actionName: nameof(GetByCode), routeValues: new { productCode = product.ProductCode }, value: product);
    }
    catch (ArgumentException e)
    {
      return Problem(statusCode: StatusCodes.Status400BadRequest, title: "Invalid product data!", detail: e.Message);
    }

  }

  [HttpPut("{productCode}")]
  [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
  public ActionResult<ProductResponse> UpdateProduct([FromRoute] string productCode, [FromBody] UpdateProductRequest req)
  {
    try
    {
      var newProduct = _service.Update(productCode, req);

      return newProduct is null ? Problem(statusCode: StatusCodes.Status404NotFound, title: "Product not found", detail: $"No product was found for code {productCode}.") : Ok(newProduct);
    }
    catch (ArgumentException e)
    {
      return Problem(
             statusCode: StatusCodes.Status400BadRequest,
             title: "Invalid product data!",
             detail: e.Message
           );
    }
  }

  [HttpDelete("{productCode}")]
  [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
  public IActionResult Delete([FromRoute] string productCode)
  {
    return _service.Delete(productCode) ? NoContent() : Problem(statusCode: StatusCodes.Status404NotFound, title: "Product not found", detail: $"No product was found for code {productCode}.");
  }
}