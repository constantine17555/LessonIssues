namespace Issue8
{
	internal static class CollectionExtensions
	{
		/// <summary>
		/// Возвращает максимальный элемент по числовому параметру
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="collection"></param>
		/// <param name="getParameter">Функция, возвращающая параметр, по которому сравниваются элементы</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="ArgumentException"></exception>
		internal static T GetMax<T>( this IEnumerable<T> collection, Func<T, float> getParameter ) where T : class
		{
			if ( collection == null )
				throw new ArgumentNullException( nameof( collection ) );
			if ( !collection.Any() )
				throw new ArgumentException( "Пустая коллекция", nameof( collection ) );
			if ( getParameter == null )
				throw new ArgumentNullException( nameof( getParameter ) );

			var maxValue = float.MinValue;
			T maxItem = null;
			foreach ( var item in collection )
			{
				var value = getParameter( item );
				if ( value > maxValue )
				{
					maxValue = value;
					maxItem = item;
				}
			}

			return maxItem!;
		}
	}
}
