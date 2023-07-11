using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCtr : MonoBehaviour
{
    public GameObject _armaObjeto;
    public Player player;
    public Slider _uiVida;
    public Text _uiVidaNum;

    private void Awake()
    {
        player = new Player(this.gameObject, _armaObjeto, _uiVida, _uiVidaNum);
    }
    void FixedUpdate()
    {
        player.onFixedUpdate();
    }

    void Update()
    {
        player.onUpdate();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("arma") || other.CompareTag("escape"))
        {
            player._interacaoCollider = other;
            //other.gameObject.GetComponent<Transform>().localScale = new Vector3(.8f, .8f, 0); //passar para a classe arma
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("arma") || other.CompareTag("escape"))
        {
            player._interacaoCollider = null;
            //other.gameObject.GetComponent<Transform>().localScale = new Vector3(.5f, .5f, 0);
        }
    }

}