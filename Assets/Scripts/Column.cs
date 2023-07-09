using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Column : Intractable
{
    public AudioSource BreakSound;

    public Rigidbody2D body;
    public Vector2 Force;
    public Vector2 ForcePoint;

    bool enbl = true;

    public override void OnInteract(MoodController.State state)
    {
        if (state == MoodController.State.Sad && enbl)
        {
            body.AddForceAtPosition(Force, ForcePoint + new Vector2(transform.position.x, transform.position.y), ForceMode2D.Impulse);
            enbl = false;
            BreakSound.Play();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        var point = transform.position + new Vector3(ForcePoint.x, ForcePoint.y);
        Gizmos.DrawSphere(point, 0.1f);
        Gizmos.DrawLine(point, point + new Vector3(Force.x, Force.y, 0) / body.mass);
    }
}
