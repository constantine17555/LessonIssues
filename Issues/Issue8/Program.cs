namespace Issue8
{
	internal class Program
	{
		static void Main( string[] args )
		{
			// 1
			List<Person> persons = new List<Person>();
			Random random = new Random( DateTime.Now.Microsecond );
			for ( int i = 0; i < 10; i++ )
			{
				persons.Add( new Person( random.Next( 100 ) ) );
			}

			var oldest = persons.GetMax( x => x.Age );

			Console.WriteLine( $"Max age: {oldest.Age}" );

			// 2
			var fileSearcher = new FileSearcher();
			fileSearcher.FileFound += OnFileSearcherFileFound;

			var currentDir = Directory.GetCurrentDirectory();
			var pathDir = Path.Combine( currentDir, "Files" );
			fileSearcher.Search( pathDir );

			fileSearcher.FileFound -= OnFileSearcherFileFound;

			Console.ReadLine();
		}

		private static void OnFileSearcherFileFound( object? sender, SearchArgs args )
		{
			var fileName = args.FilePath.Split( '\\' ).Last();
			if ( fileName == "File02.txt" )
			{
				// искомый файл найден
				Console.WriteLine( "File found" );
				args.IsFinished = true;
			}
		}
	}
}
