using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace OutputTextProcessor {
	/// <summary>
	/// Process a text and send to output medium
	/// </summary>
	public class OutputTextProcessor {
		private readonly IOutput output;
		private static readonly Regex colorRegex = new Regex(@"^(?:{(?<colorBefore>red|yellow|cyan|blue|green|white|)})?(?<word>[^\s{}]+)(?:{(?<colorAfter>red|yellow|cyan|blue|green|white|)}(?<suffix>[,.!])?)?$");

		public OutputTextProcessor(IOutput output) {
			this.output = output;
		}

		public void WriteLine(string originalText) {
			int consoleWidth = output.Width;
			int consoleHeight = output.Height;
			
			var currentLine = new StringBuilder(120);

			int linesWritten = 0;
			int charsInLine = 0;
			int lastPause = 0;

			bool insideParagraph = false;

			output.SetColor(OutputColor.Default);

			using (var sr = new StringReader(originalText)) {
				while (true) {
					var line = sr.ReadLine();
					if (line == null) break;

					line = line.Trim();
					
					if (line.Length == 0) {
						if (currentLine.Length > 0 || charsInLine > 0) {
							BeginParagraphIfNeeded();
							output.WriteText(currentLine.ToString());
							output.EndParagraph();
							linesWritten++;
							PauseAfterScreenful();

							output.WriteEmptyLine();
							linesWritten++;
							PauseAfterScreenful();

							currentLine.Clear();
							charsInLine = 0;

							continue;
						}
					}
					
					// if this is a header line, print it out in white
					if (line[0] == '#') {
						line = line.Substring(1).Trim();
						output.StartParagraph();
						output.SetColor(OutputColor.White);
						output.WriteText(line + "\n");
						output.SetColor(OutputColor.Default);
						output.EndParagraph();

						linesWritten++;
						PauseAfterScreenful();
						
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
							output.SetColor(ParseColorCode(colorBefore));
						}

						if (w.Length + currentLine.Length + charsInLine + 1 >= consoleWidth) {
							WriteCurrentLine(true);
						}

						currentLine.Append(w + (suffix == null ? " " : ""));

						if (colorAfter != null) {
							WriteCurrentLine(false);
							output.SetColor(ParseColorCode(colorAfter));

							if (suffix != null) {
								currentLine.Append(suffix + " ");
							}
						}
					}
				}

				if (currentLine.Length > 0 || linesWritten == 0) {
					WriteCurrentLine(true);
					output.EndParagraph();
				}
			}

			void BeginParagraphIfNeeded() {
				if (!insideParagraph) {
					output.StartParagraph();
					insideParagraph = true;
				}
			}
			
			void WriteCurrentLine(bool newLine) {
				BeginParagraphIfNeeded();

				if (newLine) {
					output.WriteText(currentLine.ToString() + "\n");
					linesWritten++;
					PauseAfterScreenful();
					charsInLine = 0;
				}
				else {
					output.WriteText(currentLine.ToString());
					charsInLine += currentLine.Length;
				}
				currentLine.Clear();
			}
			
			void PauseAfterScreenful() {
				if (linesWritten - lastPause + 2 > consoleHeight) {
					lastPause = linesWritten;
					output.PauseOutput();
				}
			}
		}
		
		private OutputColor ParseColorCode(string s) {
			switch (s) {
				case "red":
					return OutputColor.Red;
				case "cyan":
					return OutputColor.Cyan;
				case "yellow":
					return OutputColor.Yellow;
				case "green":
					return OutputColor.Green;
				case "blue":
					return OutputColor.Blue;
				case "white":
					return OutputColor.White;
				default:
					return OutputColor.Default;
			}
		}
		
	}
}
