using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovment : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private Transform playerCamera;
    [SerializeField]
    private TextMeshProUGUI playerPosText;
    private bool grounded = true;
    private int jumpHeight = 20;
    private int speed = 10;
    private int airSpeed = 10000;

    private void Start()
    {
        InvokeRepeating("SetPos", 0, 3);
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            Jump();
        }
        GroundCheck();
        
    }
    private void SetPos()
    {
        playerPosText.text = playerPosText.text = "Player pos: " +"\n"+ "[" + (int)transform.position.x + "/" + (int)transform.position.z + "]";
    }
    private void Move()
    {
        if (grounded)
        {
            rb.MovePosition(transform.position + (transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical")) * speed * Time.deltaTime);
        }
        else if(!grounded)
        {
            rb.AddRelativeForce(new Vector3(Input.GetAxis("Horizontal") * airSpeed*Time.deltaTime, 0, Input.GetAxis("Vertical") * airSpeed*Time.deltaTime), ForceMode.Force);
        }
        
    }
    private void Jump()
    {
        //LOCAL???
        rb.AddRelativeForce(new Vector3(Input.GetAxis("Horizontal") * speed, jumpHeight, Input.GetAxis("Vertical") * speed), ForceMode.Impulse);
        grounded = false;

    }
    private void Rotate() //Roterer spilleren til å peke samme vei som kamera
    {
        Vector3 lookDirection = new Vector3(transform.eulerAngles.x, playerCamera.eulerAngles.y, transform.eulerAngles.z);
        transform.eulerAngles = lookDirection;
    }
    private void GroundCheck()
    {
        RaycastHit hit;
        if (/*Physics.Raycast(transform.position, -Vector3.up, out hit, 1.2f)*/Physics.SphereCast(transform.position, 0.4f, -Vector3.up, out hit, 0.8f))

        {
            if (hit.transform.gameObject.tag == "Ground" || hit.transform.gameObject.tag == "Level")
            {
                //print("" + hit.transform.gameObject.tag);
                rb.velocity = new Vector3(0,0,0);
                grounded = true;
            }
            else
            {
                grounded = false;
            }
        }
    }
}
