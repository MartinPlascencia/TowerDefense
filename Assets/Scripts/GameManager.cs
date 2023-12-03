using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    /*public enum GameState{
        Playing,
        GameOver
    }
    [SerializeField] private GameState _gameState = GameState.Playing;
    public GameState State
    {
        get { return _gameState; }
        set { _gameState = value; }
    }*/
    [Header("Game Settings")]
    [Range(0,5)][SerializeField] private float _gameSpeed = 1f;
    [SerializeField] private GameState _gameState;
    public float GameSpeed
    {
        set { _gameSpeed = value; }
    }

    [Header("Events")]
    [SerializeField] private UnityEvent<bool> OnGameOver;
    [SerializeField] private UnityEvent OnGameStart;
    [SerializeField] private UnityEvent OnWin;
    [SerializeField] private UnityEvent OnLose;
    [Header("Controllers")]
    public ResourcesController ResourcesController;
    public MessageController MessageController;
    private int _enemiesOnScene;
    private bool _gameOver;
    public bool GameOver
    {
        get { return _gameOver; }
        set { 
            if(_gameOver)
                return;
            _gameOver = value; 
            OnGameOver?.Invoke(value);
            //State = GameState.GameOver;
            if(value)
                OnWin?.Invoke();
            else
                OnLose?.Invoke();
            _gameState.GamePlayingState = GameState.State.GameOver;
        }
    }
    public int EnemiesOnScene
    {
        get { return _enemiesOnScene; }
        set { _enemiesOnScene = value; }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Update()
    {
        Time.timeScale = _gameSpeed;
    }

    private void OnEnable()
    {
        _gameState.GamePlayingState = GameState.State.Menu;
    }

    public void StartGame()
    {
        _gameState.GamePlayingState = GameState.State.Playing;
        OnGameStart?.Invoke();
    }
}
