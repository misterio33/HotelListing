using Microsoft.Net.Http.Headers;

namespace HotelListing.API.Middleware;

public class CachingMiddleware
{
    private readonly RequestDelegate _next;

    public CachingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        context.Response.GetTypedHeaders().CacheControl =
            new CacheControlHeaderValue()
            {
                Public = true,
                MaxAge = TimeSpan.FromSeconds(10)
            };
        context.Response.Headers[HeaderNames.Vary] =
            new string[] { "Accept-Encoding" };

        await _next(context);
    }
}