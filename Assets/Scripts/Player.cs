using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

[System.Serializable]
public class Player
{
    public int _playerVida, _playerVidaMax = 25;
    private Slider _uiVidaSlider;
    private Text _uiVidaText;
    public Vector2 _playerVelocidade = new Vector2(0.8f, 0.8f);
    public Arma _playerArmaEquipada;
    public Collider2D _interacaoCollider;

    GameObject _armaObjeto;
    Animator _playerAnimator;
    Transform _playerTransform, _playerArmaRef;
    SpriteRenderer _playerSpriteRenderer;
    Rigidbody2D _playerRB2D;

    int andandoKey = Animator.StringToHash("andando");//https://docs.unity3d.com/ScriptReference/Animator.StringToHash.html

    Vector2 _playerMovimento;
    Vector3 _mouseRef;


    public Player(GameObject player, GameObject _armaObjeto, Slider _uiVida, Text _uiVidaNumValue)
    {
        _playerAnimator = player.GetComponent<Animator>();
        _playerTransform = player.GetComponent<Transform>();
        _playerSpriteRenderer = player.GetComponent<SpriteRenderer>();
        _playerRB2D = player.GetComponent<Rigidbody2D>();
        _playerArmaRef = player.transform.GetChild(0);
        this._armaObjeto = GameObject.Instantiate(_armaObjeto, _playerArmaRef);
        _playerArmaEquipada = this._armaObjeto.GetComponent<ArmaCtr>().armaCriada();
        _playerVida = _playerVidaMax;
        this._uiVidaSlider = _uiVida;
        this._uiVidaSlider.maxValue = _playerVida;
        this._uiVidaSlider.value = _playerVida;
        this._uiVidaText = _uiVidaNumValue;
        this._uiVidaText.text = _playerVida + "/" + _playerVidaMax;
    }

    public void onFixedUpdate()
    {
        Movimento();
    }

    public void onUpdate()
    {
        _mouseRef = Camera.main.ScreenToWorldPoint(Input.mousePosition);//Mouse ref e movimetno player é melhor pegar no monobehavor e passaro por referencia?
        Animacao();
        if (_playerArmaEquipada != null)
        {
            ArmaMovimento();
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Atirar();
            }
        }

        // Se ele apertar "e" e tiver algo para interagir, vai para a função de interação
        if (Input.GetKeyDown("e") && _interacaoCollider != null)
        {
            InterageE();
        }
    }

    void Animacao()//Transição das animações
    { //https://www.youtube.com/playlist?list=PLUur4s1pRGf9jHKo5XAKqP2XyRw35czIK
        //Transição das animações
        if (_playerMovimento.x != 0 || _playerMovimento.y != 0)
        {
            _playerAnimator.SetTrigger(andandoKey);
        }
        else
        {
            _playerAnimator.ResetTrigger(andandoKey);
        }

        //Transição da direção da Sprinte
        if (_playerTransform.position.x < _mouseRef.x)
        {
            _playerSpriteRenderer.flipX = false;
        }
        else if (_playerTransform.position.x > _mouseRef.x)
        {
            _playerSpriteRenderer.flipX = true;
        }
    }   

    void Movimento()//Movimento do personagem
    {
        _playerMovimento = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        _playerRB2D.MovePosition(_playerRB2D.position + _playerVelocidade * _playerMovimento.normalized * Time.fixedDeltaTime);
    }

    public void PegaArma(GameObject armaPega)//Recebe o objeto class arma
    {
        if (_playerArmaEquipada == null)//se não tiver arma equipada
        {
            _armaObjeto = GameObject.Instantiate(armaPega, _playerArmaRef);//invoca o objeto arma local
            _playerArmaEquipada = _armaObjeto.GetComponent<ArmaCtr>().armaCriada();
        }
        else
        {
            _armaObjeto.transform.parent = null;
            _armaObjeto = armaPega;
            _armaObjeto.transform.parent = _playerArmaRef;
            _armaObjeto.transform.SetPositionAndRotation(_playerArmaRef.position, _playerArmaRef.rotation);
            _playerArmaEquipada = _armaObjeto.GetComponent<ArmaCtr>().armaCriada();
        }
       //_playerInteragilE = true;
    }
        
    public void Atirar()//Atira com a arma
    {
        _playerArmaEquipada.Atira(_mouseRef);
    }

    public void ArmaMovimento()//Rotaciona a arma
    {
        _playerArmaEquipada.Movimenta(_mouseRef); //Arma seguir o mouse
    }

    public void InterageE()
    {
        if (_interacaoCollider.CompareTag("arma"))
        {
            PegaArma(_interacaoCollider.gameObject);
            _interacaoCollider.gameObject.GetComponent<Transform>().localScale = new Vector3(.5f, .5f, 0);
            //_playerInteragilE = true;
        }
        else if (_interacaoCollider.CompareTag("escape"))
        {
            _interacaoCollider.gameObject.GetComponent<EscapeCtr>().Interagir();
        }
    }

    public void TomarDano(int dano)
    {
        _playerVida -= dano;
        _uiVidaSlider.value = _playerVida;
        if (_playerVida <= 0)
        {
            GameManager.GetInstance().SetGameOver(true);
        }
        _uiVidaText.text = _playerVida + "/" + _playerVidaMax;
    }

}