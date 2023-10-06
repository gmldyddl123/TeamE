using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RanagePlayer : PlayerStat
{

    public GameObject bowString;
    Transform RemeberbowStringTransform;

    public Transform bowDrawHand;
    bool bowDraw = false;

    public GameObject arrowPrefab;
    Arrow currentArrow;
    public Transform arrowStartLookAtPos;

    private void Awake()
    {
        attackMoveSpeed = -2.0f;
        RemeberbowStringTransform = bowString.transform;
    }

    protected override void Update()
    {
        base.Update();
        
    }

    private void LateUpdate()
    {
        if (bowDraw)
        {
            bowString.transform.position = bowDrawHand.position;
        }
    }

    public override void AttackMove(Vector3 movedir)
    {

        //base.Attack(movedir);
        //playerInputSystem.UseGravity();
        //������ ��¦ ���� attackMove ���� �ִϸ��̼� �̹�Ʈ���� ����ȴ�
        //�Ʒ��� �̵��ϴ� �뵵�� ������ �� ���� ����� �ൿ�̶� ���� ������Ʈ�� �ű�°� ������ ����


        characterController.Move(
             Vector3.down * 3.0f
             * Time.fixedDeltaTime);
        if (attackMove)
        {
            //playerInputSystem.UseGravity();
            characterController.Move(attackMoveSpeed * Time.fixedDeltaTime * movedir);
        }

    }


    public void DrawArrow()
    {
        GameObject gameObject = Instantiate(arrowPrefab, bowDrawHand);
        //gameObject.transform.Translate(0, transform.GetChild(0).transform.position.y, 0, Space.Self);
        currentArrow = gameObject.GetComponent<Arrow>();
        //gameObject.transform.LookAt(arrowStartLookAtPos);
        
    }

    public void FireArrow()
    {
        bowDraw = false;
        bowString.transform.localPosition = RemeberbowStringTransform.localPosition;        
        Debug.Log(RemeberbowStringTransform.position);
        currentArrow.FireArrow();
    }

    public void DrawBowString()
    {
        bowDraw = !bowDraw;

        Debug.Log(bowDraw);

        if (!bowDraw)
        {
            bowString.transform.localPosition = RemeberbowStringTransform.localPosition;
            Debug.Log(RemeberbowStringTransform.position);
        }

        //bowString.transform.position = RemeberbowStringTransform.position;
        //return;
    }

}
