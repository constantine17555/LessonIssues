namespace Issue6
{
	/// <summary>
	/// Агент для взаимодействия с пользователем
	/// </summary>
	internal interface IUiAgent
	{
		/// <summary>
		/// Передать сообщение пользователю
		/// </summary>
		/// <param name="message"></param>
		public void Output( string message );

		/// <summary>
		/// Получить сообщение от пользователя
		/// </summary>
		/// <returns></returns>
		public string? Input();
	}
}
