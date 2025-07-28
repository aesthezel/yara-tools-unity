using System.Collections.Generic;

namespace YaraTools.Patterns.Behavioral
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }

    public class CommandHandler
    {
        private List<ICommand> _commands = new List<ICommand>();

        public void ExecuteCommand(ICommand command)
        {
            command.Execute();
            _commands.Add(command);
        }

        public void UndoLastCommand()
        {
            if (_commands.Count > 0)
            {
                ICommand lastCommand = _commands[_commands.Count - 1];
                lastCommand.Undo();
                _commands.RemoveAt(_commands.Count - 1);
            }
        }
    }
}
