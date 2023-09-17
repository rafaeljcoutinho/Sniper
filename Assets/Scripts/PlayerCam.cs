using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerCam : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float cameraSensitivity;
    [SerializeField] private Slider zoomSlider;

    Vector2 lookInput;
    float cameraPitch;

    float camCenter;

    private void Awake()
    {
        camCenter = Screen.width / 2;
    }

    void Update()
    {
        GetTouchInput();
        LookAround();

    }

    void GetTouchInput()
    {
        if(Input.touchCount > 0)
        {
            foreach (Touch i in Input.touches)
            {
                if(i.position.x > camCenter)
                {
                    if (i.phase == TouchPhase.Moved)
                    {
                        if(i.deltaPosition.magnitude > .1f)
                            lookInput = i.deltaPosition * cameraSensitivity * zoomSlider.value * Time.deltaTime;
                    }
                    else if (i.phase == TouchPhase.Stationary)
                    {
                        lookInput = Vector2.zero;
                    }
                }
            }
        }
        else
        {
            lookInput = Vector2.zero;
        }
    }
    void LookAround()
    {
        cameraPitch = Mathf.Clamp(cameraPitch - lookInput.y, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(cameraPitch, 0, 0);
        transform.Rotate(transform.up, lookInput.x);
    }
}
