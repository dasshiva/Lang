using System;
using System.IO;
using System.Text;

namespace Lang.Lexer {
    public enum LexerTokens {
        T_INT, 
        T_DECIMAL, 
        T_STRING,
        T_EOF
    }

    public readonly record struct LexerResult (LexerTokens Tk, int INum, double DNum, string? Str) {
        public LexerResult(LexerTokens Tk) : this(Tk, 0, 0.0, null) {}
        public LexerResult(int INum) : this(LexerTokens.T_INT, INum, 0.0, null) {}
        public LexerResult(double DNum) : this(LexerTokens.T_DECIMAL, 0, DNum, null) {}
        public LexerResult(string? Str) : this(LexerTokens.T_STRING, 0, 0.0, Str) {}
        public override string ToString() => Tk switch {
                LexerTokens.T_INT => $"Type: Integer Value: {INum}",
                LexerTokens.T_DECIMAL => $"Type: Double Value: {DNum}",
                LexerTokens.T_STRING => "Type: String Value: " + (Str ?? "null"),
                LexerTokens.T_EOF => "Type: End of file",
                _ => "" // Not possible 
        };
    }

    public class LexerMain {
        private int _lineno = 1;
        public int LineNo {get => _lineno; }

        private string? _line;
        public string? Line {get => _line; }

        private int _pos = 0;
        private int _length = 0;

        private bool _end = false;
        
        public readonly string File;

        private readonly StreamReader src;

        public LexerMain (string file) {
                File = file;
                src = new StreamReader(file);
        }

        private void read() {
		_line = src.ReadLine();
		if (_line == null) {
    			src.Close();
    			_end = true; 
    			return;
		}
		_lineno++;
	}

        private LexerResult create_token(string? target) {
            int inum;
            double dnum;

            if (Int32.TryParse(target, out inum)) 
                return new LexerResult(inum);
            else if (Double.TryParse(target, out dnum))
                return new LexerResult(dnum);
            else
                return new LexerResult(target);
        }

        public LexerResult Next() {
            if (_pos == _length) {
                read();
                if(_end)
                   return new LexerResult(LexerTokens.T_EOF);
                _pos = 0;
                _length = _line.Length;
            }
            StringBuilder sb = new StringBuilder();
            while (_pos < _length) {
                if (Char.IsWhiteSpace(_line[_pos])) {
                    _pos++;
                    break;
                }
                sb.Append(_line[_pos]);
                _pos++;
            }
            return create_token(sb.ToString());
        }
    }
}
