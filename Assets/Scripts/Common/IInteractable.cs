using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IInteractable
{
    bool IsDirectUse
    {
        get;    // 상호작용 가능한 오브젝트가 직접 사용가능한 것인지, 간접 사용 가능한 것인지 표시하기 위한 프로퍼티
    }

    void Use(); // 사용하는 기능이 있다고 선언해 놓은 것
}
