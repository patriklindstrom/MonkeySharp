using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;
using MonkeySharp.Lexer;
using NUnit.Framework.Internal.Execution;

namespace MonkeySharp.Test
{
    public class TokenComparer : Comparer<Token>
    {
        public override int Compare(Token x, Token y)
        {
            var literalCompare = String.Compare(x.Literal, y.Literal, StringComparison.Ordinal);
            var tokentypeCompare = String.Compare(x.TokenType, y.TokenType, StringComparison.Ordinal);
            return literalCompare == 0 && tokentypeCompare == 0 ? 0 : -1;
        }
    }
    [TestFixture]
    public class SimpleLexTester
    {
        [Test]
        public void TestHello()
        { //Arrange
            var helloTestMsg = "Hello Mr Tester";
            //Act
           Debug.WriteLine(helloTestMsg);
            //Assert
            Assert.True("Hello Mr Tester" == helloTestMsg) ;
        }
        [Test]
        public void Simple_Test_read_next_token()
        {
            // Arrange
            var testInput = "=+(){},;";
            var expectedTokens = new List<Token>
            {
                new Token(Tokens.ASSIGN, "=")
               , new Token(Tokens.PLUS  , "+")
               , new Token(Tokens.LPAREN  , "(")
               , new Token(Tokens.RPAREN  , ")")
               , new Token(Tokens.LBRACE  , "{")
               , new Token(Tokens.RBRACE  , "}")
               , new Token(Tokens.COMA  , ",")
               , new Token(Tokens.SEMICOLON  , ";")
               , new Token(Tokens.EOF  , ((char)0).ToString())
            };
            var sut = new Lexer.Lexer(testInput);
            var lexTestResult = new List<Token>();
            // Act 
            foreach (var t in expectedTokens)
            {
                Token token = sut.NextToken();
                lexTestResult.Add(token);
            }            
            // Assert
            CollectionAssert.AreEqual(expectedTokens, lexTestResult, new TokenComparer(), "The simple lexer did not work tokentype is wrong in test");
        }

        [Test]
        public void Yes_it_is_a_letter()
        {
            //Arrange
            var sut = new Lexer.Lexer("abcdZz_");
            bool verdict = true;
            //Act
            foreach (char testChar in sut.ToParse)
            {
                 verdict = sut.IsLetter(testChar);
                if (verdict == false) {break; }
            }
           Assert.IsTrue(verdict,"Wrong about what char is letter i Lexer");
        }
        [Test]
        public void No_it_is_not_a_letter()
        {
            //Arrange
            var sut = new Lexer.Lexer(@"% # & / \ @ "" å ");
            bool verdict = false;
            //Act
            foreach (char testChar in sut.ToParse)
            {
                verdict = sut.IsLetter(testChar);
                if (verdict == true) { break; }
            }
            Assert.IsFalse(verdict, "Wrong about what char is letter i Lexer. Thinks non letter is letter");
        }
        [Test]
        public void Simple_Test_read_simple_Statement()
        {
            // Arrange
            var testInput =
                #region testInputStatement

                @"
                let five = 5;
let ten = 10;

let add = fn(x, y) {
  x + y;
};

let result = add(five, ten);
            ";
            #endregion 
            //Act
            var expectedTokens = new List<Token>
            #region expectedListDef
                  {
          new Token(Tokens.LET, "let")
        , new Token(Tokens.IDENT, "five")
        , new Token(Tokens.ASSIGN, "=")
        , new Token(Tokens.INT, "5")
        , new Token(Tokens.SEMICOLON, ";")
        , new Token(Tokens.LET, "let")
        , new Token(Tokens.IDENT, "ten")
        , new Token(Tokens.ASSIGN, "=")
        , new Token(Tokens.INT, "10")
        , new Token(Tokens.SEMICOLON, ";")
        , new Token(Tokens.LET, "let")
        , new Token(Tokens.IDENT, "add")
        , new Token(Tokens.ASSIGN, "=")
        , new Token(Tokens.FUNCTION, "fn")
        , new Token(Tokens.LPAREN, "(")
        , new Token(Tokens.IDENT, "x")
        , new Token(Tokens.COMA, ",")
        , new Token(Tokens.IDENT, "y")
        , new Token(Tokens.RPAREN, ")")
        , new Token(Tokens.LBRACE, ", new Token(")
        , new Token(Tokens.IDENT, "x")
        , new Token(Tokens.PLUS, "+")
        , new Token(Tokens.IDENT, "y")
        , new Token(Tokens.SEMICOLON, ";")
        , new Token(Tokens.RBRACE, ")")
        , new Token(Tokens.SEMICOLON, ";")
        , new Token(Tokens.LET, "let")
        , new Token(Tokens.IDENT, "result")
        , new Token(Tokens.ASSIGN, "=")
        , new Token(Tokens.IDENT, "add")
        , new Token(Tokens.LPAREN, "(")
        , new Token(Tokens.IDENT, "five")
        , new Token(Tokens.COMA, ",")
        , new Token(Tokens.IDENT, "ten")
        , new Token(Tokens.RPAREN, ")")
        , new Token(Tokens.SEMICOLON, ";")
               , new Token(Tokens.EOF  , ((char)0).ToString())
            };
            #endregion
            var sut = new Lexer.Lexer(testInput);
            var lexTestResult = new List<Token>();
            foreach (var t in expectedTokens)
            {
                Token token = sut.NextToken();
                lexTestResult.Add(token);
            }
            // Assert
            CollectionAssert.AreEqual(expectedTokens, lexTestResult, new TokenComparer(), "The simple lexer did not work tokentype is wrong in statement test");
        }
    }
 
}
