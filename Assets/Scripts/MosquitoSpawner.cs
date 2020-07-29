using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MosquitoSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject mosquito;
    public int countMosquitoes;
    private List<GameObject> mosquitoes;
    private Queue<int> indexMosquitoes;
    private float speed;
    private float spawnTime;

    void Start()
    {
        mosquitoes = new List<GameObject>();
        indexMosquitoes = new Queue<int>();
        GameManager.Instance.AfterGameStart += Init;
        GameManager.Instance.onGameOver += Reset;
    }

    void Init()
    {
        speed = 2;
        spawnTime = 2.5f;
        for (int i = mosquitoes.Count; i < countMosquitoes; i++)
        {
            var mq = Instantiate(mosquito) as GameObject;
            mq.GetComponent<Mosquito>().onDead = BackToPull;
            mq.active = false;
            mosquitoes.Add(mq);
            mq.transform.SetParent(transform);
            indexMosquitoes.Enqueue(i);
        }
        for(int i = 0; i < 4; i++)
        {
            Spawn();
        }
        StartCoroutine(Spawner());
    }

    public IEnumerator Spawner()
    {
        while (GameManager.Instance.IsGameStart)
        {
            if (indexMosquitoes.Count > 0)
            {
                Spawn();
            }
            yield return new WaitForSeconds(spawnTime);
        }
    }

    void Spawn()
    {
        int idx = indexMosquitoes.Dequeue();
        var mq = mosquitoes[idx];
        mq.active = true;
        Vector3 pos = transform.position + Random.Range(-5, 5) * Vector3.up;

        bool fromRight = false;
        if (Random.Range(-1, 2) > 0)
        {
            pos *= -1;
            fromRight = true;
        }
            
        mq.GetComponent<Mosquito>().Init(Human.Instance.Position, speed, pos, idx, fromRight);
        speed = Mathf.Min(speed + 0.2f, 10f);
    }

    public void BackToPull(int index)
    {
        spawnTime = Mathf.Max(spawnTime - 0.1f, 1f);
        mosquitoes[index].active = false;
        indexMosquitoes.Enqueue(index);
    }

    public void Reset()
    {
        StopCoroutine(Spawner());
        indexMosquitoes.Clear();
        for (int i = 0; i < mosquitoes.Count; i++)
        {
            indexMosquitoes.Enqueue(i);
            mosquitoes[i].active = false;
        }
    }

}
