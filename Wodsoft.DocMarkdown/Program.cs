using Blazorise;
using Blazorise.Icons.Material;
using Blazorise.Material;
using Blazorise.TreeView;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Net.Http.Json;
using Wodsoft.DocMarkdown;
using Wodsoft.DocMarkdown.Renderers;
using Wodsoft.DocMarkdown.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var httpClient = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
var configResponse = await httpClient.GetAsync("config.json");
if (!configResponse.IsSuccessStatusCode)
    throw new FileNotFoundException("Could not load config.json.");
var config = await configResponse.Content.ReadFromJsonAsync<DocConfig>();
HttpClient docClient;
string baseUrl;
if (config!.BaseUrl == "")
    baseUrl = builder.HostEnvironment.BaseAddress;
else
{
    baseUrl = config!.BaseUrl;
    if (!baseUrl.EndsWith("/"))
        baseUrl += "/";
}
docClient = new HttpClient { BaseAddress = new Uri(baseUrl) };
var navManager = new NavManager(docClient, config.Path);
var langManager = new LanguageManager(config!.Languages);
var verManager = new VersionManager(config!.Versions);
var catalogManager = new CatalogManager();
var engine = new DocEngine(navManager, langManager, verManager, catalogManager, config!, docClient);
engine.AddRenderer(new ParagraphBlockRenderer());
engine.AddRenderer(new LiteralInlineRenderer());
engine.AddRenderer(new LineBreakInlineRenderer());
engine.AddRenderer(new EmphasisInlineRenderer());
engine.AddRenderer(new CodeInlineRenderer());
engine.AddRenderer(new LinkInlineRenderer());
engine.AddRenderer(new AutolinkInlineRenderer());
engine.AddRenderer(new HeadingBlockRenderer());
engine.AddRenderer(new FencedCodeBlockRenderer());
engine.AddRenderer(new QuoteBlockRenderer());
engine.AddRenderer(new ListBlockRenderer());
engine.AddRenderer(new ListItemBlockRenderer());
engine.AddRenderer(new TableRenderer());
engine.AddRenderer(new CodeBlockRenderer());
engine.AddRenderer(new HtmlBlockRenderer());
engine.AddRenderer(new HtmlInlineRenderer());
engine.AddRenderer(new HtmlEntityInlineRenderer());
engine.AddRenderer(new ThematicBreakBlockRenderer());
builder.Services.AddSingleton(navManager);
builder.Services.AddSingleton(engine);
builder.Services.AddSingleton(langManager);
builder.Services.AddSingleton(verManager);
builder.Services.AddSingleton(httpClient);
builder.Services.AddSingleton(catalogManager);
builder.Services.AddBlazorise(options =>
{
    options.Immediate = true;
})
    .AddMaterialProviders()
    .AddMaterialIcons();
await builder.Build().RunAsync();
