using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rose : MonoBehaviour
{
    public Animator animator;

    void OnCollisionStay2D(Collision2D collision)
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Enabled")) return;
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
}
