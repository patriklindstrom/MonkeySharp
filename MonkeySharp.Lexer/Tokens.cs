using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MonkeySharp.Lexer
{
    // for enumeration of strings see this old solution : https://www.codeproject.com/Articles/11130/String-Enumerations-in-C

    // public enum TokenType { Late = -1, OnTime = 0, Early = 1 };
    public sealed class Tokens
    {

        private Tokens() { }

        public const string ILLEGAL = "ILLEGAL";
        public const string EOF = "EOF";
        // Identifiers + literals
        public const string IDENT  = "IDENT"; // add, foobar , x,y,...
        public const string INT = "INT"; // 12345
        // Operator
        public const string ASSIGN = "=";
        public const string PLUS = "+";
    // Delimiters
        public const string COMA = ",";
        public const string SEMICOLON = ";";

        public const string LPAREN = "+";
        public const string RPAREN = "+";
        public const string LBRACE = "+";
        public const string RBRACE = "+";


        //Keywords
        public const string FUNCTION = "FUNCTION";
        public const string LET = "LET";
    }

    struct Token
    {
        private string TokenType;
        private string Literal;
    }
}
