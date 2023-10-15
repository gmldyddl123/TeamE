using Cinemachine;
using player;
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



    public Transform normalAttackArrowPos;
    public Transform normalAttackArrowTargetPos;



    public Transform TempBugFix;

    /// <summary>
    /// ��ų
    /// </summary>


    GameObject skillArrow;
    //BoxCollider skillCollider;

    bool stopCamera = false;

    protected override void Awake()
    {
        maxAttackCount = 6;

        base.Awake();

        attackMoveSpeed = -2.0f;
        RemeberbowStringPositionVector = bowString.transform.localPosition;

        skillEffect = transform.GetChild(2).GetComponent<ParticleSystem>();
        skillCollider = transform.GetChild(3).GetComponent<BoxCollider>();

        AttackCollider skillColliderComponent = skillCollider.GetComponent<AttackCollider>();
        skillColliderComponent.atkPower = EnemyTargetDamage;

        maxHP = 40.0f;
        Atk = 15.0f;
        Def = 10.0f;

        Debug.Log($"{maxHP}, {Atk}, {Def}");

        playerName = "����";

        CalculatedAttackPower = Atk;
        skillCart = transform.GetChild(4).GetChild(0).GetComponent<CinemachineDollyCart>();
        skillCutSceneCamera = transform.GetChild(4).GetChild(0).GetChild(0).GetComponent<CinemachineVirtualCamera>();


        //�޺� ������

        attackDamageCalculation[0] = 1.1f; //�޺�1

        attackDamageCalculation[1] = 1.2f;  //�޺�2

        attackDamageCalculation[2] = 1.25f;  //�޺�3

        attackDamageCalculation[3] = 1.3f; //�޺�4

        attackDamageCalculation[4] = 1.6f; //���ظ��

        attackDamageCalculation[maxAttackCount-1] = 2.35f; // ��ų


        //gameObject.SetActive( false );
    }

    //protected override void Update()
    //{
    //    base.Update();

    //}

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
            Vector3 dir = new Vector3(movedir.x * attackMoveSpeed, movedir.y, movedir.z * attackMoveSpeed);
            //characterController.Move(attackMoveSpeed * Time.fixedDeltaTime * movedir);
            characterController.Move(dir * Time.fixedDeltaTime);

        }

    }


    public override void SkillCameraOn()
    {
        base.SkillCameraOn();
        stopCamera = true;
        skillCart.m_Speed = 0.8f;
        //skillCutSceneCamera.Priority = 50;
        StartCoroutine(SkillCameraStopPos());
    }

    IEnumerator SkillCameraStopPos()
    {
        while( skillCart.m_Position < 2.0f)
        {

            yield return null;
        }
        skillCart.m_Speed = 0.0f;
        //SkillCameraStop();
    }

    //public void SkillCameraStop()
    //{
    //    skillCart.m_Speed = 0.0f;
    //}

    public void SkillCameraZoomOut()
    {
        skillCart.m_Speed = 30.0f;
    }

    public override void UltimateSkill()
    {
        skillEffect.Play();
    }

    /// <summary>
    /// ȭ�� �̾� ��� �ִϸ��̼� �̹�Ʈ
    /// </summary>
    public void DrawArrow()
    {
        GameObject gameObject = Instantiate(arrowPrefab, bowDrawHand);
        attackCount = 4;
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
        
        arrow.ArrowDamageSetting(EnemyTargetDamage());
        bowString.transform.localPosition = RemeberbowStringPositionVector;
        //currentArrow.FireArrow();


        //�߾����� ���󰡱�
        Camera camera = Camera.main;
        cameraCenter = camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.5f));

        if (Physics.Raycast(cameraCenter, camera.transform.forward, out ray, 100.0f))
        {
            Debug.Log(ray.collider.name);
            fireDir = ray.point;
        }
        else
        {
            fireDir = TempBugFix.position;

            //fireDir = TempBugFix.position;
            //fireDir = ray.
        }
        arrow.AimDirArrow(fireDir);
    }


    /// <summary>
    /// �ִϸ��̼� �̹�Ʈ���� Ÿ�̹��� ����ִ�
    /// </summary>
    public void NormalAttackFireArrow()
    {

        attackCount++;
        GameObject gameObject = Instantiate(arrowPrefab, normalAttackArrowPos);
        arrow = gameObject.GetComponent<Player_Arrow>();
        arrow.ArrowDamageSetting(EnemyTargetDamage());


        if(playerController.LockOnTarget)
        {
            arrow.AimDirArrow(playerController.LockOnTarget.position);
        }
        else
        {
            arrow.AimDirArrow(normalAttackArrowTargetPos.position);
        }

        arrow.FireArrow();


        //bowDraw = false;
        //bowString.transform.localPosition = RemeberbowStringPositionVector;
        //currentArrow.FireArrow();


        //�߾����� ���󰡱�
        //Camera camera = Camera.main;
        //cameraCenter = camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.5f));
        //if (Physics.Raycast(cameraCenter, camera.transform.forward, out ray, 100.0f))
        //{
        //    fireDir = ray.point;
        //}
        //else
        //{
        //    fireDir = camera.transform.forward * 100.0f;
        //}

    }



    public void SkillDrawArrow()
    {
        skillArrow = Instantiate(arrowPrefab, bowDrawHand);
    }

    public void SkillDrawBowString()
    {
        bowDraw = true;
    }

    public void SkillFire()
    {
        Destroy(skillArrow);
        bowDraw = false;
        bowString.transform.localPosition = RemeberbowStringPositionVector;
        SkillColliderActive();
        skillEffect.Play();
    }

}
