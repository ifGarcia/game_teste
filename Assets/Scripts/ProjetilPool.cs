using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjetilPool : MonoBehaviour
{
    public GameObject[] pooling = null;
    public GameObject prefab = null;
    public int maxObjects = 0;
    private static ProjetilPool uniqueInstance = null;

    private void Awake()
    {
        if (uniqueInstance == null)
        {
            uniqueInstance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        pooling = new GameObject[maxObjects];

        for (int i = 0; i < maxObjects; i++)
        {
            pooling[i] = GameObject.Instantiate<GameObject>(prefab);
            pooling[i].SetActive(false);
            DontDestroyOnLoad(pooling[i]);
        }

    }

    public static ProjetilPool GetInstance()
    {
        return uniqueInstance;
    }

    public GameObject Create(Vector3 postion, Quaternion rotation, int origin)
    {
        for (int i = 0; i < maxObjects; i++)
        {
            if (!pooling[i].activeSelf)
            {
                pooling[i].transform.position = postion;
                pooling[i].transform.rotation = rotation;
                pooling[i].GetComponent<ProjetilCtr>().origin = origin;
                pooling[i].SetActive(true);
                return pooling[i];
            }
        }
        return null;
    }

    public void ReturnToPool(GameObject pollObject)
    {
        pollObject.SetActive(false);
    }

}