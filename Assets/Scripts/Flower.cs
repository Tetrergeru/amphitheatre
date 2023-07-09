using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    public AudioSource JumpAudio;

    public Animator animator;
    public float JumpMultiplier = 1;

    void OnCollisionStay2D(Collision2D collision)
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Enabled")) return;
        animator.SetTrigger("Jump");
        var player = collision.collider.GetComponent<CharacterController>();
        player.Jump(JumpMultiplier, transform.TransformDirection(Vector3.up));
        JumpAudio.Play();
    }
}
