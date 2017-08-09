using System;
using System.Diagnostics;
using System.Text;

namespace MonkeySharp.Lexer
{
    public class Lexer
    {
        public string ToParse { get; set; }
        private int Pos { get; set; }
        private  int ReadPosition { get; set; }
        private  char ch { get; set; }

        public Lexer(string toParse)
        {
            ToParse = toParse;
            ReadChar();
        }
        // ToDO: Replace switch with dictionary instead ?  https://stackoverflow.com/questions/11617091/in-a-switch-vs-dictionary-for-a-value-of-func-which-is-faster-and-why
        public bool IsLetter(char chr)
        {
            return 'a' <= chr && chr <= 'z' || 'A' <= chr && chr <= 'Z' || chr == '_';
        }
        public Token NextToken()
        {
            Token tok;

            switch (ch)
            {
                case '=':
                    tok= ParseToken(Tokens.ASSIGN);
                    break;
                case ';':
                    tok = ParseToken(Tokens.SEMICOLON);
                    break;
                case '(':
                    tok = ParseToken(Tokens.LPAREN);
                    break;
                case ')':
                    tok = ParseToken(Tokens.RPAREN);
                    break;
                case ',':
                    tok = ParseToken(Tokens.COMA);
                    break;
                case '+':
                    tok = ParseToken(Tokens.PLUS);
                    break;
                case '{':
                    tok = ParseToken(Tokens.LBRACE);
                    break;
                case '}':
                    tok = ParseToken(Tokens.RBRACE);
                    break;
                case (char)0:
                    tok = ParseToken(Tokens.EOF,(char)0);
                    break;
                default:
                {
                        if (IsLetter(ch))
                    {
                        tok = new Token(tokenType:Tokens.UNKNOWN, literal: ReadIdentifier());
                        return tok;
                    }
                        else
                        {
                            tok = new Token(tokenType: Tokens.ILLEGAL, literal: ch.ToString());
                        }
                        break;
                }
            }
            ReadChar();
           return tok;
        }

        private string ReadIdentifier()
        {
            var position =this.Pos;
            while (IsLetter(this.ch))
            {
                this.ReadChar();
            }
            int length = this.Pos - position;
            return this.ToParse.Substring(position, length);
        }

        public Token ParseToken(string tokenType)
        {
            var  tok = new Token(tokenType: tokenType, literal: tokenType);
            Debug.Print($"Parse NextToken as type: {tok.TokenType} with value: {tok.Literal}");
            return tok;
        }

        public Token ParseToken(string tokenType,char letterToParse)
        {
            var tok = new Token(tokenType: tokenType, literal: letterToParse.ToString());
            Debug.Print($"Parse NextToken as type: {tok.TokenType} with value: {tok.Literal}");
            return tok;
        }

        private void ReadChar()
        {
            ch = ReadPosition >= ToParse.Length ? (char) 0 : ToParse[ReadPosition];
            Pos = ReadPosition;
            ReadPosition += 1;
        }
        
    }
}