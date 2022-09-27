using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    public Transform cameraPosition;
    public float mouseSensitivity;
    public bool invertX;
    public bool invertY;
    public float JumpVelosity;
    private CharacterController characterController;
    private Vector3 movementVector;
    private Rigidbody rB;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        rB = GetComponent<Rigidbody>();
    }
    void Update()
    {
        //movementVector.x = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
        //movementVector.z = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;
        Vector3 movementHorizontal = Input.GetAxis("Horizontal") * transform.forward;
        Vector3 movementVertical = Input.GetAxis("Vertical") * -transform.right;

        movementVector = movementHorizontal + movementVertical;
        movementVector = movementVector * movementSpeed * Time.deltaTime;

        characterController.Move(movementVector);

        Vector2 mouseVector = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;

        if(invertX)
        {
            mouseVector.x = -mouseVector.x;
        }
        if (invertY)
        {
            mouseVector.y = -mouseVector.y;
        }

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseVector.x, transform.rotation.eulerAngles.z);
        cameraPosition.rotation = Quaternion.Euler(cameraPosition.rotation.eulerAngles + new Vector3(mouseVector.y, 0f, 0f));
    }
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rB.AddForce(Vector3.up * JumpVelosity, ForceMode.Impulse);
        }
    }
}
