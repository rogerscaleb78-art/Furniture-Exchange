var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// The HTML/JS front end is opened straight from disk, so the API accepts
// requests from any origin.
builder.Services.AddCors(options =>
{
    options.AddPolicy("OpenPolicy", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Swagger UI for exploring the API (development only).
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// CORS must run before authorization so browser preflight requests succeed.
app.UseCors("OpenPolicy");
app.UseAuthorization();

app.MapControllers();

app.Run();
