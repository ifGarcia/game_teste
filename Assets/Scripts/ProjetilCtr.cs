using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjetilCtr : MonoBehaviour
{
    private Rigidbody2D _projetilRigidBody;
    private int dano = 1;
    private int _ultiplicadorDano;
    public int origin; //se 1 = player, 2 = inimigo

    void Start()
    {
        _projetilRigidBody = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("inimigo") && origin == 1)
        {
            other.gameObject.GetComponent<InimigoCtr>().inimigo.TomarDano(dano);
            ProjetilPool.GetInstance().ReturnToPool(this.gameObject);
        }
        else if (other.gameObject.CompareTag("Player") && origin == 2)
        {
            other.gameObject.GetComponent<PlayerCtr>().player.TomarDano(dano);
            ProjetilPool.GetInstance().ReturnToPool(this.gameObject);
        }
        else if (other.gameObject.CompareTag("parede"))
        {
            ProjetilPool.GetInstance().ReturnToPool(this.gameObject);
        }
    }
}