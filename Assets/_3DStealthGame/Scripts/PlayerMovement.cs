using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    Animator m_Animator;

    public InputAction MoveAction;
    public InputAction BoostAction;

    public float walkSpeed = 1.0f;
    public float turnSpeed = 20f;
    public float boostSpeed = 3.5f;
    public float boostDuration = 2f;
    public float boostCooldown = 5f;

    public Image boostUI;

    Rigidbody m_Rigidbody;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;

    bool isBoosting = false;
    bool canBoost = true;
    float currentSpeed;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        MoveAction.Enable();
        BoosterSetup();
        currentSpeed = walkSpeed;
        UpdateBoostUI();
        m_Animator = GetComponent<Animator>();
    }

    void BoosterSetup()
        {
            BoostAction.Enable();
            BoostAction.performed += ctx => TryBoost();
        }

    void FixedUpdate()
    {
        Vector2 input = MoveAction.ReadValue<Vector2>();
        float horizontal = input.x;
        float vertical = input.y;

        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();

        bool isWalking = horizontal != 0 || vertical != 0;
        m_Animator.SetBool("IsWalking", isWalking);

        if (m_Movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(m_Movement, Vector3.up);
            m_Rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 720 * Time.deltaTime);
            m_Rigidbody.MoveRotation(m_Rotation);
        }

        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * currentSpeed * Time.deltaTime);
    }

    void TryBoost()
    {
        if (canBoost && !isBoosting)
            StartCoroutine(BoostRoutine());
    }

    IEnumerator BoostRoutine()
    {
        isBoosting = true;
        canBoost = false;

        currentSpeed = boostSpeed;
        UpdateBoostUI();

        yield return new WaitForSeconds(boostDuration);

        currentSpeed = walkSpeed;
        isBoosting = false;
        UpdateBoostUI();

        yield return new WaitForSeconds(boostCooldown);

        canBoost = true;
        UpdateBoostUI();
    }

    void UpdateBoostUI()
    {
        if (boostUI == null) return;

        if (isBoosting)
            boostUI.color = Color.green;
        else if (!canBoost)
            boostUI.color = Color.red;
        else
            boostUI.color = Color.white;
    }

}
