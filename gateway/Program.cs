var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient(SchemaNames.Library, c => c.BaseAddress = new Uri("http://localhost:5220/api"));

builder.Services
    .AddGraphQLServer()
    .AddRemoteSchema(SchemaNames.Library);

var app = builder.Build();

app.MapGraphQL("/api");
app.Run();

class SchemaNames
{  
    public const string Library = "library";
}