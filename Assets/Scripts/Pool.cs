using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pool : MonoBehaviour
{
    public List<PoolItem> items;
    public List<Transform> instanced = new List<Transform>();

    public void Initialize()
    {
        Transform tr = transform;
        foreach (var item in items)
        {
            instanced.AddRange(item.Instantiate(tr));
        }
    }

    public Transform GetRandom()
    {
        var actived = instanced.Where(t => !t.gameObject.activeSelf)
            .ToArray();
        var randActive = Random.Range(0, actived.Length);
        return actived[randActive];
    }
    
    public Transform GetRandomSafe()
    {
        var actived = instanced.Where(t => !t.gameObject.activeSelf)
            .ToArray();
        var randActive = Random.Range(0, actived.Length);
        return actived[randActive];

    }
}

[System.Serializable]
public class PoolItem
{
    public Transform prefab;
    public int quantity;

    public Transform[] Instantiate(Transform parent)
    {
        Transform[] transforms = new Transform[quantity];
        for (int i = 0; i < quantity; i++)
        {
            transforms[i] = GameObject.Instantiate(prefab, parent);
            transforms[i].gameObject.SetActive(false);
        }
        return transforms;
    }

}
