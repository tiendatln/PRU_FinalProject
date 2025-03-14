using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameInitiator : MonoBehaviour
{
    [SerializeField]private Camera _Camera;
    [SerializeField] private GameObject _map;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _gameManager;
    [SerializeField] private GameObject _UIController;
    [SerializeField] private GameObject _eventSystem;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _objectPool;
    [SerializeField] private GameObject _DeadUI;
    [SerializeField] private GameObject _enemy;
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private async void Start()
    {
        CreateGameObject();
    }


    private void CreateGameObject()
    {
        _Camera = Instantiate(_Camera);
        _map = Instantiate(_map);
        _player = Instantiate(_player);
        _pauseMenu = Instantiate(_pauseMenu);
       _DeadUI = Instantiate(_DeadUI);
        _enemy = Instantiate(_enemy);
       
        _objectPool = Instantiate(_objectPool);
        _gameManager = Instantiate(_gameManager);
        _UIController = Instantiate(_UIController);
        _eventSystem = Instantiate(_eventSystem);
    }
    
}
