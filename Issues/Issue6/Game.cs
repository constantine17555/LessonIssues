using Microsoft.Extensions.Options;

namespace Issue6
{
	internal class Game
	{
		private readonly INumberGenerator _numberGenerator;
		private readonly IUiAgent _uiAgent;
		private readonly AppSettings _appSettings;

		public Game( INumberGenerator numberGenerator, IUiAgent uiAgent, IOptions<AppSettings> options )
		{
			_numberGenerator = numberGenerator;
			_uiAgent = uiAgent;
			_appSettings = options.Value;
		}

		/// <summary>
		/// Выполняет логику игры
		/// </summary>
		internal void Run()
		{
			var isGuessed = false;
			var targetNumber = _numberGenerator.GetRandomNumber( _appSettings.MinRandomValue, _appSettings.MaxRandomValue );
			int attemptsLeft = _appSettings.MaxAttempts;

			while ( !isGuessed )
			{
				if ( attemptsLeft <= 0 )
				{
					_uiAgent.Output( $"You lose!" );
					break;
				}

				_uiAgent.Output( $"Guess number (attempts left: {attemptsLeft})" );
				attemptsLeft--;
				var input = _uiAgent.Input();
				if ( !int.TryParse( input, out var userNumber ) )
				{
					Console.WriteLine( "It isn't number" );
					continue;
				}

				isGuessed = userNumber == targetNumber;
				var answer = userNumber switch
				{
					_ when userNumber > targetNumber => "Less",
					_ when userNumber < targetNumber => "More",
					_ => "You win!"
				};

				_uiAgent.Output( answer );
			}
		}
	}
}
