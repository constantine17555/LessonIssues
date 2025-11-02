namespace Issue6
{
	internal class SimpleNumberGenerator : INumberGenerator
	{
		public int GetRandomNumber( int minValue, int maxValue )
		{
			var random = new Random( DateTime.Now.Millisecond );
			var targetNumber = random.Next( minValue, maxValue );
			return targetNumber;
		}
	}
}
