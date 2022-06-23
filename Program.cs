using System;
using Lexer;
using CommandLine;

namespace Lang {
	public class Options {
		[Option ('f', "file", Required = true, HelpText = "Specifies the input file")]
		public string? File {get; set;} 
	}

	internal class Start {
		static string? file;
		static void Main(string[] args) {
			Parser.Default.ParseArguments<Options>(args)
			.WithParsed<Options>(ProcessOpts);
			LexerMain lex = new LexerMain(file);
			while (!lex.End) {
				Console.WriteLine(lex.Next().ToString());
			}
		}

		static void ProcessOpts (Options o) {
			file = o.File;
		}
	}
}
