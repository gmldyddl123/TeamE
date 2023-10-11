using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RanagePlayer : PlayerStat
{
    //Ȱ ����
    public GameObject bowString;

    //������ Ȱ ���� ��ġ ȭ�� �߻��Ҷ� ���� �ٽ� �����ϱ� ���� �뵵
    Vector3 RemeberbowStringPositionVector;

    //Ȱ ���� ��
    public Transform bowDrawHand;
    bool bowDraw = false;

<<<<<<< HEAD
=======
    //ȭ�� ������ ������ ���� 
    public GameObject arrowPrefab;
    //public Transform arrowStartLookAtPos;

    //ȭ�� �߻��ϴ� ��ġ ī�޶� �߾����� ���󰡱� ���� �ʿ��ϴ�
    public Transform arrowFirePos;
    Player_Arrow arrow;
    Vector3 cameraCenter;
    Vector3 fireDir;
    RaycastHit ray;

>>>>>>> 20231008Test_HanJunHee
    private void Awake()
    {
        attackMoveSpeed = -2.0f;
        RemeberbowStringPositionVector = bowString.transform.localPosition;
    }

    protected override void Update()
    {
        base.Update();
        
    }

    private void LateUpdate()
    {
        if (bowDraw)
        {
            Debug.Log("������");
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

<<<<<<< HEAD
=======
    /// <summary>
    /// ȭ�� �̾� ��� �ִϸ��̼� �̹�Ʈ
    /// </summary>
    public void DrawArrow()
    {
        GameObject gameObject = Instantiate(arrowPrefab, bowDrawHand);
        //gameObject.transform.Translate(0, transform.GetChild(0).transform.position.y, 0, Space.Self);

        arrow = gameObject.GetComponent<Player_Arrow>();
        playerController.currentArrow = arrow;

        //gameObject.transform.LookAt(arrowStartLookAtPos);

    }
    /// <summary>
    /// Ȱ ���� ����
    /// </summary>
>>>>>>> 20231008Test_HanJunHee
    public void DrawBowString()
    {
        bowDraw = !bowDraw;

<<<<<<< HEAD
        Debug.Log(bowDraw);

        if(!bowDraw)
        {           
            bowString.transform.localPosition = RemeberbowStringTransform.localPosition;
            Debug.Log(RemeberbowStringTransform.position);
=======
        if (!bowDraw)
        {
            bowString.transform.localPosition = RemeberbowStringPositionVector;
>>>>>>> 20231008Test_HanJunHee
        }
    }
    /// <summary>
    /// ȭ�� �߻��ϸ鼭 Ȱ������ ���ƿ;���
    /// </summary>
    public void FireArrow()
    {
        bowDraw = false;
        bowString.transform.localPosition = RemeberbowStringPositionVector;
        //currentArrow.FireArrow();


        //�߾����� ���󰡱�
        Camera camera = Camera.main;
        cameraCenter = camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.5f));
        if (Physics.Raycast(cameraCenter, camera.transform.forward, out ray, 100.0f))
        {
            fireDir = ray.point;
        }
        else
        {
            fireDir = camera.transform.forward * 100.0f;
        }
        arrow.AimDirArrow(fireDir);
    }


    private void OnDrawGizmos()
    {
        //ȭ�� ����
        
        
    }

}
