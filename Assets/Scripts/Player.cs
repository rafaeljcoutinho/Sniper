using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] Weapon weapon;
    [SerializeField] Camera cameraPlayer;
    [SerializeField] Slider zoomSlider;
    [SerializeField] GameObject ScopeUI;

    private float camInitZoom;
    private bool isScope;
    private void Awake()
    {
        Application.targetFrameRate = 120;
        camInitZoom = cameraPlayer.fieldOfView;
        zoomSlider.value = zoomSlider.maxValue;
    }
    private void Update()
    {
        weapon.AdjustCameraZoom(cameraPlayer, camInitZoom, zoomSlider);
    }


    public void Shoot()
    {
        weapon.Shoot(cameraPlayer.transform);
    }

    public void Aim()
    {
        if (isScope == false)
        {
            isScope = true;
            weapon.EnableScope();
            ScopeUI.SetActive(true);
        }
        else
        {
            isScope = false;
            weapon.DisableScope();
            ScopeUI.SetActive(false);
        }
    }

    public void Reload()
    {
        weapon.Reload();
    }
}
