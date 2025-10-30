using System.Diagnostics;

namespace Issue3
{
	internal class Program
	{
		async static Task Main( string[] args )
		{
			try
			{
				Stopwatch stopwatch = new Stopwatch();
				stopwatch.Start();

				var folderPath = "Files";

				var whitespaceCount = await GetWhitespaceCountInDirectory( folderPath );

				stopwatch.Stop();

				Console.WriteLine( $"Whitespace count: {whitespaceCount}" );
				Console.WriteLine( $"Execution time: {stopwatch.Elapsed}" );
			}
			catch ( Exception exc )
			{
				Console.WriteLine( exc.ToString() );
			}

			Console.ReadKey();
		}

		/// <summary>
		/// Возвращает количество пробелов в файлых указанной директории
		/// </summary>
		/// <param name="localDirectoryPath"></param>
		/// <returns></returns>
		private static async Task<int> GetWhitespaceCountInDirectory( string localDirectoryPath )
		{
			ArgumentNullException.ThrowIfNullOrWhiteSpace( localDirectoryPath );

			var filePaths = Directory.GetFiles( localDirectoryPath );

			var tasks = filePaths.Select( x => GetWhitespaceCountInFile( x ) );

			var results = await Task.WhenAll( tasks );

			var whitespaceCount = results.Sum();

			return whitespaceCount;
		}

		/// <summary>
		/// Возвращает количество пробелов в указанном файле
		/// </summary>
		/// <param name="localFilePath"></param>
		/// <returns></returns>
		private async static Task<int> GetWhitespaceCountInFile( string localFilePath )
		{
			using ( var reader = new StreamReader( localFilePath ) )
			{
				var content = await reader.ReadToEndAsync();
				var whitespaceCount = GetWhitespaceCount( content );
				return whitespaceCount;
			}
		}

		/// <summary>
		/// Возвращает количество пробелов в тексте
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static int GetWhitespaceCount( string text )
		{
			if ( string.IsNullOrEmpty( text ) )
				return 0;

			var count = 0;
			foreach ( var @char in text )
			{
				if ( @char == ' ' )
					count++;
			}

			return count;
		}
	}
}
