using UnityEngine;
using UnityEditor;

[System.Serializable]
public class Arma
{
    public GameObject _armaPrefabObjet;
    public GameObject _projetilPrefabObjet;
    public Transform _armaTransform, _projetilRef;

    public int _armaDano = 12;
    public float _projetilVelocidade = 1f;

    public Arma(GameObject arma, GameObject projetilPrefab)
    {
        _projetilPrefabObjet = projetilPrefab;
        _armaTransform = arma.GetComponent<Transform>();
        _projetilRef = arma.transform.GetChild(0);
    }

    public void Movimenta(Vector3 direcao)
    {
        _armaTransform.up = direcaoNormalized(direcao);
    }

    public void Atira(Vector3 direcao)
    {
        GameObject projetil = ProjetilPool.GetInstance().Create(_projetilRef.position, _projetilRef.rotation, 1);
        projetil.GetComponent<Rigidbody2D>().velocity = direcaoNormalized(direcao) * _projetilVelocidade; //joga o objeto
        /*/https://philipperocha.wordpress.com/2016/05/29/criando-emissao-de-projeteis-para-o-personagem-como-forma-de-ataque-aos-inimigos/
        //http://equilibrecursos.com.br/2013/12/unity-3d-17-atirando-projeteis-parte-2-tiros-estilo-megaman/
        GameObject projetil = GameObject.Instantiate(_projetilPrefabObjet, _projetilRef.position, _projetilRef.rotation); //Estancia objeto
        projetil.GetComponent<Rigidbody2D>().velocity = direcaoNormalized(direcao) * _projetilVelocidade; //joga o objeto
        //ObjectPool.GetInstance().Create(spawnBullet.position, Quaternion.identity, direction);*/
    }

    private Vector2 direcaoNormalized(Vector3 direcao)
    {
        return new Vector2(direcao.x - _armaTransform.position.x, direcao.y - _armaTransform.position.y).normalized;
    }

}