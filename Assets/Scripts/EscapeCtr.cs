using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeCtr : MonoBehaviour
{
    public GameObject _textoObjetoPref;
    private GameObject _textoObjeto;

    public void Interagir()
    {
        GameManager.GetInstance().SetChefe_1(true);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && _textoObjeto == null)
        {
            _textoObjeto = Instantiate(_textoObjetoPref, other.transform);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(_textoObjeto);
        }
    }

}
