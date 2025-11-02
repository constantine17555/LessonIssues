namespace Issue5.Model
{
	internal class Bike : Vehicle, ICloneable, IMyCloneable<Bike>
	{
		public int MaxSpeed { get; }

		public Bike( string color, int maxSpeed )
			: base( color )
		{
			MaxSpeed = maxSpeed;
		}

		object ICloneable.Clone()
		{
			return DeepClone();
		}

		public  override Bike DeepClone()
		{
			return new Bike( Color, MaxSpeed );
		}

		public override string ToString()
		{
			return $"{nameof( Id )}: {Id}; {nameof( Color )}: {Color}; {nameof( MaxSpeed )}: {MaxSpeed}";
		}
	}
}
