using System;
using Lang.Lexer;
using CommandLine;

namespace Lang {
	public class Options {
		[Option ('f', "file", Required = true, HelpText = "Specifies the input file")]
		public string? File {get; set;} 
	}

	internal class Start {
		static Options res;
		static void Main(string[] args) {
			Parser.Default.ParseArguments<Options>(args)
			.WithParsed<Options>(o => res = o);
			if (res == null)
				Environment.Exit(-1);
			try {
				LexerMain lex = new LexerMain(res.File);
			}
			catch (Exception e) {
				Console.WriteLine(e.Message);
				Environment.Exit(-1);
			}
		}
	}
}
