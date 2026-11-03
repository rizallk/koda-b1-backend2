using Backend2.Models;

namespace Backend2.Repositories;

public interface IProductRepository
{
  IReadOnlyList<Product> GetAll();
  Product? GetByCode(string productCode);
  bool TryAdd(Product product);
  bool TryUpdate(Product product);
  bool TryDelete(string productCode);
}