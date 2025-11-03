using System.Text;

namespace Issue7
{
	/// <summary>
	/// Сериализует/десериализует объект с публичными полями
	/// </summary>
	internal class MySerializer
	{
		private const char Separator = ';';

		/// <summary>
		/// Возвращает строку в csv формате с разделителем ";"
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static string Serialize( object obj )
		{
			var type = obj.GetType();
			var fields = type.GetFields();
			var names = fields.Select( x => x.Name );

			var stringBuilder = new StringBuilder();
			stringBuilder.AppendLine( string.Join( Separator, names ) );

			var values = fields
				.Select( x => x.GetValue( obj ) )
				.ToList();
			stringBuilder.AppendLine( string.Join( Separator, values ) );


			foreach ( var field in fields )
			{
				var value = field.GetValue( obj );
			}

			return stringBuilder.ToString();
		}

		public static T Deserialize<T>( string data ) where T : new()
		{
			try
			{
				ArgumentNullException.ThrowIfNullOrWhiteSpace( data );
				var fieldValues = ParseData( data );

				var result = new T();
				var type = result.GetType();
				var fields = type.GetFields();

				foreach ( var fieldValue in fieldValues )
				{
					// определяем поле по имени
					var field = fields.Single( x => x.Name == fieldValue.Key );
					var value = ConvertValue( fieldValue.Value, field.FieldType );

					field.SetValue( result, value );
				}

				return result;
			}
			catch ( Exception exc )
			{
				throw new Exception( "Ошибка при десериализации", exc );
			}
		}

		private static object ConvertValue( string value, Type type )
		{
			object result = type switch
			{
				_ when type == typeof( int ) => int.Parse( value ),
				_ when type == typeof( double ) => double.Parse( value ),
				_ when type == typeof( string ) => value,
				_ => throw new NotSupportedException( $"Unknown type {type}" )
			};

			return result;
		}

		private static Dictionary<string, string> ParseData( string data )
		{
			var rows = data.Split( "\r\n", StringSplitOptions.RemoveEmptyEntries );
			if ( rows.Length != 2 )
			{
				throw new ArgumentException( "Wrong format: row count", nameof( data ) );
			}

			var fieldNames = rows[0].Split( Separator );
			if ( fieldNames.Any( x => x.Contains( ' ' ) ) )
			{
				throw new ArgumentException( "Wrong format: contain whitespace", nameof( data ) );
			}

			var values = rows[1].Split( Separator );

			if ( fieldNames.Length != values.Length )
			{
				throw new ArgumentException( "Wrong format: value count difference", nameof( data ) );
			}

			var result = new Dictionary<string, string>();
			for ( int i = 0; i < fieldNames.Length; i++ )
			{
				result[fieldNames[i]] = values[i];
			}

			return result;
		}
	}
}
