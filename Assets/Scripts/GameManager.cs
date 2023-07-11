using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager uniqueIntance = null;
    public static GameManager GetInstance() { return uniqueIntance; }
    public StadoGame stadoGame = new StadoGame();

    private void Awake()
    {
        if (uniqueIntance == null)
        {
            uniqueIntance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void SetMenu(bool value)
    {
        stadoGame.bOnMenu = value;
    }

    public void SetChefe_1(bool value)
    {
        stadoGame.bOnChefe_1 = value;
    }

    public void SetGameOver(bool value)
    {
        stadoGame.bOnGameOver = value;
    }

    void Update()
    {
        stadoGame.OnUpdate();
    }

}

