using AdPlatform.Services;

var builder = WebApplication.CreateBuilder(args);

// Добавление сервисов в контейнер зависимостей
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Регистрация сервиса
builder.Services.AddSingleton<IAdService, AdService>();

var app = builder.Build();

// Конфигурация HTTP конвейера
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();