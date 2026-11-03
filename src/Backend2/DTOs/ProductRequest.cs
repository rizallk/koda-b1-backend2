using Backend2.Models;

namespace Backend2.DTOs;

public sealed record ProductRequest(string ProductCode, string Name, decimal Price, int Stock, string Category);