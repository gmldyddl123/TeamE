using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionController : MonoBehaviour
{
    //[SerializeField] Camera cam;

    //RaycastHit hitInfo;

    //[SerializeField] GameObject go_NormalCrosshair;
    //[SerializeField] GameObject go_InteractiveCrosshair;
    //[SerializeField] GameObject go_Crosshair;
    //[SerializeField] GameObject go_Cursor;

    bool isContact = false;
    public static bool isInteract = false;

    [SerializeField] ParticleSystem ps_QuestionEffect;

    DialogueManager theDM;

    public void SettingUI(bool p_flag)
    {
        // 스태미너바, NPC체력바이름 비활성화 
        // 플레이어 상호작용 버튼

        // 키 비활성화는 Invoke해서 유저에서 끄는걸로
    }

    void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
    }

    void Update()
    {
        CheckObject();
        ClickLeftBtn();
    }

    void CheckObject()
    {
        //Vector3 t_MousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);

        //if(Physics.Raycast(cam.ScreenPointToRay(t_MousePos), out hitInfo, 100))
        //{
        //    Contact();
        //}
        //else
        //{
        //    NotContact();
        //}
    }

    void Contact()
    {
        //if (hitInfo.transform.CompareTag("Interaction"))
        //{
            if (!isContact)
            {
                isContact = true;
                //go_InteractiveCrosshair.SetActive(true);
                //go_NormalCrosshair.SetActive(false);
            }
        //}
        else
        {
            NotContact();
        }
    }

    void NotContact()
    {
        if (isContact)
        {
            isContact = false;
            //go_InteractiveCrosshair.SetActive(false);
            //go_NormalCrosshair.SetActive(true);
        }
    }
    void ClickLeftBtn()
    {
        if (!isInteract)
        { 
            if (Input.GetMouseButtonDown(0))
            {
                if (isContact)
                {
                    Interact();
                }
            }
        }
    }

    void Interact()
    {
        isInteract = true;

        ps_QuestionEffect.gameObject.SetActive(true);
        //Vector3 t_targetPos = hitInfo.transform.position;
        //ps_QuestionEffect.GetComponent<QuestionEffect>().SetTarget(t_targetPos);
        //ps_QuestionEffect.transform.position = cam.transform.position;

        StartCoroutine(WaitCollision());
    }

    IEnumerator WaitCollision()
    {
        //yield return new WaitUntil(()=>QuestionEffect.isCollide);
        //QuestionEffect.isCollide = false;
        yield return null;
        //theDM.ShowDialogue();
    }

    /*
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {

        }
    }
    

    void OnClickBtn()
    {

    }
    */
}
