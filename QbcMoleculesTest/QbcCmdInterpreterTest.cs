using QbcMoleculesBusinessLogic.Data.ProcessCommands;
using System.Collections.Generic;
using Xunit;

namespace QbcMoleculesTest
{
    public class QbcCmdInterpreterTest
    {
        [Fact]
        public void BuildCmd_Should_Return_Empty_List_Test()
        {
            List<string> dataToTest = new ();

            var result = QbcCmdInterpreter.BuildCmd(dataToTest.ToArray());

            Assert.Empty(result);

        }


        [Fact]
        public void BuildCmd_Should_Return_List_Test()
        {
            List<string> dataToTest = new ();

            dataToTest.Add("-test1");
            dataToTest.Add("param1");
            dataToTest.Add("param2=2");
            dataToTest.Add("-test2");
            dataToTest.Add("param1");
            dataToTest.Add("param2=2");

            var result = QbcCmdInterpreter.BuildCmd(dataToTest.ToArray());

            Assert.True(result.Count == 2);

        }
    }
}