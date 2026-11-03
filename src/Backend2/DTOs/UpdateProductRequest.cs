namespace Backend2.DTOs;

public record UpdateProductRequest(string Name, decimal Price, int Stock, string Category);