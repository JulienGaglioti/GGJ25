using UnityEngine;

public class Test : MonoBehaviour
{
    private void Start() 
    {
        InputManager.Instance.AttackAction += TestAttack;
    }

    private void OnDestroy() 
    {
        InputManager.Instance.AttackAction -= TestAttack;
    }

    private void TestAttack()
    {
        print("attacked");
    }
}
