namespace Issue8
{
	internal class FileSearcher
	{
		internal event EventHandler<SearchArgs> FileFound;

		internal void Search( string directoryPath )
		{
			ArgumentNullException.ThrowIfNullOrWhiteSpace( directoryPath );
			if ( !Directory.Exists( directoryPath ) )
				throw new ArgumentException( $"Каталог не найден по пути {directoryPath}" );

			var filePaths = Directory.GetFiles( directoryPath );
			foreach ( var filePath in filePaths )
			{
				var searchArgs = FileFoundNotify( filePath );

				if ( searchArgs.IsFinished )
				{
					break;
				}
			}
		}

		private SearchArgs FileFoundNotify( string filePath )
		{
			var searchArgs = new SearchArgs( filePath );
			try
			{
				FileFound?.Invoke( this, searchArgs );
			}
			catch ( Exception exc )
			{
				// log
			}
			return searchArgs;
		}
	}
}
