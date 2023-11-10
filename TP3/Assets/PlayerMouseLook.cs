using UnityEngine;

public class PlayerMouseLook : MonoBehaviour
{
    public float sensitivity = 300f;
    public Transform playerBody;

    float yRotation = 0f;

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;

        yRotation += mouseX;

        transform.localRotation = Quaternion.Euler(0f, yRotation, 0f);
    }
}