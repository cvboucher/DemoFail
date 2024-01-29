using DemoFail.Shared;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;



static IEdmModel GetEdmModel()
{
    var builder = new ODataConventionModelBuilder();
    builder.Namespace = "Sirrus.Client";
    var orgUnits = builder.EntitySet<OrgUnit>("OrgUnits");
    builder.EntitySet<Manager>("Managers");
    builder.EntitySet<Location>("Locations");

    return builder.GetEdmModel();
}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews().AddOData(opt => { opt.Select().Filter().Count().OrderBy().Expand().SetMaxTop(10000).AddRouteComponents("odata", GetEdmModel()); });
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

//app.UseCors();
app.UseCors(x => x
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed(origin => true) // allow any origin
                    .AllowCredentials()); // allow credentials

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
