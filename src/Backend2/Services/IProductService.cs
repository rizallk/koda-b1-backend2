using Backend2.DTOs;

namespace Backend2.Services;

public interface IProductService
{
  IReadOnlyList<ProductResponse> GetAll();
  ProductResponse? GetByCode(string productCode);
  ProductResponse? Create(ProductRequest request);
  ProductResponse? Update(string productCode, UpdateProductRequest request);

  bool Delete(string productCode);
}