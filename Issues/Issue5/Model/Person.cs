namespace Issue5.Model
{
	internal class Person : ICloneable, IMyCloneable<Person>
	{
		/// <summary>
		/// Не задаётся при создании экземпляра
		/// </summary>
		public int Id { get; set; }
		public string Name { get; set; }
		public int Age { get; set; }
		public Vehicle Transport { get; set; }

		public Person( string name, int age, Vehicle transport )
		{
			Name = name;
			Age = age;
			Transport = transport;
		}

		public object Clone()
		{
			return DeepClone();
		}

		public Person DeepClone()
		{
			return new Person( Name, Age, Transport );
		}
	}
}
