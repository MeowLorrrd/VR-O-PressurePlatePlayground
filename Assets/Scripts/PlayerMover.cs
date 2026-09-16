using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public sealed class PlayerMover : MonoBehaviour
{
    [SerializeField] private float speed = 0f;

    private Rigidbody body;
    private bool slidingToHalt;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }
    
    
    private void FixedUpdate()
    {
        Vector2 input = ReadInput();
        Vector3 horizontalVelocity = new(input.x, 0, input.y);
        body.linearVelocity = new Vector3(horizontalVelocity.x, 0, horizontalVelocity.z) * speed;
    }

    private static Vector2 ReadInput()
    {
        Keyboard keyboard = Keyboard.current;
        Vector2 input = Vector2.zero;
        if (null == keyboard) { return input; }
    
        if (keyboard[Key.W].IsPressed() || keyboard[Key.UpArrow].IsPressed()) { input.y += 1.0f; }
        if (keyboard[Key.S].IsPressed() || keyboard[Key.DownArrow].IsPressed()) { input.y -= 1.0f; }
        if (keyboard[Key.D].IsPressed() || keyboard[Key.RightArrow].IsPressed()) { input.x += 1.0f; }
        if (keyboard[Key.A].IsPressed() || keyboard[Key.LeftArrow].IsPressed()) { input.x -= 1.0f; }
        return input;
    }
}
