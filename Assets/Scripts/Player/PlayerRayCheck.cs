
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
        RaycastHit hit;

        public Transform wallEnterCheckPos;
        public Transform wallDirCheckPos;

        bool isWallHit => hit.collider != null;


        RaycastHit hitinfo;
        Vector3 hitpoint;
        Vector3 normal;



        public Transform rightToLeftRay;
        public Transform leftToRightRay;
        private void Update()
        {
            if (Physics.Raycast(wallDirCheckPos.position, wallDirCheckPos.forward, out hitinfo, 2f))
            {
                hitpoint = hitinfo.point;
                normal = hitinfo.normal;
            }

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

        ControllerColliderHit colliderHit;

        

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

        public IEnumerator TestTimeDelay(bool test)
        {
            test = true;
            yield return new WaitForSeconds(0.1f);
            test = false;
            testDelayCorution = false;
        }


        bool testDelayCorution = false;

        public void DelayTest(bool climbingTest)
        {
            if( !testDelayCorution)
            {
                testDelayCorution = true;
                StartCoroutine(TestTimeDelay(climbingTest));
            }
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
