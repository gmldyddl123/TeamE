using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Arrow : MonoBehaviour
{
    float arrowSpeed = 10.0f;

    bool startFire = false;

    float fallingRotateSpeed = 0.2f;

    //Vector3 fireDir;

    //Rigidbody rigid;


    float arrowDamage = 0.0f;
    SphereCollider arrowCollider;

    //CapsuleCollider arrowCollider;

    private void Awake()
    {
        //rigid = GetComponent<Rigidbody>();
        arrowCollider = GetComponent<SphereCollider>();
    }

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

    public void ArrowDamageSetting(float damage)
    {
        arrowDamage = damage;
    }

    public void AimDirArrow(Vector3 fireDir)
    {
        transform.LookAt(fireDir);
    }


    public void FireArrow()
    {
        //transform.LookAt(dir);
        //rigid.isKinematic = false;
        transform.parent = null;
        startFire = true;
        arrowCollider.enabled = true;
        StartCoroutine(FallingArrow());
    }


    IEnumerator FallingArrow()
    {
        yield return new WaitForSeconds(1.0f);
        while (true)
        {
            Quaternion targerRotation = Quaternion.LookRotation(Vector3.down);
            Quaternion arrowRoation = Quaternion.Slerp(transform.rotation, targerRotation, fallingRotateSpeed * Time.fixedDeltaTime);

            transform.rotation = arrowRoation;
            //transform.Rotate(-transform.up);
            yield return null; 
        }
    }


    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (!startFire)
    //        return;

    //    StopAllCoroutines();
    //    gameObject.GetComponent<Rigidbody>().isKinematic = true;
    //    startFire = false;
    //    gameObject.transform.parent = collision.transform;
    //    transform.LookAt(collision.contacts[0].normal);
    //    transform.Translate(0.1f * transform.forward, Space.Self);
    //    Destroy(gameObject, 5.0f);
    //    if (collision.gameObject.CompareTag("Enemy"))
    //    {
    //        //몬스터 피해
    //    }
    //}


    private void OnTriggerEnter(Collider other)
    {
        if (!startFire)
            return;

        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        StopAllCoroutines();
        arrowCollider.enabled = false;
        startFire = false;

        int escape = 0;
        Transform otherParent = other.transform;
        do
        {
            otherParent = other.transform.parent;
            escape++;

            if (escape > 100)
                break;
        } while (other.transform.parent == null);

        gameObject.transform.parent = otherParent;

        transform.Translate(0.1f * transform.forward, Space.Self);
        Destroy(gameObject, 5.0f);
        if (other.gameObject.CompareTag("Enemy"))
        {
            //몬스터 피해
            other.GetComponent<IncludingStatsActor>().OnDamage(arrowDamage);
        }
    }
}
