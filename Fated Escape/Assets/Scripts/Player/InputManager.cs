using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [HideInInspector] public float vertical;
    [HideInInspector] public float horizontal;
    [HideInInspector] public float xValue, yValue;

    public float sensitivity = 1f;

    private void FixedUpdate()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        xValue += Input.GetAxis("Mouse X") * sensitivity;
        yValue += Input.GetAxis("Mouse Y") * -1 * sensitivity;
    }
}
