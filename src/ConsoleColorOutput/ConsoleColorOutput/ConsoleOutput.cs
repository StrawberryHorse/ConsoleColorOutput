using System;
using OutputTextProcessor;

namespace ConsoleColorOutput
{
    public class ConsoleOutput : IOutput
    {
	    public ConsoleOutput() {}
	    
	    public int Width => System.Console.BufferWidth;

	    public int Height => System.Console.WindowHeight;
	    
	    public void StartParagraph() {
			// noop
	    }

	    public void EndParagraph() {
		    // noop
	    }

	    public void SetColor(OutputColor color) {
		    var consoleColor = Convert(color);
		    Console.ForegroundColor = consoleColor;

		    ConsoleColor Convert(OutputColor outputColor) {
			    switch (outputColor) {
				    case OutputColor.Gray:
				    case OutputColor.Black:
				    case OutputColor.Default:
					    return ConsoleColor.Gray;
				    case OutputColor.Blue:
					    return ConsoleColor.Blue;
				    case OutputColor.Green:
					    return ConsoleColor.Green;
				    case OutputColor.Cyan:
					    return ConsoleColor.Cyan;
				    case OutputColor.Red:
					    return ConsoleColor.Red;
				    case OutputColor.Yellow:
					    return ConsoleColor.Yellow;
				    case OutputColor.White:
					    return ConsoleColor.White;
				    default:
					    throw new ArgumentOutOfRangeException(nameof(outputColor), outputColor, null);
			    }
		    }
	    }

	    public void WriteText(string str) {
		    Console.Write(str);
	    }

	    public void WriteEmptyLine() {
		    Console.WriteLine();
	    }

	    public void PauseOutput() {
		    System.Console.Write("--more--");
		    System.Console.ReadKey();
		    System.Console.Write("\r            \r");
	    }	    
    }
}
