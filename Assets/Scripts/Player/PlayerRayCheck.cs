
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.InputSystem.HID;

namespace player
{
    public partial class PlayerController
    {
        LayerMask mask;
        RaycastHit wallHit;
        RaycastHit downHit;

        public Transform wallEnterCheckPos;
        public Transform wallDirCheckPos;



        public bool isWallHit;

        public bool completeWallRaiseUp = false;
        public bool exitWallState = false;

        RaycastHit hitinfo;
        Vector3 hitpoint;
        Vector3 normal;



        public Transform rightToLeftRay;
        public Transform leftToRightRay;
        private void Update()
        {
            if(playerCurrentStates is ClimbingState)
            {
                //CheckFrontWall();

                if (Physics.Raycast(wallDirCheckPos.position, wallDirCheckPos.forward, out hitinfo, 2f))
                {
                    hitpoint = hitinfo.point;
                    normal = hitinfo.normal;
                }
            }
        }


        /// <summary>
        /// 정면이 벽인지 확인하여 등반상태로 돌입하거나 벽위로 등반한다
        /// </summary>
        public void CheckFrontWall()
        {
            float rayRange = 0.3f;

            if (playerCurrentStates is ClimbingState)
            {
                rayRange = 0.8f;

            }

            if (Physics.Raycast(wallEnterCheckPos.position, wallEnterCheckPos.forward, out wallHit, rayRange, mask))
            {
                isWallHit = true;
            }
            else
            {
                isWallHit = false;
            }
        }

        /// <summary>
        /// 벽 위로 등반할때 어디까지 올라갈지 체크용 발에서 정면으로 쏘기때문에 캐릭터가 등반할때까지 위로 올린다
        /// </summary>
        public void CheckRaiseUpWallCheck()
        {
            if (!Physics.Raycast(transform.position, transform.forward, out wallHit, 0.3f, mask))
            {
                completeWallRaiseUp = true;
            }
        }

        /// <summary>
        /// 벽에서 아래버튼 눌러서 다시 바닥으로
        /// </summary>
        public bool CheckDownGroundEnter()
        {
            if (Physics.Raycast(transform.position, -transform.up, out downHit, 0.3f, mask))
            {
                return true;
            }
            return false;
        }

        private void OnDrawGizmos()
        {
            Debug.DrawRay(wallEnterCheckPos.position, wallEnterCheckPos.forward * 0.3f, Color.red);
            Debug.DrawRay(transform.position, transform.forward * 0.3f, Color.red);

            //Debug.DrawRay(transform.position, transform.forward * 0.2f, Color.yellow);

            //Debug.DrawRay(wallDirCheckPos.position, transform.right * 1f, Color.yellow);
            //Debug.DrawRay(wallDirCheckPos.position, transform.right * -1f, Color.yellow);

            //Debug.DrawRay(MoveRightLeftWallCheck.position, transform.right * -1f, Color.yellow);
            //Debug.DrawRay(MoveLeftRightWallCheck.position, transform.right * 1f, Color.yellow);


            Debug.DrawRay(wallDirCheckPos.position + wallDirCheckPos.TransformDirection(new Vector3(0.3f * MoveDir.x, 0, -1.0f)), wallDirCheckPos.forward * 1.0f, Color.yellow);
            Debug.DrawRay(rightToLeftRay.position, rightToLeftRay.forward * 1f, Color.red);
            Debug.DrawRay(leftToRightRay.position, leftToRightRay.forward * 1f, Color.red);
        }

        bool setComplet = false;
        Vector3 climbingMoveRotateHitVector;
        public Vector3 ClimbingMoveRotateHitVector
        {
            get => climbingMoveRotateHitVector;
            set
            {
                if(climbingMoveRotateHitVector != value)
                {
                    setComplet = true;
                    climbingMoveRotateHitVector = value;
                   
                    StartCoroutine(ClimbingRotateHitVectorDelaySet());
                }
            }
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if(!setComplet && playerCurrentStates is ClimbingState)
            {
                ClimbingMoveRotateHitVector = hit.normal;
            }
        }


        

        private IEnumerator ClimbingRotateHitVectorDelaySet()
        {
            yield return new WaitForSeconds(0.05f);
            setComplet = false;
        }

        //private IEnumerator TestPush()
        //{
        //    //while()
        //    //{
        //    //    characterController.Move(transform.TransformDirection(Vector3.forward) * 2.0f * Time.fixedDeltaTime);
        //    //    yield return null;
        //    //}
        //}
    }


}
