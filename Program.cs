using System;
using System.IO;
using CommandLine;

namespace Lang {
	public class Options {
		[Option ('f', "file", Required = true, HelpText = "Specifies the input file")]
		public string File {get; set;} 
	}

	internal class Start {
		static StringReader? fs = null;
		static void Main(string[] args) {
			Parser.Default.ParseArguments<Options>(args)
				.WithParsed(CheckOpts);
			
		}

		private static void CheckOpts(Options o) {
			try {
				fs = new StringReader(o.File);
			} catch (Exception e) {
				Console.WriteLine(e.Message);
				Environment.Exit(-1);
			}
		}
	}
}
