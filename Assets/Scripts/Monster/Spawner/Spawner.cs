
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Spawner : MonoBehaviour
{
    float Radius;
    int monsterCount;
    public GameObject monster;
    public int maxMonsterCount = 5;
    public float spawnWait = 3.0f;
    private void Awake()
    {
        
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        //Spawn();
    }
    //public void SpawnArea(Transform spawnTransform, float radius)
    //{
        
    //    Vector3 getPoint = Random.onUnitSphere ;
    //    getPoint.y = 0.0f;

    //    float r = Random.Range(0.0f, radius);
    //    spawnTransform.position = (getPoint *r) +transform.position;
    //}

    //void Spawn()
    //{
        
           
    //        if (monsterCount == 0)
    //        {

    //            for (int i = 0; 0 < maxMonsterCount; i++)
    //            {

    //                GameObject obj = Instantiate(monster);
    //                SpawnArea(obj.transform, Radius);
    //            }
    //        }
        
        
    //}
}

