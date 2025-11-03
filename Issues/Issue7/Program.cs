using System.Text.Json;
using Common;

namespace Issue7
{
	internal class Program
	{
		static void Main( string[] args )
		{
			F f = F.Get();

			var count = 1000000;

			CarryOutExperimant( f, count );
			CarryOutExperimantJson( f, count );

			Console.WriteLine();

			var person = new Person()
			{
				_name = "Tom",
				_age = 20,
				_weight = 55.5,
			};

			CarryOutExperimant( person, count );
			CarryOutExperimantJson( person, count );

			Console.ReadKey();
		}

		private static void CarryOutExperimant<T>( T obj, int iterations ) where T : new()
		{
			ArgumentNullException.ThrowIfNull( obj );

			var serializeExecutionTime = Tools.MeasureExecution( () =>
			{
				for ( int i = 0; i < iterations; i++ )
				{
					MySerializer.Serialize( obj );
				}
			} );

			var csvF = MySerializer.Serialize( obj );
			var deserializeExecutionTime = Tools.MeasureExecution( () =>
			{
				for ( int i = 0; i < iterations; i++ )
				{
					MySerializer.Deserialize<T>( csvF );
				}
			} );

			Console.WriteLine( $"Serialize: {serializeExecutionTime}" );
			Console.WriteLine( $"Deserialize: {deserializeExecutionTime}" );
		}

		private static void CarryOutExperimantJson<T>( T obj, int iterations ) where T : new()
		{
			ArgumentNullException.ThrowIfNull( obj );

			var options = new JsonSerializerOptions();
			options.IncludeFields = true;

			var serializeExecutionTime = Tools.MeasureExecution( () =>
			{
				for ( int i = 0; i < iterations; i++ )
				{
					JsonSerializer.Serialize( obj, options );
				}
			} );

			var csvF = JsonSerializer.Serialize( obj, options );
			var deserializeExecutionTime = Tools.MeasureExecution( () =>
			{
				for ( int i = 0; i < iterations; i++ )
				{
					JsonSerializer.Deserialize<T>( csvF, options );
				}
			} );

			Console.WriteLine( $"SerializeJson: {serializeExecutionTime}" );
			Console.WriteLine( $"DeserializeJson: {deserializeExecutionTime}" );
		}
	}
}
