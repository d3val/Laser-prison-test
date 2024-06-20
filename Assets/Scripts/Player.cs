using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class Player : MonoBehaviour
{

    //Input system variables
    [SerializeField] InputActionAsset primaryInputs;
    private InputActionMap gameplayMap;
    private InputAction moveForwardAction;
    private InputAction rotateAction;

    //Movement variables
    [SerializeField] float moveSpeed = 10.0f;
    [SerializeField] float rotationSpeed = 100.0f;
    private float xAxisValue = 0;
    private float yAxisValue = 0;

    private void Awake()
    {
        gameplayMap = primaryInputs.FindActionMap("Gameplay");
        moveForwardAction = gameplayMap.FindAction("ForwardMove");
        rotateAction = gameplayMap.FindAction("Rotate");

        moveForwardAction.performed += ReadVerticalAxis;
        moveForwardAction.canceled += ReadVerticalAxis;

        rotateAction.performed += ReadHorizontalAxis;
        rotateAction.canceled += ReadHorizontalAxis;
    }

    private void OnEnable()
    {
        moveForwardAction.Enable();
        rotateAction.Enable();
    }

    private void OnDisable()
    {
        moveForwardAction.Disable();
        rotateAction.Disable();
    }
    //Methods that read input values
    private void ReadVerticalAxis(InputAction.CallbackContext ctx)
    {
        yAxisValue = ctx.ReadValue<float>();
    }
    private void ReadHorizontalAxis(InputAction.CallbackContext ctx)
    {
        xAxisValue = ctx.ReadValue<float>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        transform.position += Time.deltaTime * moveSpeed * yAxisValue * transform.forward;

        transform.Rotate(transform.up, Time.deltaTime * rotationSpeed * xAxisValue);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Laser"))
            Debug.Log("Ay!!!");
    }
}
