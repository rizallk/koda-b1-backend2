using Backend2.DTOs;
using Backend2.Models;
using Backend2.Repositories;

namespace Backend2.Services;

public sealed class ProductService(IProductRepository repository) : IProductService
{
  private readonly IProductRepository _repository = repository;

  public ProductResponse? Create(ProductRequest req)
  {
    var product = new Product(req.ProductCode, req.Name, req.Price, req.Stock, req.Category);

    return _repository.TryAdd(product) ? ProductResponse.FromModel(product) : null;
  }

  public bool Delete(string productCode)
  {
    return _repository.TryDelete(productCode);
  }

  public IReadOnlyList<ProductResponse> GetAll()
  {
    return _repository.GetAll().Select(ProductResponse.FromModel).ToArray();
  }

  public ProductResponse? GetByCode(string productCode)
  {
    var product = _repository.GetByCode(productCode);

    return product is null ? null : ProductResponse.FromModel(product);
  }

  public ProductResponse? Update(string productCode, UpdateProductRequest req)
  {
    var product = new Product(productCode, req.Name, req.Price, req.Stock, req.Category);

    return _repository.TryUpdate(product) ? ProductResponse.FromModel(product) : null;
  }
}