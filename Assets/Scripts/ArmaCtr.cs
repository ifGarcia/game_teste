using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaCtr : MonoBehaviour
{
    [SerializeField]
    public GameObject _tiroPrefabObjeto;
    public Arma _arma;

    private void Awake()
    {
        _arma = new Arma(this.gameObject, _tiroPrefabObjeto);
    }
    public Arma armaCriada()
    {
        return _arma;
    }

}
