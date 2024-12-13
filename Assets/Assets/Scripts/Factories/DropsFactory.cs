using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropsFactory : MonoBehaviour
{
    [SerializeField] private Drops[] Drops;
    private Dictionary<string, Drops> idDrops;

    private void Awake()
    {
        idDrops = new Dictionary<string, Drops>();

        foreach (var drops in Drops)
        {
            idDrops.Add(drops.Id, drops);
        }
    }

    public Drops Create(string id)
    {
        if (!idDrops.TryGetValue(id, out Drops drops))
        {
            return null;
        }
        return Instantiate(drops);
    }
}
