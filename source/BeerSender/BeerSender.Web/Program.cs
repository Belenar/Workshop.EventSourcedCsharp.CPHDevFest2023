using BeerSender.Domain.Infrastructure;
using BeerSender.Web.Event_store;
using BeerSender.Web.Hubs;
using BeerSender.Web.JsonHelpers;
using BeerSender.Web.Projections;
using BeerSender.Web.Read_store;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSignalR();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new Command_converter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<Event_context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Events"));
});
builder.Services.AddDbContext<Read_context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Read_store"));
});
builder.Services.AddScoped<Event_service>();
builder.Services.AddScoped<Command_router>(sp =>
    {
        var event_service = sp.GetRequiredService<Event_service>();
        var router = new Command_router(
            event_service.Get_events,
            event_service.Save_event);
        return router;
    });

builder.Services.AddScoped<Box_overview_projection>();
builder.Services.AddHostedService<Projection_service<Box_overview_projection>>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
} else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapHub<EventHub>("/event-hub");

app.Run();
