namespace Issue5.Model
{
	internal class Manager : Person, ICloneable, IMyCloneable<Manager>
	{
		/// <summary>
		/// Целевой план
		/// </summary>
		public string Aim { get; set; }

		public Manager( string name, int age, Vehicle transport, string aim )
			: base( name, age, transport )
		{
			Aim = aim;
		}

		public new object Clone()
		{
			return DeepClone();
		}

		public new Manager DeepClone()
		{
			var transport = Transport.DeepClone();
			return new Manager( Name, Age, transport, Aim );
		}

		public override string ToString()
		{
			return $"{nameof( Id )}: {Id}; {nameof( Name )}: {Name}; {nameof( Age )}: {Age}; {nameof( Aim )}: {Aim}; {nameof( Transport )}: {Transport}";
		}
	}
}
