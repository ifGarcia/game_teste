using UnityEngine;
using UnityEditor;

public class InimigoCtr : MonoBehaviour
{
    public GameObject __tiroPrefabObjeto, _playerObject;
    public Inimigo inimigo;

    private void Awake()
    {
        inimigo = new Inimigo(this.gameObject, _playerObject, __tiroPrefabObjeto);
    }

    private void Update()
    {
        inimigo.onUpdate();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "parede")
        {
            inimigo.estaParado = true;
            if (inimigo.estaDirecaoPlayer == true)
            {
                inimigo.estaDirecaoPlayer = false;
                inimigo.estaRecuando = true;
            }
            else
            {
                inimigo.estaDirecaoPlayer = true;
                inimigo.estaRecuando = false;
            }
        }
    }

}