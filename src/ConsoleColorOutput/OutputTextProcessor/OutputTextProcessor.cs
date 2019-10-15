using System;

namespace OutputTextProcessor {
	/// <summary>
	/// Process a text and send to output medium
	/// </summary>
	public class OutputTextProcessor {
		private readonly IOutput _output;

		public OutputTextProcessor(IOutput output) {
			_output = output;
		}

		public void WriteLine(string text) {
			_output.Output(text);
		}
	}
}
