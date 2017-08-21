using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;

namespace MonkeySharp.Lexer
{
    public class Lexer
    {
        public string ToParse { get; set; }
        private int Pos { get; set; }
        private  int ReadPosition { get; set; }
        private  char ch { get; set; }
        private Dictionary<string,string> KeyWords { get; set; }
        public Lexer(string toParse)
        {

            ToParse = toParse;
            KeyWords = new Dictionary<string, string>
            {
                  ["fn"] = Tokens.FUNCTION
                , ["let"] = Tokens.LET
            };
            ReadChar();
        }
        // ToDO: Replace switch with dictionary instead ?  https://stackoverflow.com/questions/11617091/in-a-switch-vs-dictionary-for-a-value-of-func-which-is-faster-and-why
        public bool IsLetter(char chr)
        {
            return 'a' <= chr && chr <= 'z' || 'A' <= chr && chr <= 'Z' || chr == '_';
        }

        public bool IsDigit(char chr)
        {
            return '0' <= ch && ch <= '9';

        }
        public Token NextToken()
        {
            Token tok;
            this.SkipWhiteSpace();
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
                            var tokLiteral = this.ReadIdentifier();
                            var tokType = LookupIdentifier(tokLiteral);
                        tok = new Token(tokenType: tokType, literal: tokLiteral);
                            
                        return tok;
                    } else if (IsDigit(this.ch))
                        {
                            var tokType = Tokens.INT;
                            var tokLiteral = this.ReadNumber();
                            tok = new Token(tokenType: tokType, literal: tokLiteral);
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

        private string ReadNumber()
        {
            var position=this.Pos;
            while (IsDigit(this.ch))
        {
            this.ReadChar();
        }
            int length = this.Pos - position;
            return this.ToParse.Substring(position, length);
        }


        public  string LookupIdentifier(string tokLiteral)
        {
            return this.KeyWords.ContainsKey(tokLiteral) ? this.KeyWords[tokLiteral] : Tokens.IDENT;
        }

        private void SkipWhiteSpace()
        {
            while (this.ch == ' ' || this.ch == '\t' || this.ch == '\n' || this.ch == '\r')
            {
                this.ReadChar();
            }
         
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

        public void ReadChar()
        {
            ch = ReadPosition >= ToParse.Length ? (char) 0 : ToParse[ReadPosition];
            Pos = ReadPosition;
            ReadPosition += 1;
        }
        
    }
}