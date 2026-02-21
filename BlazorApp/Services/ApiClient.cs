using BlazorApp.Models;

namespace BlazorApp.Services;

public class ApiClient
{
    private readonly IHttpClientFactory _factory;

    public ApiClient(IHttpClientFactory factory) => _factory = factory;

    private HttpClient Client => _factory.CreateClient("Api");

    public async Task<List<Product>> GetProductsAsync(CancellationToken ct = default) =>
        await Client.GetFromJsonAsync<List<Product>>("/api/products", ct) ?? new List<Product>();

    public async Task<Product?> GetProductAsync(int id, CancellationToken ct = default) =>
        await Client.GetFromJsonAsync<Product>($"/api/products/{id}", ct);

    public async Task<Product?> CreateProductAsync(Product product, CancellationToken ct = default)
    {
        var response = await Client.PostAsJsonAsync("/api/products", product, ct);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Product>(ct);
    }

    public async Task<Product?> UpdateProductAsync(int id, Product product, CancellationToken ct = default)
    {
        var response = await Client.PutAsJsonAsync($"/api/products/{id}", product, ct);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Product>(ct);
    }

    public async Task DeleteProductAsync(int id, CancellationToken ct = default)
    {
        var response = await Client.DeleteAsync($"/api/products/{id}", ct);
        response.EnsureSuccessStatusCode();
    }

    public async Task<ReportSummary?> GetReportSummaryAsync(CancellationToken ct = default) =>
        await Client.GetFromJsonAsync<ReportSummary>("/api/reports/summary", ct);
}

public class ReportSummary
{
    public int TotalProducts { get; set; }
    public List<CategorySummary> ByCategory { get; set; } = new();
    public int LowStockCount { get; set; }
}

public class CategorySummary
{
    public string Category { get; set; } = string.Empty;
    public int Count { get; set; }
    public decimal TotalValue { get; set; }
}
