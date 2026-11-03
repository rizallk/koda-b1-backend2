using Backend2.Data;
using Backend2.Models;

namespace Backend2.Repositories;

public sealed class InMemoryProductRepository(InMemoryProductCatalog catalog) : IProductRepository
{
  private readonly InMemoryProductCatalog _catalog = catalog;

  public IReadOnlyList<Product> GetAll() => _catalog.GetAll();

  public Product? GetByCode(string productCode) => _catalog.GetByCode(productCode);

  public bool TryAdd(Product product) => _catalog.TryCreateProduct(product);

  public bool TryDelete(string productCode) => _catalog.TryRemove(productCode);

  public bool TryUpdate(Product product) => _catalog.TryUpdateProduct(product);
}