namespace Issue5.Model
{
	internal class Employee : Person, ICloneable, IMyCloneable<Employee>
	{
		public TimeSpan ShiftDuration { get; set; }

		public Employee( string name, int age, Vehicle transport, TimeSpan shiftDuration )
			: base( name, age, transport )
		{
			ShiftDuration = shiftDuration;
		}

		public new object Clone()
		{
			return DeepClone();
		}

		public new Employee DeepClone()
		{
			var transport = Transport.DeepClone();
			return new Employee( Name, Age, transport, ShiftDuration );
		}

		public override string ToString()
		{
			return $"{nameof( Id )}: {Id}; {nameof( Name )}: {Name}; {nameof( Age )}: {Age}; {nameof( ShiftDuration )}: {ShiftDuration}; {nameof( Transport )}: {Transport}";
		}
	}
}
