namespace MonkeySharp.Lexer
{
    public class Lexer
    {
        public string ToParse { get; set; }
        private int Pos { get; set; }

        public Lexer(string toParse)
        {
            ToParse = toParse;
        }
        public Token NextToken()
        {
           return new Token(Tokens.EOF, "");
        }
    }
}