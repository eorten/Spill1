using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    public float offsetY = 0f;
    public float offsetX = 0f;
    public float offsetZ = 0f;
    private int sens = 100;
    float xRotation = 0f;
    float yRotation = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void LateUpdate()
    {
        Rotate();
        Move();
    }

    void Rotate()
    {
        float y = Input.GetAxis("Mouse Y") * Time.deltaTime * sens;
        float x = Input.GetAxis("Mouse X") * Time.deltaTime * sens;

        xRotation -= y;
        yRotation -= x;
        xRotation = Mathf.Clamp(xRotation, -89, 89);

        //Vector3 rotateValue = new Vector3(y, x * -1, 0);
        transform.localEulerAngles = new Vector3(xRotation, -1*yRotation, 0);
    }

    void Move()
    {
        transform.position = new Vector3(player.position.x + offsetX, player.position.y + offsetY, player.position.z + offsetZ);
    }
}
