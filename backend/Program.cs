using backend.Repository;
using backend.Graphql;
using backend.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var context = builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("lib"));
});

builder.Services.AddScoped<BookService,BookService>();
builder.Services.AddTransient<BookEventPublisher,BookEventPublisher>();
builder.Services.AddScoped<BookMutation, BookMutation>();
builder.Services.AddScoped<BookQuery, BookQuery>();

builder.Services
    .AddCors()
    .AddGraphQLServer()
    .AddQueryType<BookQuery>()
    .AddMutationType<BookMutation>()
    .AddSubscriptionType<BookSubscription>()
    .AddInMemorySubscriptions();

var app = builder.Build();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials());

app.UseWebSockets();
app.MapGraphQL();

app.Run();