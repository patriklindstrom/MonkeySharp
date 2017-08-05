using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;
using MonkeySharp.Lexer;

namespace MonkeySharp.Test
{
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
        public void Test_read_next_token()
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
               , new Token(Tokens.SEMICOLON  , ";")
               , new Token(Tokens.EOF  , "")
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
            CollectionAssert.AreEqual(expectedTokens,lexTestResult, $"The simple lexer did not work tokentype is wrong expected:  got : ");
            Assert.True(false,$"The simple lexer did not work tokentype is wrong expected:  got : ");
        }
    }
 
}
