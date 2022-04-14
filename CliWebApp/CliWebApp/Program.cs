using System.Diagnostics;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

var app = builder.Build();


app.Use(async (context, next) =>
{
    await next.Invoke();

    if (context.Response.StatusCode == 200)
    {
        if(UrlHandler.validUrlArray.ContainsKey(context.Request.Path.Value.ToLower()))
            UrlHandler.validUrlArray[context.Request.Path.Value.ToLower()]++;
        else
            UrlHandler.validUrlArray.Add(context.Request.Path.Value.ToLower(), 1);
    }
    else
    {
        UrlHandler.invalidUrls.Add(context.Request.Path.Value);
    }
});

//books
app.Use(async (context, next) =>
{
    if(context.Request.Path.Value.StartsWith("/books"))
        await context.Response.WriteAsync("Books");
    
    else
        await next.Invoke();
});

app.Use(async (context, next) =>
{
    if (context.Request.Path.Value.StartsWith("/time"))
        await context.Response.WriteAsync(DateTime.Now.ToLongTimeString());
    
    else
        await next.Invoke();
});

app.Use(async (context, next) =>
{
    if (context.Request.Path.Value.StartsWith("/stats"))
        foreach (var item in UrlHandler.validUrlArray)
        {
            await context.Response.WriteAsync(item.Key + ":" + item.Value + Environment.NewLine);
        }

    else
        await next.Invoke();
});

app.Use(async (context, next) =>
{
    if (context.Request.Path.Value.StartsWith("/404"))
        foreach(var item in UrlHandler.invalidUrls)
        {
            await context.Response.WriteAsync(item + Environment.NewLine);
        }

    else
        await next.Invoke();
});

app.Use(async (context, next) =>
{
    var filePath = context.Request.Path.Value;

    var fullPath = Path.Join(builder.Environment.ContentRootPath, filePath);
    if (File.Exists(fullPath))
    {
        await context.Response.WriteAsync(File.ReadAllText(fullPath));
    }
    else
        await next.Invoke();
});

app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{num1?}/{num2?}");

app.Run();


public static class UrlHandler
{
    public static Dictionary<string, int> validUrlArray = new Dictionary<string, int>();

    public static List<string> invalidUrls = new List<string>();
}

