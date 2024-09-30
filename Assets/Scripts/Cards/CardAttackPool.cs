using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class CardAttackPool : MonoBehaviour
{
    public static CardAttackPool instance;
    private List<GameObject> pooledCardAttacks = new List<GameObject>();
    private int amountToPool = 20;

    [SerializeField] private GameObject attackPrefab;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(attackPrefab);
            obj.SetActive(false);
            pooledCardAttacks.Add(obj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetPooledCardAttack()
    {
        for(int i = 0;i < pooledCardAttacks.Count;i++)
        {
            if (!pooledCardAttacks[i].activeInHierarchy)
            {
                return pooledCardAttacks[i];
            }
        }
        return null;
    }
}
