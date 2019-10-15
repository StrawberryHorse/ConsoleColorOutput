using System;
using ConsoleColorOutput;

namespace TestCCO
{
    class Program
    {
        static void Main(string[] args)
        {
            var ou = new ConsoleOutputHelper();

            // First test case: we need to change to color with the {color} syntax
            ou.WriteLine("{yellow}Hello World!{}");

            // Second test case: headings
            ou.WriteLine("# This should be white as it is something important.");

            // Third test case: wrapping 
            ou.WriteLine(@"This is written in multiple
lines in the source code. But a single line break should
not break the output into multiple lines. Instead, the output
should flow around the console. So this multi line
message should fit inside the console, and get wrapped
accordingly.");

			// Fourth test case: inline colors
			ou.WriteLine(@"So let's color {cyan}something{} in the middle.");

			// Fifth test case
			ou.WriteLine(@"1 When we are creating many text.

2 And I mean many.

3 Many {yellow}many{} many.

4 We want this to pause when the console is full. 

5 Or display.

6 Whatever.

7 More text here.

8 More text here.

9 More text here.

10 More text here.

11 More text here.

12 More text here.

13 More text here.

14 More text here.

15 More text here. More text here. More text here. More text here. More text here. More text here. More text here.
More text here. More text here. More text here. More text here. More text here. More text here. More text here.

16 More text here.

17 More text here.

18 that should be enough. {green}DONE{}.");
        }
    }
}
