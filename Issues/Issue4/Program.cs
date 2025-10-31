using System.Runtime.InteropServices;
using Common;

namespace Issue4
{
	internal class Program
	{
		/// <summary>
		/// Максимальное значение случайных значений в массиве
		/// Не должно быть очень большим, т.к. может привести к переполнению при суммировании значений массива
		/// </summary>
		private const int MaxRandomValue = 100;

		static void Main( string[] args )
		{
			ShowEnvironmentCharacteristics();

			var array1 = GetRandomArray( 100_000 );
			var array2 = GetRandomArray( 1_000_000 );
			var array3 = GetRandomArray( 10_000_000 );

			Console.WriteLine();
			Console.WriteLine( $"Array{null,-5} | singleThread{null,-3}  | multiThread{null,-5} | parallelLinq" );

			SumExperiment( array1 );
			SumExperiment( array2 );
			SumExperiment( array3 );

			Console.ReadKey();
		}

		private static void ShowEnvironmentCharacteristics()
		{
			Console.WriteLine( "=== Environment Information ===" );
			Console.WriteLine( $"Machine Name: {Environment.MachineName}" );
			Console.WriteLine( $"User Name: {Environment.UserName}" );
			Console.WriteLine( $"OS Version: {Environment.OSVersion}" );
			Console.WriteLine( $"Processor Count: {Environment.ProcessorCount}" );
			Console.WriteLine( $"Working Set: {Environment.WorkingSet / 1024 / 1024} MB" );
			Console.WriteLine( $"Is 64-bit OS: {Environment.Is64BitOperatingSystem}" );
			Console.WriteLine( $"Is 64-bit Process: {Environment.Is64BitProcess}" );
			Console.WriteLine( $"OS Architecture: {RuntimeInformation.OSArchitecture}" );
			Console.WriteLine( $"Process Architecture: {RuntimeInformation.ProcessArchitecture}" );
		}

		private static int[] GetRandomArray( int length )
		{
			Random random = new Random( DateTime.Now.Microsecond );
			var array = Enumerable.Range( 0, length )
				.Select( i => random.Next( MaxRandomValue ) )
				.ToArray();
			return array;
		}

		private static void SumExperiment( int[] array )
		{
			var singleThreadExecutionTime = Tools.MeasureExecution( () => SingleThreadSum( array ) );
			var multiThreadExecutionTime = Tools.MeasureExecution( () => MultiThreadSum( array ) );
			var parallelLinqExecutionTime = Tools.MeasureExecution( () => ParallelLinqSum( array ) );

			Console.WriteLine( $"{array.Length,-10} | {singleThreadExecutionTime} | {multiThreadExecutionTime} | {parallelLinqExecutionTime}" );
		}

		private static void SingleThreadSum( int[] array )
		{
			var sum = 0;
			foreach ( var item in array )
			{
				sum += item;
			}
		}

		private static async Task MultiThreadSum( int[] array )
		{
			var parts = Environment.ProcessorCount;
			var tasks = new List<Task<int>>();
			for ( int i = 1; i <= parts; i++ )
			{
				var from = array.Length / parts * ( i - 1 );
				var to = i == parts ?
					array.Length
					: array.Length / parts * i;
				var task = Task.Run<int>( () =>
				{
					var subSum = 0;
					for ( int j = from; j < to; j++ )
					{
						subSum += array[j];
					}
					return subSum;
				} );
				tasks.Add( task );
			}

			var sums = await Task.WhenAll( tasks );
			var sum = sums.Sum();
		}

		private static void ParallelLinqSum( int[] array )
		{
			var sum = array.AsParallel().Sum();
		}
	}
}
