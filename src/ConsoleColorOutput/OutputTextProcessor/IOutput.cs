namespace OutputTextProcessor {
	/// <summary>
	/// Abstract away the output medium from the text processor
	/// </summary>
	public interface IOutput {
		
		/// <summary>
		/// Return the width of the output medium for wrapping
		/// </summary>
		int Width { get; }
		
		/// <summary>
		/// Return the height of the output medium for pausing output
		/// </summary>
		int Height { get; }

		/// <summary>
		/// Output a block of text
		/// </summary>
		/// <param name="str"></param>
		void Output(string str);
		
		/// <summary>
		/// Pause output
		/// </summary>
		void PauseOutput();
	}
}
