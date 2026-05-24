using TransactionSimulator.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<FeatureBuilder>();
builder.Services.AddSingleton<TransactionGenerator>();
builder.Services.AddHttpClient<MlPredictionService>(client =>
{
	client.BaseAddress = new Uri("https://shavonne-unvenereal-phil.ngrok-free.dev");
});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
