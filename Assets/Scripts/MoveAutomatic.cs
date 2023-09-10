using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAutomatic : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(Vector3.up, 0.1f);
    }
}
