using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    float arrowSpeed = 10.0f;

    bool startFire = false;

    float fallingRotateSpeed = 0.8f;

    private void OnEnable()
    {
        //StartCoroutine(FallingArrow());
    }

    void Update()
    {
        if (startFire)
        {
            //rigidbody.AddForce(0, 0, arrowForce, ForceMode.Impulse);
            transform.Translate(arrowSpeed* Time.deltaTime * transform.forward, Space.World);
        }
    }


    public void InitArrow()
    {

    }


    public void FireArrow()
    {
        //transform.LookAt(dir);
        Debug.Log("들어왔다");
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
}
