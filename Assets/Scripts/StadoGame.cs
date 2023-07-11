using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[System.Serializable]
public class StadoGame
{
    public enum EState { Inicio, Menu, Chefe_1, GameOver };
    private EState _currentEState = EState.Menu;
    private FState _state = null;

    public bool bOnMenu = false;
    public bool bOnChefe_1 = false;
    public bool bOnGameOver = false;


    public StadoGame()
    {
        _currentEState = EState.Inicio;
        _state = new FInicio(this);
    }

    public void OnUpdate()
    {
        EState frameState = _state.OnUpdate();
        if (frameState != _currentEState)
        {
            _state.OnEnd();
            // Troca de estado
            _currentEState = frameState;

            switch (_currentEState)
            {
                case EState.Inicio:
                    _state = new FInicio(this);
                    break;
                case EState.Menu:
                    _state = new FMenu(this);
                    break;
                case EState.Chefe_1:
                    _state = new FChefe_1(this);
                    break;
                case EState.GameOver:
                    _state = new FGameOver(this);
                    break;
                default:
                    break;
            }
            _state.OnBegin();
        }
    }

}

public abstract class FState
{
    protected StadoGame.EState _nextState;
    protected StadoGame _gameState;

    public FState(StadoGame inGameState) { _gameState = inGameState; }

    public abstract void OnBegin();
    public abstract StadoGame.EState OnUpdate();
    public abstract void OnEnd();

}

public class FInicio : FState
{
    public FInicio(StadoGame inGameState) : base(inGameState) { }

    public override void OnBegin()
    {
        _nextState = StadoGame.EState.Inicio;
        SceneManager.LoadScene("Inicio");
    }

    public override StadoGame.EState OnUpdate()
    {
        if (_gameState.bOnMenu)
            _nextState = StadoGame.EState.Menu;

        // Defino qual estado estou...
        Debug.Log("Estou no Inicio");

        return _nextState;
    }

    public override void OnEnd()
    {
        GameManager.GetInstance().SetMenu(false);
    }
}

public class FMenu : FState
{
    public FMenu(StadoGame inGameState) : base(inGameState) { }

    public override void OnBegin()
    {
        _nextState = StadoGame.EState.Menu;
        SceneManager.LoadScene("Menu");
    }

    public override StadoGame.EState OnUpdate()
    {
        if (_gameState.bOnChefe_1)
            _nextState = StadoGame.EState.Chefe_1;

        // Defino qual estado estou...
        Debug.Log("Estou no Menu");

        return _nextState;
    }

    public override void OnEnd()
    {
        GameManager.GetInstance().SetChefe_1(false);
    }
}

public class FChefe_1 : FState
{
    public FChefe_1(StadoGame inGameState) : base(inGameState) { }

    public override void OnBegin()
    {
        _nextState = StadoGame.EState.Chefe_1;
        SceneManager.LoadScene("Chefe_1");
    }

    public override StadoGame.EState OnUpdate()
    {
        if (_gameState.bOnGameOver)
            _nextState = StadoGame.EState.GameOver;


        Debug.Log("Estou no Jogo");
        return _nextState;
    }

    public override void OnEnd()
    {
        GameManager.GetInstance().SetGameOver(false);
    }
}

public class FGameOver : FState
{
    private float _targetTime = 3.0f;
    private float _deltaTime = 0.0f;

    public FGameOver(StadoGame inGameState) : base(inGameState) { }

    public override void OnBegin()
    {
        _deltaTime = 0.0f;
        _nextState = StadoGame.EState.GameOver;
        SceneManager.LoadScene("GameOver");
    }

    public override StadoGame.EState OnUpdate()
    {
        _deltaTime += Time.deltaTime;
        if (_deltaTime >= _targetTime)
            _nextState = StadoGame.EState.Menu;

        Debug.Log("Estou no GameOver");
        return _nextState;
    }

    public override void OnEnd()
    {

    }

}
