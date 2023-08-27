using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.HID;

namespace player
{
    public partial class PlayerController
    {
        LayerMask mask;
        RaycastHit hit;

        public Transform wallEnterCheckPos;
        public Transform wallDirCheckPos;

        bool isWallHit => hit.collider != null;

        
        private void Update()
        {
            //Debug.Log(isWallHit);
            //CheckFrontWall();
        }

        void CheckFrontWall()
        {
            if (Physics.Raycast(wallEnterCheckPos.position, wallEnterCheckPos.forward, out hit, 0.3f, mask))
            {
                //Debug.Log($"Ãæµ¹{hit.collider.gameObject.name}");
            }

        }

        private void OnDrawGizmos()
        {
            Debug.DrawRay(wallEnterCheckPos.position, wallEnterCheckPos.forward * 0.4f, Color.red);
            //Debug.DrawRay(transform.position, transform.forward * 0.2f, Color.yellow);

            //Debug.DrawRay(wallDirCheckPos.position, transform.right * 1f, Color.yellow);
            //Debug.DrawRay(wallDirCheckPos.position, transform.right * -1f, Color.yellow);
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
                //climbingMoveRotateHitVector = hit.moveDirection;
                //StartCoroutine(ClimbingRotateHitVectorDelaySet());

                ClimbingMoveRotateHitVector = hit.normal;

                //Debug.Log(climbingMoveRotateHitVector);
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
