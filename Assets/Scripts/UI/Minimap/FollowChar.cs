using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowChar : MonoBehaviour
{
    [SerializeField]
    private bool x, y, z; // 이 값이 true면 target 좌표, false면 현재 좌표 그대로
    [SerializeField]
    private Transform target; // 대상

    private void Update()
    {
        if (!target) return;

        transform.position = new Vector3(
            (x ? target.position.x : transform.position.x),
            (y ? target.position.y : transform.position.y),
            (z ? target.position.z : transform.position.z));

    }
}
