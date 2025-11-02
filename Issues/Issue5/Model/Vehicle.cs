namespace Issue5.Model
{
	internal class Vehicle : ICloneable, IMyCloneable<Vehicle>
	{
		/// <summary>
		/// Не задаётся при создании экземпляра
		/// </summary>
		public int Id { get; set; }
		public string Color { get; set; }

		public Vehicle( string color )
		{
			Color = color;
		}

		public object Clone()
		{
			return DeepClone();
		}

		public virtual Vehicle DeepClone()
		{
			return new Vehicle( Color );
		}
	}
}
