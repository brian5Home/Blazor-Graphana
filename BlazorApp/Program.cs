using BlazorApp.Components;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenTelemetry()
    .ConfigureResource(r => r.AddService("BlazorGrafanaApp.Blazor"))
    .WithTracing(t =>
    {
        t.AddAspNetCoreInstrumentation();
        t.AddHttpClientInstrumentation();
        t.AddOtlpExporter(o =>
        {
            o.Endpoint = new Uri(builder.Configuration["Otlp:Endpoint"] ?? "http://otel-collector:4317");
        });
    });

var apiBaseUrl = builder.Configuration["Api:BaseUrl"] ?? "http://api:8080";
builder.Services.AddHttpClient("Api", client => { client.BaseAddress = new Uri(apiBaseUrl); });
builder.Services.AddScoped<BlazorApp.Services.ApiClient>();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

// Only redirect to HTTPS when not in Docker (Docker typically serves HTTP only; redirection breaks asset loading)
if (app.Environment.IsDevelopment())
    app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
