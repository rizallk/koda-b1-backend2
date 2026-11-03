using Backend2.Models;

namespace Backend2.Data;

public sealed class InMemoryProductCatalog
{
  private readonly Lock _sync = new();
  private readonly List<Product> _products = [];

  public IReadOnlyList<Product> GetAll()
  {
    lock (_sync)
    {
      return _products.ToArray();
    }
  }

  public Product? GetByCode(string productCode)
  {
    lock (_sync)
    {

      return _products.FirstOrDefault(product => string.Equals(product.ProductCode, productCode.Trim(), StringComparison.OrdinalIgnoreCase));
    }
  }

  public bool TryCreateProduct(Product data)
  {
    lock (_sync)
    {
      if (_products.Any(p => string.Equals(p.ProductCode, data.ProductCode, StringComparison.OrdinalIgnoreCase)))
      {
        return false;
      }
      _products.Add(data);
      return true;
    }
  }

  public bool TryUpdateProduct(Product product)
  {
    lock (_sync)
    {
      var index = _products.FindIndex(p => string.Equals(p.ProductCode, product.ProductCode, StringComparison.OrdinalIgnoreCase));

      if (index < 0) return false;

      _products[index] = product;
      return true;
    }
  }

  public bool TryRemove(string productCode)
  {
    var index = _products.FindIndex(p => string.Equals(p.ProductCode, productCode.Trim(), StringComparison.OrdinalIgnoreCase));

    if (index < 0) return false;

    _products.RemoveAt(index);

    return true;
  }
}