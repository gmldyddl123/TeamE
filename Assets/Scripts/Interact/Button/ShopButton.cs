using UnityEngine;

public class ShopButton : ButtonScript
{
    public override void Interact()
    {
        base.Interact(); // 부모 클래스의 Interact() 메서드 호출
        Debug.Log("Shop Button Interact");
        // 상점 이용 기능 구현
    }
}