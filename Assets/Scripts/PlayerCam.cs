using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float cameraSensitivity;

    Vector2 lookInput;
    float cameraPitch;


    void Update()
    {
        GetTouchInput();
        LookAround();

    }

    void GetTouchInput()
    {
        if(Input.touchCount > 0)
        {
            if(Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                lookInput = Input.GetTouch(0).deltaPosition * cameraSensitivity * Time.deltaTime;
            }
            else if(Input.GetTouch(0).phase == TouchPhase.Stationary)
            {
                lookInput = Vector2.zero;
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
