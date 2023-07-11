using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pet_Invok
{
    Animator _petAnimator;
    Transform _petTransform;
    SpriteRenderer _petSpriteRenderer;

    bool estaParado = true;

    int andandoKey = Animator.StringToHash("andando");

    public Vector2 _petVelocidade = new Vector2(0.5f, 0.5f); //Velocidade em que o pet irá andar

    public Pet_Invok(GameObject pet)
    {
        _petAnimator = pet.GetComponent<Animator>();
        _petTransform = pet.GetComponent<Transform>();
        _petSpriteRenderer = pet.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Animacao();

        /*if (alvo != null)
        {
            MovimentoDirecaoAlvo();
        }
        else
        {
            ProcuraAlvo();
        }*/
    }

    private void Animacao()
    {
        //Transição das animações
        if (!estaParado)
        {
            _petAnimator.SetTrigger(andandoKey);
        }
        else
        {
            _petAnimator.ResetTrigger(andandoKey);
        }

        //Transição da direção da Sprinte
        /*if (_playerTransform.position.x < _petTransform.position.x)
        {
            _petSpriteRenderer.flipX = true;
        }
        else if (_playerTransform.position.x > _petTransform.position.x)
        {
            _petSpriteRenderer.flipX = false;
        }*/
    }

    private void MovimentoDirecaoAlvo()
    {
        //_inimigoRB2D.MovePosition(_inimigoRB2D.position + _inimigoVelocidade * (_playerTransform.position - _inimigoTranform.position).normalized * Time.fixedDeltaTime);
    }
}
