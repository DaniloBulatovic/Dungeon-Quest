using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float sensitivity = 3.0f;

    void Update()
    {
        #if UNITY_EDITOR
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        transform.eulerAngles += new Vector3(-mouseY * sensitivity, mouseX * sensitivity, 0);
        #endif
    }
}
