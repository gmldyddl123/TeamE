using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : PooledObject
{
    float speed;

    private void Awake()
    {
        speed = 3;
    }

    private void Update()
    {
        transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * speed);
    }
}
