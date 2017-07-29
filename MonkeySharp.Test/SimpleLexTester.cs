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
                new Token() {Literal = "=", TokenType = Tokens.ASSIGN}
               , new Token() {Literal = "+", TokenType = Tokens.PLUS}
               , new Token() {Literal = "(", TokenType = Tokens.LPAREN}
               , new Token() {Literal = ")", TokenType = Tokens.RPAREN}
               , new Token() {Literal = "{", TokenType = Tokens.LBRACE}
               , new Token() {Literal = "}", TokenType = Tokens.RBRACE}
            };

            var sut = new Lexer.Lexer(testInput);
            // Act
            sut.NextToken();
            // Assert
            Assert.True(false,$"The simple lexer did not work tokentype is wrong expected:  got : ");
        }
    }
 
}
