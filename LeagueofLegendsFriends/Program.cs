var builder = WebApplication.CreateBuilder(args);

// Add CORS policy to allow requests from your frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", builder =>
    {
        builder.WithOrigins("http://localhost:5174") // Your React app's address
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Register RiotApiService with HttpClient
builder.Services.AddHttpClient<RiotApiService>();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable CORS for the defined policy
app.UseCors("AllowReactApp");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "LeagueStatsAPI v1");
        c.RoutePrefix = string.Empty; // Serves Swagger UI at the root


    });
}

// Redirect HTTP to HTTPS
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.Run();
