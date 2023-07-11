using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inimigo 	//Basicamente seguimos esses videos, porém modificando para nossas necessidades.
{										//https://www.youtube.com/channel/UCoxRNjIDKlzxxl8OOJub6CA
    public bool entroNoRange = false;			//https://www.youtube.com/channel/UCC2k-SBDIRK1Jg1vJSYOBYg
    public bool estaDirecaoPlayer = true;		//https://www.youtube.com/channel/UCok1vSaNxZZrxufASLHSqJg
    public bool estaParado = false;			//https://unity3d.com/pt/learn/tutorials
    public bool estaRecuando = false;

    int andandoKey = Animator.StringToHash("andando");

    public Vector2 _inimigoVelocidade = new Vector2(.5f, .5f); //Velocidade em que o inimigo irá andar
    public float _inimigoRangeAtaque = 1;
    public float _inimigoRangeRecua = 2;
    public float tempoParado;
    public float tempoRecua;
    public float duracaoRecua = 5;
    public float duracaoParado = 5;
    public float playerDistancia;


    private Transform _projetilRefTranform, _inimigoTranform;
    private GameObject _projetilPrefabObjeto;
    private Transform _playerTransform;
    private Animator _inimigoAnimator; //Para utilizar a animação do inimigo andando
    private Rigidbody2D _inimigoRB2D;
    private SpriteRenderer _inimigoSpriteRenderer;

    public int _inimigoVida = 100;

    public Inimigo(GameObject inimigo, GameObject _playerObject, GameObject projetilPrefab)
    {
        _inimigoAnimator = inimigo.GetComponent<Animator>();//Inicializando o animator
        _inimigoTranform = inimigo.GetComponent<Transform>();
        _inimigoRB2D = inimigo.GetComponent<Rigidbody2D>();
        _inimigoSpriteRenderer = inimigo.GetComponent<SpriteRenderer>();
        _playerTransform = _playerObject.GetComponent<Transform>();
        _projetilPrefabObjeto = projetilPrefab;
        _projetilRefTranform = inimigo.transform.GetChild(0);
    }

    public void onUpdate()
    {
        playerDistancia = Vector3.Distance(_inimigoTranform.position, _playerTransform.position);

        Atirar();
        Animacao();

        if (estaDirecaoPlayer) //se esta em direção ao player
        {
            if (playerDistancia <= _inimigoRangeAtaque) //se entrou no range de ataque
            {
                entroNoRange = true;
                estaDirecaoPlayer = false;
                estaParado = true;
            }
            else
            {
                entroNoRange = false;
                estaDirecaoPlayer = true;
                estaParado = false;
            }
            MovimentoDirecaoPlayer();
        }
        else if (estaParado) //se esta na fase parado
        {
            if (tempoParado <= duracaoParado) //se o tempo de ficar parado ainda não foi alcançado
            {
                tempoParado += Time.deltaTime;
                estaDirecaoPlayer = false;
                estaParado = true;
            }
            else
            {
                estaDirecaoPlayer = false;
                estaParado = false;
                estaRecuando = true;
                tempoParado = 0;
            }
            Parado();
        }
        else if (estaRecuando) //se esta recuando em relção ao player
        {
            if(playerDistancia >= _inimigoRangeRecua || tempoRecua >= duracaoRecua) //se o tempo de de recuar ou a distancia maxima de recuo não foi atingido
            {
                estaDirecaoPlayer = true;
                estaParado = false;
                estaRecuando = false;
                tempoRecua = 0;
            }
            else
            {
                tempoRecua += Time.deltaTime;
            }
            MovimentoDirecaoRecuaPlayer();
        }
    }

    private void MovimentoDirecaoPlayer()
    {

        _inimigoRB2D.MovePosition(_inimigoRB2D.position + _inimigoVelocidade * (_playerTransform.position - _inimigoTranform.position).normalized * Time.fixedDeltaTime);
    }

    private void MovimentoDirecaoRecuaPlayer()
    {
        _inimigoRB2D.MovePosition(_inimigoRB2D.position + _inimigoVelocidade * (_inimigoTranform.position - _playerTransform.position).normalized * Time.fixedDeltaTime);
    }

    private void Animacao()
    {
        //Transição das animações
        if (!estaParado)
        {
            _inimigoAnimator.SetTrigger(andandoKey);
        }
        else
        {
            _inimigoAnimator.ResetTrigger(andandoKey);
        }

        //Transição da direção da Sprinte
        if (_playerTransform.position.x < _inimigoTranform.position.x)
        {
            _inimigoSpriteRenderer.flipX = true;
        }
        else if (_playerTransform.position.x > _inimigoTranform.position.x)
        {
            _inimigoSpriteRenderer.flipX = false;
        }
    }

    void Parado()
    {
        _inimigoRB2D.MovePosition(_inimigoRB2D.position);
    }

    void Atirar()
    {
        if (entroNoRange == true)
        {
            for (int i=0;i<=3;i++)
            {
                GameObject projetil = ProjetilPool.GetInstance().Create(_projetilRefTranform.position, _projetilRefTranform.rotation, 2);
                projetil.GetComponent<Rigidbody2D>().velocity = (_playerTransform.position - _inimigoTranform.position) * 1f; //joga o objeto
            }
            entroNoRange = false;
        }
    }

    public void TomarDano(int dano)
    {
        _inimigoVida -= dano;
        if (_inimigoVida <= 0)
        {
            Debug.Log("Morreu otario");
        }
    }

}