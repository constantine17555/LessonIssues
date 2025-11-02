namespace Issue5.Model
{
	internal class Director : Person, ICloneable, IMyCloneable<Director>
	{
		public string Department { get; set; }

		public Director( string name, int age, Vehicle transport, string departmen )
			: base( name, age, transport )
		{
			Department = departmen;
		}

		public new object Clone()
		{
			return DeepClone();
		}

		public new Director DeepClone()
		{
			var transport = Transport.DeepClone();
			return new Director( Name, Age, transport, Department );
		}

		public override string ToString()
		{
			return $"{nameof( Id )}: {Id}; {nameof( Name )}: {Name}; {nameof( Age )}: {Age}; {nameof( Department )}: {Department}; {nameof( Transport )}: {Transport}";
		}
	}
}
