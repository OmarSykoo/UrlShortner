using Microsoft.EntityFrameworkCore;
using Shortner;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<UrlShorteningService>();
builder.Services.AddDbContext<UrlDbContext>((sp, options) =>
{
    options.UseSqlite("Data Source=shorten.db;");
});

WebApplication app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("api/shorten", async (
    ShortenRequest request,
    UrlShorteningService urlShorteningService,
    UrlDbContext urlDbContext,
    HttpContext httpContext) =>
{
    if (!Uri.TryCreate(request.Url, UriKind.Absolute, out _))
        return Results.BadRequest("Invalid Url Format");
    var code = await urlShorteningService.GenerateUniqueCode();
    var shortendUrl = new ShortendUrl()
    {
        Id = Guid.NewGuid(),
        LongUrl = request.Url,
        Code = code,
        ShortUrl = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}/api/{code}",
        CreatedOnUtc = DateTime.Now
    };
    urlDbContext.ShortendUrls.Add(shortendUrl);
    await urlDbContext.SaveChangesAsync();
    return Results.Ok(shortendUrl.ShortUrl);
});

app.MapGet("api/{code}", async (string code, UrlDbContext context) =>
{
    ShortendUrl? shortendUrl = await context.ShortendUrls.FirstOrDefaultAsync(x => x.Code == code);
    if (shortendUrl == null)
        return Results.NotFound($"Url with code {code} doesn't exist");
    return Results.Redirect(shortendUrl.LongUrl);
});

app.UseHttpsRedirection();
app.Run();