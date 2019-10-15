using System;
using System.IO;
using System.Text;

namespace ConsoleColorOutput {
	public class ConsoleOutputHelper {
		public void WriteLine(string originalText) {
			var currentLine = new StringBuilder(120);

			int linesWritten = 0;
			int charsInLine = 0;
			int lastPause = 0;

			ResetColor();

			using (var sr = new StringReader(originalText)) {
				while (true) {
					var line = sr.ReadLine();
					if (line == null) break;

					line = line.Trim();

					// if this is a header line, print it out in white
					if (line[0] == '#') {
						line = line.Substring(1).Trim();
						Console.ForegroundColor = ConsoleColor.White;
						Console.WriteLine(line);
						ResetColor();
						continue;
					}

					Console.WriteLine(line);
				}
			}
		}

		private static void ResetColor() {
			System.Console.ForegroundColor = ConsoleColor.Gray;
		}
	}
}
