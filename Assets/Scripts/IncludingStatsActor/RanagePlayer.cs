using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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

    //ȭ�� ������ ������ ���� 
    public GameObject arrowPrefab;
    //public Transform arrowStartLookAtPos;

    //ȭ�� �߻��ϴ� ��ġ ī�޶� �߾����� ���󰡱� ���� �ʿ��ϴ�
    public Transform arrowFirePos;
    Player_Arrow arrow;
    Vector3 cameraCenter;
    Vector3 fireDir;
    RaycastHit ray;


    protected override void Awake()
    {
        //base.Awake();

        attackMoveSpeed = -2.0f;
        RemeberbowStringPositionVector = bowString.transform.localPosition;

        maxHP = 40.0f;
        Atk = 15.0f;
        Def = 10.0f;

        Debug.Log($"{maxHP}, {Atk}, {Def}");

        playerName = "����";

        //gameObject.SetActive( false );
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
    public void DrawBowString()
    {
        bowDraw = !bowDraw;

        if (!bowDraw)
        {
            bowString.transform.localPosition = RemeberbowStringPositionVector;
        }
    }
    /// <summary>
    /// ȭ�� �߻��ϸ鼭 Ȱ������ ���ƿ;���
    /// </summary>
    public void FireArrow()
    {
        bowDraw = false;
        arrow.ArrowDamageSetting(CalculatedAttackPower);
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
