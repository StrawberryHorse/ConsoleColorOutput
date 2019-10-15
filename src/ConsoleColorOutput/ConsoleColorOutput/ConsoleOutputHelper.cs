using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleColorOutput {
	public class ConsoleOutputHelper {
		private static Regex colorRegex = new Regex(@"^(?:{(?<colorBefore>red|yellow|cyan|blue|green|white|)})?(?<word>[^\s{}]+)(?:{(?<colorAfter>red|yellow|cyan|blue|green|white|)}(?<suffix>[,.!])?)?$");

		public void WriteLine(string originalText) {
			int consoleWidth = Console.BufferWidth;
			
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
					
					if (line.Length == 0) {
						if (currentLine.Length > 0 || charsInLine > 0) {
							System.Console.WriteLine(currentLine.ToString());
							linesWritten++;

							System.Console.WriteLine();
							linesWritten++;

							currentLine.Clear();
							charsInLine = 0;

							continue;
						}
					}
					
					// if this is a header line, print it out in white
					if (line[0] == '#') {
						line = line.Substring(1).Trim();
						Console.ForegroundColor = ConsoleColor.White;
						Console.WriteLine(line);
						linesWritten++;
						ResetColor();
						continue;
					}

					var words = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

					foreach (var currentWord in words) {
						string colorBefore = null, colorAfter = null, suffix = null;

						var w = currentWord;
						if (colorRegex.IsMatch(w)) {
							var m = colorRegex.Match(w);
							if (m.Groups["colorBefore"].Success) {
								colorBefore = m.Groups["colorBefore"].ToString();
							}
							if (m.Groups["colorAfter"].Success) {
								colorAfter = m.Groups["colorAfter"].ToString();
							}
							if (m.Groups["suffix"].Success) {
								suffix = m.Groups["suffix"].ToString();
							}
							w = m.Groups["word"].ToString();
						}

						if (colorBefore != null) {
							WriteCurrentLine(false);
							System.Console.ForegroundColor = ParseColorCode(colorBefore);
						}

						if (w.Length + currentLine.Length + charsInLine + 1 >= consoleWidth) {
							WriteCurrentLine(true);
						}

						currentLine.Append(w + (suffix == null ? " " : ""));

						if (colorAfter != null) {
							WriteCurrentLine(false);
							System.Console.ForegroundColor = ParseColorCode(colorAfter);

							if (suffix != null) {
								currentLine.Append(suffix + " ");
							}
						}
					}
				}

				if (currentLine.Length > 0 || linesWritten == 0) {
					WriteCurrentLine(true);
				}
			}

			void WriteCurrentLine(bool newLine) {
				if (newLine) {
					System.Console.WriteLine(currentLine.ToString());
					linesWritten++;
					charsInLine = 0;
				}
				else {
					System.Console.Write(currentLine.ToString());
					charsInLine += currentLine.Length;
				}
				currentLine.Clear();
			}
		}

		private static void ResetColor() {
			System.Console.ForegroundColor = ConsoleColor.Gray;
		}
		
		private ConsoleColor ParseColorCode(string s) {
			switch (s) {
				case "red":
					return ConsoleColor.Red;
				case "cyan":
					return ConsoleColor.Cyan;
				case "yellow":
					return ConsoleColor.Yellow;
				case "green":
					return ConsoleColor.Green;
				case "blue":
					return ConsoleColor.Blue;
				case "white":
					return ConsoleColor.White;
				default:
					return ConsoleColor.Gray;
			}
		}
	
	}
}
