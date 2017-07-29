using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;

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
            var sut = new Lexer.Lexer();
            // Act
            sut.NextToken();
            // Assert
            Assert.True(false,$"The simple lexer did not work tokentype is wrong expected:  got : ");
        }
    }
 
}
