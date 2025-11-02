namespace Issue6
{
	/// <summary>
	/// Возвращает значение близкие к максимуму
	/// </summary>
	internal class MaxSimpleNumberGenerator : SimpleNumberGenerator
	{
		public new int GetRandomNumber( int minValue, int maxValue )
		{
			var first = base.GetRandomNumber( minValue, maxValue );
			var second = base.GetRandomNumber( minValue, maxValue );

			return Math.Max( first, second );
		}
	}
}
