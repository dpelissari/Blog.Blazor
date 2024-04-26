using Blog.Blazor.Components;
using Blog.Blazor.Data;
using Blog.Blazor.Interfaces;
using Blog.Blazor.Services;

var builder = WebApplication.CreateBuilder(args);

// conection string
var connectionString = builder.Configuration.GetConnectionString("Blog") ?? "Data Source=Blog.db";

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// sqlite
builder.Services.AddSqlite<AplicacaoDbContexto>(connectionString);


// configurando a injeção de dependencia dos nossos serviços
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IAutorService, AutorService>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();
app.UseStatusCodePagesWithRedirects("/404");

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
