using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController : MonoBehaviour
{
    enum State { Walk, Jump }

    public float speed = 10;
    public float jump = 30;
    public float coyoteTime = 0.1f;
    public float jumpTime = 1;
    public float jumpHeight = 3;
    public float lowGravityScale = 0.5f;
    public AnimationCurve curve;

    Rigidbody2D body;
    float timer = 0.0f;
    float lastCurveValue;

    State state = State.Walk;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (state == State.Walk) {
            if (timer > 0)
            timer -= Time.deltaTime; // coyote time
        }

        if (state == State.Jump) {
            timer += Time.deltaTime / jumpTime;
            float curveValue = curve.Evaluate(timer);
            float delta = curveValue - lastCurveValue;
            lastCurveValue = curveValue;
            transform.position += Vector3.up * delta * jumpHeight;
            if (timer >= 1)
            {
                state = State.Walk;
                timer = 0;
            }
        }

        float horizontal = Input.GetAxis("Horizontal") * speed;
        float vertical = body.velocity.y;

        if (state == State.Walk) {
            if (Input.GetAxis("Jump") > 0.01f) {
                body.gravityScale = lowGravityScale;
                if (timer > 0) {
                    lastCurveValue = curve.Evaluate(0);
                    timer = 0;
                    state = State.Jump;
                    body.gravityScale = 0;
                    vertical = 0;
                }
            } else {
                body.gravityScale = 1.0f;
            }
        }

        body.velocity = Vector2.right * horizontal + Vector2.up * vertical;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (state == State.Walk)
            timer = coyoteTime;
    }
}
