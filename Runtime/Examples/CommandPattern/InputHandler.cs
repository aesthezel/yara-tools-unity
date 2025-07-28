using UnityEngine;
using YaraTools.Patterns.Behavioral;

namespace YaraTools.Examples.CommandPattern
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] private GameObject _gameObject;
        private CommandHandler _commandHandler = new CommandHandler();

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                _commandHandler.ExecuteCommand(new ChangeColorCommand(_gameObject, Color.red));
            }
            else if (Input.GetKeyDown(KeyCode.G))
            {
                _commandHandler.ExecuteCommand(new ChangeColorCommand(_gameObject, Color.green));
            }
            else if (Input.GetKeyDown(KeyCode.B))
            {
                _commandHandler.ExecuteCommand(new ChangeColorCommand(_gameObject, Color.blue));
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                _commandHandler.ExecuteCommand(new MoveCommand(_gameObject, Vector3.up, 1f));
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                _commandHandler.ExecuteCommand(new MoveCommand(_gameObject, Vector3.down, 1f));
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _commandHandler.ExecuteCommand(new MoveCommand(_gameObject, Vector3.left, 1f));
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                _commandHandler.ExecuteCommand(new MoveCommand(_gameObject, Vector3.right, 1f));
            }
            else if (Input.GetKeyDown(KeyCode.Z))
            {
                _commandHandler.UndoLastCommand();
            }
        }
    }
}
