namespace Issue8
{
	internal class SearchArgs : EventArgs
	{
		public string FilePath { get; set; }
		public bool IsFinished { get; set; }

		public SearchArgs( string filePath )
		{
			FilePath = filePath;
		}
	}
}
