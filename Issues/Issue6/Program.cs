using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Issue6
{
	internal class Program
	{
		static void Main( string[] args )
		{
			var host = Host
				.CreateDefaultBuilder( args )
				.ConfigureServices( ( context, services ) =>
				{
					services.AddScoped<Game>();
					services.AddScoped<IUiAgent, UiConsoleAgent>();
					services.AddScoped<INumberGenerator, SimpleNumberGenerator>();
					services.Configure<AppSettings>( context.Configuration.GetSection( "AppSettings" ) );
				} )
				.Build();

			var service = host.Services.GetRequiredService<Game>();
			service.Run();
		}

		// Описание
		// Single responsibility
		// Типы UiAgent, NumberGenerator, Game имею каждый свою зону ответственности

		// Open/Closed Principle
		// Можно добавлять новые способы взаимодействия с ползователей, реализуюя IUiAgent. Текущие реализации при этом не меняются

		// Liskov Substitution Principle (LSP)
		// Если заменить SimpleNumberGenerator на наследник MaxSimpleNumberGenerator, то логика не будет противоречить начальной

		// Interface Segregation Principle( ISP)
		// Нет интерфейсов, объединяющих функциональность, которую не может поддерживать один объект

		// Dependency Inversion Principle( DIP)
		// Используется DI контейнер Microsoft.Extensions.DependencyInjection, зависимости передаются через конструктор
	}
}
