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

		void StartParagraph();

		void EndParagraph();

		void SetColor(OutputColor color);
		
		/// <summary>
		/// Output a block of text
		/// </summary>
		/// <param name="str"></param>
		void WriteText(string str);

		void WriteEmptyLine();
		
		/// <summary>
		/// Pause output
		/// </summary>
		void PauseOutput();
	}

	public enum OutputColor {
		Default = -1,
		
		Black = 0,
		
		Gray = 7,	
		
		Blue = 9,
		
		Green = 10,

		Cyan = 11,
		
		Red = 12,
		
		Yellow = 14,
		
		White = 15,
	}
}
