using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Arrow : MonoBehaviour
{
    float arrowSpeed = 10.0f;

    bool startFire = false;

    float fallingRotateSpeed = 0.1f;

    //Vector3 fireDir;

    private void OnEnable()
    {
        //StartCoroutine(FallingArrow());
        //fireDir = transform.forward;
    }

    //void Update()
    //{
    //    if (startFire)
    //    {
    //        //transform.Translate(arrowSpeed* Time.deltaTime * transform.forward, Space.World);
    //        transform.Translate(arrowSpeed * Time.deltaTime * transform.forward, Space.World);

    //    }
    //}

    private void FixedUpdate()
    {
        if (startFire)
        {
            //transform.Translate(arrowSpeed* Time.deltaTime * transform.forward, Space.World);
            transform.Translate(arrowSpeed * Time.fixedDeltaTime * transform.forward, Space.World);

        }
    }

    public void AimDirArrow(Vector3 fireDir)
    {
        transform.LookAt(fireDir);
    }


    public void FireArrow()
    {
        //transform.LookAt(dir);
        transform.parent = null;
        startFire = true;
        StartCoroutine(FallingArrow());
    }


    IEnumerator FallingArrow()
    {
        yield return new WaitForSeconds(3.0f);
        while (true)
        {
            Quaternion targerRotation = Quaternion.LookRotation(Vector3.down);
            Quaternion arrowRoation = Quaternion.Slerp(transform.rotation, targerRotation, fallingRotateSpeed * Time.fixedDeltaTime);

            transform.rotation = arrowRoation;
            //transform.Rotate(-transform.up);
            yield return null; 
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        startFire = false;
        StopAllCoroutines();
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        transform.Translate(0.5f * transform.forward, Space.World);
        Destroy(gameObject, 5.0f);
        Debug.Log("엔터");
        if(collision.gameObject.CompareTag("Enemy"))
        {        
            //몬스터 피해
        }
    }
}
