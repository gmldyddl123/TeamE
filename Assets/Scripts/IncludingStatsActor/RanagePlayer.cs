using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RanagePlayer : PlayerStat
{

    public GameObject bowString;
    Transform RemeberbowStringTransform;

    public Transform bowDrawHand;
    bool bowDraw = false;

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
            Debug.Log("당기는중");
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

    public void DrawBowString()
    {
        bowDraw = !bowDraw;

        Debug.Log(bowDraw);

        if(!bowDraw)
        {           
            bowString.transform.localPosition = RemeberbowStringTransform.localPosition;
            Debug.Log(RemeberbowStringTransform.position);
        }

        //bowString.transform.position = RemeberbowStringTransform.position;
        //return;
    }

}
