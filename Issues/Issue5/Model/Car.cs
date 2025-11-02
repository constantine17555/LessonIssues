namespace Issue5.Model
{
	internal class Car : Vehicle, ICloneable, IMyCloneable<Car>
	{
		public string Model { get; set; }

		public Car( string color, string model )
			: base( color )
		{
			Model = model;
		}

		object ICloneable.Clone()
		{
			return DeepClone();
		}

		public override Car DeepClone()
		{
			return new Car( Color, Model );
		}

		public override string ToString()
		{
			return $"{nameof( Id )}: {Id}; {nameof( Color )}: {Color}; {nameof( Model )}: {Model}";
		}
	}
}
