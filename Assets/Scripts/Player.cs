using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Weapon equipedWeapon;
    private void Awake()
    {
        Application.targetFrameRate = 120;
    }
    private void Update()
    {

    }
}
