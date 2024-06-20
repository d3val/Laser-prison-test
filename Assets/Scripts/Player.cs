using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class Player : MonoBehaviour
{

    //Movement variables
    [Header("Movement parameters")]
    [SerializeField] float moveSpeed = 10.0f;
    [SerializeField] float rotationSpeed = 100.0f;
    [SerializeField] float invulnerabilityTime = 3.0f;
    private int invulnerabilityCount = 0;
    private bool isRecharging = false;
    private bool isInvencible = false;
    private float xAxisValue = 0;
    private float yAxisValue = 0;

    //Input system variables
    [Header("Input Action Asset")]
    [SerializeField] InputActionAsset primaryInputs;
    private InputActionMap gameplayMap;
    private InputAction moveForwardAction;
    private InputAction rotateAction;
    private InputAction invulnerabilityAction;



    private void Awake()
    {
        gameplayMap = primaryInputs.FindActionMap("Gameplay");
        moveForwardAction = gameplayMap.FindAction("ForwardMove");
        rotateAction = gameplayMap.FindAction("Rotate");
        invulnerabilityAction = gameplayMap.FindAction("Invulnerability");

        moveForwardAction.performed += ReadVerticalAxis;
        moveForwardAction.canceled += ReadVerticalAxis;

        rotateAction.performed += ReadHorizontalAxis;
        rotateAction.canceled += ReadHorizontalAxis;

        invulnerabilityAction.performed += StartInvulnerability;
    }

    private void OnEnable()
    {
        moveForwardAction.Enable();
        rotateAction.Enable();
        invulnerabilityAction.Enable();
    }

    private void OnDisable()
    {
        moveForwardAction.Disable();
        rotateAction.Disable();
        invulnerabilityAction.Disable();
    }

    //Methods that read input values for movement
    private void ReadVerticalAxis(InputAction.CallbackContext ctx)
    {
        yAxisValue = ctx.ReadValue<float>();
    }
    private void ReadHorizontalAxis(InputAction.CallbackContext ctx)
    {
        xAxisValue = ctx.ReadValue<float>();
    }

    private void StartInvulnerability(InputAction.CallbackContext ctx)
    {
        if (isRecharging)
            return;

        StartCoroutine(Invulnerability());
    }

    //Controls the invulnerability power up
    private IEnumerator Invulnerability()
    {
        isRecharging = true;
        isInvencible = true;
        GetComponent<MeshRenderer>().material.color = Color.green;
        yield return new WaitForSeconds(invulnerabilityTime);
        isInvencible = false;
        invulnerabilityCount++;
        GetComponent<MeshRenderer>().material.color = Color.red;
        yield return new WaitForSeconds(3 + (1 * invulnerabilityCount));
        GetComponent<MeshRenderer>().material.color = Color.white;
        isRecharging = false;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    //Moves the player
    private void Move()
    {
        transform.position += Time.deltaTime * moveSpeed * yAxisValue * transform.forward;

        transform.Rotate(transform.up, Time.deltaTime * rotationSpeed * xAxisValue);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Laser") && !isInvencible)
            GameManager.instance.DecreaseLife();
    }
}
