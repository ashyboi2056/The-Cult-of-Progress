using UnityEngine;
using UnityEngine.InputSystem;
using NaughtyAttributes;

public class PLAYER_Movement : DEBUGMonoBehaviour
{
    [Header("Movement Settings")]
    [ShowNonSerializedField]
    private static float moveSpeed = 5f;

    private Vector2 moveInput;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Called by the Input System when Move action is triggered
    public void OnMove(InputValue inputValue)
    {
        moveInput = inputValue.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = moveInput.normalized * moveSpeed;
    }
}
