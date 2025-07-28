using UnityEngine;
using YaraTools.Patterns.Behavioral;

namespace YaraTools.Examples.CommandPattern
{
    public class ChangeColorCommand : ICommand
    {
        private readonly GameObject _gameObject;
        private readonly Color _newColor;
        private Color _previousColor;

        public ChangeColorCommand(GameObject gameObject, Color newColor)
        {
            _gameObject = gameObject;
            _newColor = newColor;
        }

        public void Execute()
        {
            _previousColor = _gameObject.GetComponent<Renderer>().material.color;
            _gameObject.GetComponent<Renderer>().material.color = _newColor;
        }

        public void Undo()
        {
            _gameObject.GetComponent<Renderer>().material.color = _previousColor;
        }
    }

    public class MoveCommand : ICommand
    {
        private readonly GameObject _gameObject;
        private readonly Vector3 _direction;
        private readonly float _distance;

        public MoveCommand(GameObject gameObject, Vector3 direction, float distance)
        {
            _gameObject = gameObject;
            _direction = direction;
            _distance = distance;
        }

        public void Execute()
        {
            _gameObject.transform.position += _direction * _distance;
        }

        public void Undo()
        {
            _gameObject.transform.position -= _direction * _distance;
        }
    }
}
