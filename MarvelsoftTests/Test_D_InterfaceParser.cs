using MarvelsoftConsole.Interfaces;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace MarvelsoftTests
{
    [TestFixture]
    public class Test_D_InterfaceParser
    {
        private Mock<IParser> IParserMock;

        [SetUp]
        public void Setup()
        {
            IParserMock = new Mock<IParser>();
        }

        [Test]
        public async Task InterfaceParser_A_ProcessAsync()
        {
            IParserMock.Setup(x => x.ProcessAsync())
                .Returns(Task.CompletedTask);

            var result = await TestParserSubject.OperationAsync(IParserMock.Object, "process");

            Assert.IsInstanceOf<Task>(result);
        }

        [Test]
        public async Task InterfaceParser_B_ParseAsync()
        {
            IParserMock.Setup(x => x.ParseAsync<int>(1, 2))
                .Returns(Task.FromResult(Task.CompletedTask));

            var result = await TestParserSubject.OperationAsync(IParserMock.Object, "parse");

            Assert.IsInstanceOf<Task>(result);
        }

        /// <summary>
        /// A subject class in which we'll send the mocked interface to operate on (both async tasks).
        /// </summary>
        private class TestParserSubject
        {
            public async static Task<Task> OperationAsync(IParser parser, string operation)
            {
                if (operation == "process")
                {
                    await parser.ProcessAsync();
                }
                else if (operation == "parse")
                {
                    await parser.ParseAsync<int>(1, 2);
                }

                return Task.CompletedTask;
            }
        }
    }
}
