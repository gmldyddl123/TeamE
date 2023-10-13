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


    protected override void Awake()
    {
        //base.Awake();

        attackMoveSpeed = -2.0f;
        RemeberbowStringPositionVector = bowString.transform.localPosition;

        maxHP = 40.0f;
        Atk = 15.0f;
        Def = 10.0f;

        Debug.Log($"{maxHP}, {Atk}, {Def}");

        playerName = "엠버";

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
        //적한테 살짝 접근 attackMove 값은 애니메이션 이밴트에서 실행된다
        //아래로 이동하는 용도임 없으면 좀 꼬임 공통된 행동이라 어택 스테이트로 옮기는게 좋을거 같아


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
    /// 화살 뽑아 드는 애니메이션 이밴트
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
        arrow.ArrowDamageSetting(CalculatedAttackPower);
        bowString.transform.localPosition = RemeberbowStringPositionVector;
        //currentArrow.FireArrow();


        //중앙으로 날라가기
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
        //화살 방향
        
        
    }

}
