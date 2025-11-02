namespace Issue6
{
	/// <summary>
	/// Агент для взаимодействия с пользователем через консоль
	/// </summary>
	internal class UiConsoleAgent : IUiAgent
	{
		/// <inheritdoc/>
		public void Output( string message )
		{
			Console.WriteLine( message );
		}

		/// <inheritdoc/>>
		public string? Input()
		{
			return Console.ReadLine();
		}
	}
}
