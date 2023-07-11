using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCtr : MonoBehaviour
{
    public void NovoJogo()
    {
        GameManager.GetInstance().SetMenu(true);
    }

    public void Opcoes()
    {
        //GameManager.GetInstance().SetMenu(false);
    }

    public void Sair()
    {
        //GameManager.GetInstance().SetMenu(false);
    }

}
