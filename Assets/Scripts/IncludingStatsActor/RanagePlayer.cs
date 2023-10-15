using Cinemachine;
using player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RanagePlayer : PlayerStat
{
    //활 시위
    public GameObject bowString;

    //태초의 활 시위 위치 화살 발사할때 줄이 다시 복귀하기 위한 용도
    Vector3 RemeberbowStringPositionVector;

    //활 당기는 손
    public Transform bowDrawHand;
    bool bowDraw = false;

    //화살 뽑을때 프리팹 생성 
    public GameObject arrowPrefab;
    //public Transform arrowStartLookAtPos;

    //화살 발사하는 위치 카메라 중앙으로 날라가기 위해 필요하다
    public Transform arrowFirePos;
    Player_Arrow arrow;
    Vector3 cameraCenter;
    Vector3 fireDir;
    RaycastHit ray;



    public Transform normalAttackArrowPos;
    public Transform normalAttackArrowTargetPos;



    public Transform TempBugFix;

    /// <summary>
    /// 스킬
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

        playerName = "엠버";

        CalculatedAttackPower = Atk;
        skillCart = transform.GetChild(4).GetChild(0).GetComponent<CinemachineDollyCart>();
        skillCutSceneCamera = transform.GetChild(4).GetChild(0).GetChild(0).GetComponent<CinemachineVirtualCamera>();


        //콤보 데미지

        attackDamageCalculation[0] = 1.1f; //콤보1

        attackDamageCalculation[1] = 1.2f;  //콤보2

        attackDamageCalculation[2] = 1.25f;  //콤보3

        attackDamageCalculation[3] = 1.3f; //콤보4

        attackDamageCalculation[4] = 1.6f; //조준모드

        attackDamageCalculation[maxAttackCount-1] = 2.35f; // 스킬


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
        //적한테 살짝 접근 attackMove 값은 애니메이션 이밴트에서 실행된다
        //아래로 이동하는 용도임 없으면 좀 꼬임 공통된 행동이라 어택 스테이트로 옮기는게 좋을거 같아


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
    /// 화살 뽑아 드는 애니메이션 이밴트
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
    /// 활 시위 당기기
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
    /// 화살 발사하면서 활시위가 돌아와야함
    /// </summary>
    public void FireArrow()
    {
        bowDraw = false;
        
        arrow.ArrowDamageSetting(EnemyTargetDamage());
        bowString.transform.localPosition = RemeberbowStringPositionVector;
        //currentArrow.FireArrow();


        //중앙으로 날라가기
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
    /// 애니메이션 이밴트에서 타이밍을 잡고있다
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


        //중앙으로 날라가기
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
