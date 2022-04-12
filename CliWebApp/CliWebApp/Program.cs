using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.SetValidPaths(new string[] {"/books", "/time", "/stats" });

app.HandleUrl();

app.ShowStats();

app.ShowErrorUrls();

app.Run();


public static class UrlRequestCounter
{
    private static Dictionary<string, int> urlTracker = new Dictionary<string, int>();

    private static string[] validUrlArray = new string [] { };

    private static List<string> invalidUrls = new List<string>();

    public static void SetValidPaths(this IApplicationBuilder app, string[] validPaths)
    {
        validUrlArray = validPaths;
    }

    public static void HandleUrl(this IApplicationBuilder app)
    {
        app.Use(async (context, next) =>
        {
            if (validUrlArray.Any(x => context.Request.Path.Value.ToLower().StartsWith(x)))
            {
                await context.Response.WriteAsync(context.Request.Path.Value.ToUpper() + Environment.NewLine);

                if (urlTracker.Keys.Any(x => x.ToLower() == context.Request.Path.Value.ToLower()))
                    urlTracker[context.Request.Path.Value.ToLower()]++;
                else
                    urlTracker.Add(context.Request.Path.Value.ToLower(), 1);
            }
            else
            {
                if(!invalidUrls.Contains(context.Request.Path.Value.ToLower()))
                    invalidUrls.Add(context.Request.Path.Value.ToLower());
            }
            
            next.Invoke();
        });   
    }

    public static void ShowStats(this IApplicationBuilder app)
    {
        app.Use(async (context, next) =>
        {
            if (context.Request.Path.Value.ToLower().StartsWith("/stats"))
            {
                foreach(var item in urlTracker)
                {
                    await context.Response.WriteAsync(item.Key.ToLower() + ":" + item.Value + Environment.NewLine);
                }
            }

            next.Invoke();
        });
    }

    public static void ShowErrorUrls(this IApplicationBuilder app)
    {
        app.Use(async (context, next) =>
        {
            if (context.Request.Path.Value.ToLower().StartsWith("/404"))
            {
                foreach (var item in invalidUrls)
                {
                    await context.Response.WriteAsync(item + Environment.NewLine);
                }
            }

            next.Invoke();
        });
    }
}
