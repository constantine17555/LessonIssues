using Issue5.Model;

namespace Issue5
{
	internal class Program
	{
		static void Main( string[] args )
		{
			var directorCar = new Car( "Red", "Audi" );
			var director = new Director( "Tom", 35, directorCar, "Development" );

			Bike employeeBike = new Bike( "Black", 120 );
			var employee = new Employee( "Mike", 20, employeeBike, new TimeSpan( 8, 0, 0 ) );

			List<Person> persons = new List<Person>();
			for ( int i = 0; i < 10; i++ )
			{
				// создаём экземпляры клонированием прототипа 
				var newDirector = director.DeepClone();
				// явно указывает id каждому экземпляру - показывает, что это действиетльно разные объекты
				newDirector.Id = i;
				newDirector.Transport.Id = i + 5000;

				// создаём экземпляры клонированием прототипа 
				var newEmployee = employee.DeepClone();
				// явно указывает id каждому экземпляру - показывает, что это действиетльно разные объекты
				newEmployee.Id = i + 100;
				newEmployee.Transport.Id = i + 6000;

				persons.Add( newDirector );
				persons.Add( newEmployee );
			}

			foreach ( var person in persons )
			{
				Console.WriteLine( person );
			}

			Console.ReadKey();

			// Вывод
			// ICloneable
			// + стандартный, простой
			// - устарел, возвращает object - требует приведения типов
			// IMyCloneable<T>
			// + возвращает нужный тип
			// - требуется проработка собственного интерфейса
		}
	}
}
