using System;
using OutputTextProcessor;

namespace ConsoleColorOutput
{
    public class ConsoleOutput : IOutput
    {
	    public ConsoleOutput() {
		    
	    }


	    public int Width => System.Console.BufferWidth;

	    public int Height => System.Console.WindowHeight;

	    public void Output(string str) {
		    Console.WriteLine(str);
	    }

	    public void PauseOutput() {
	    }	    
    }
}
