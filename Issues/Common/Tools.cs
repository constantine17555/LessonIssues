using System.Diagnostics;

namespace Common
{
	public class Tools
	{
		/// <summary>
		/// Выполняет функцию и измеряет время выполнения
		/// </summary>
		/// <param name="action">Функция</param>
		/// <returns>Время выполнения</returns>
		public static TimeSpan MeasureExecution( Action action )
		{
			ArgumentNullException.ThrowIfNull( action );
			Stopwatch stopwatch = Stopwatch.StartNew();
			action();
			stopwatch.Stop();
			return stopwatch.Elapsed;
		}
	}
}
