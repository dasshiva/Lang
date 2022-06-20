using System;
using CommandLine;

namespace Lang {
	public class Options {
		[Option ('f', "file", Required = true, HelpText = "Specifies the input file")]
		public string? file {get; set;} 
	}

	internal class Start {
		static void Main(string[] args) {
			Parser.Default.ParseArguments<Options>(args)
				.WithParsed(CheckOpts);
		}

		private static void CheckOpts(Options o) {
		}
	}
}
