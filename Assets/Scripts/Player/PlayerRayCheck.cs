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

        bool isWallHit => hit.collider != null;

        
        private void Update()
        {
            //Debug.Log(isWallHit);
            //CheckFrontWall();
        }

        void CheckFrontWall()
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, 0.3f, mask))
            {
                //Debug.Log($"Ãæµ¹{hit.collider.gameObject.name}");
            }

        }

        private void OnDrawGizmos()
        {
            Debug.DrawRay(transform.position, transform.forward * 0.4f, Color.red);
        }

        public Vector3 climbingMoveRotateHitVector { get; private set; }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if(playerCurrentStates is ClimbingState)
            {
                climbingMoveRotateHitVector = hit.point;
                Debug.Log(climbingMoveRotateHitVector);
            }
        }
    }


}
