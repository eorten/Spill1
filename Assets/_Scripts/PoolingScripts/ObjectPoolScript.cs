using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolScript : MonoBehaviour
{
    [SerializeField]
    private GameObject levelPrefab;
    [SerializeField]
    private Queue<GameObject> levelPool = new Queue<GameObject>();
    private int poolStartSize = 50;

    void Start()
    {
        for (int i = 0; i < poolStartSize; i++)
        {
            GameObject level = Instantiate(levelPrefab);
            levelPool.Enqueue(level);
            level.SetActive(false);
        }
    }

    public GameObject GetLevel() //Samme som instantiate
    {
        if (levelPool.Count > 0)
        {
            GameObject level = levelPool.Dequeue();
            level.SetActive(true);
            return level;
        }
        else
        {
            GameObject level = Instantiate(levelPrefab);
            return level;
        }
    }
    public void ReturnLevel(GameObject level) //Samme som destroy
    {
        levelPool.Enqueue(level);
        level.SetActive(false);
    }
}
