using NUnit.Framework;
using YaraTools.Patterns.Behavioral;

namespace YaraTools.Tests.Runtime.Behavioral
{
    public class CommandTests
    {
        private class MockCommand : ICommand
        {
            public bool Executed { get; private set; }
            public bool Undone { get; private set; }

            public void Execute()
            {
                Executed = true;
            }

            public void Undo()
            {
                Undone = true;
            }
        }

        [Test]
        public void TestExecuteCommand()
        {
            var commandHandler = new CommandHandler();
            var mockCommand = new MockCommand();

            commandHandler.ExecuteCommand(mockCommand);

            Assert.IsTrue(mockCommand.Executed);
        }

        [Test]
        public void TestUndoCommand()
        {
            var commandHandler = new CommandHandler();
            var mockCommand = new MockCommand();

            commandHandler.ExecuteCommand(mockCommand);
            commandHandler.UndoLastCommand();

            Assert.IsTrue(mockCommand.Undone);
        }
    }
}
