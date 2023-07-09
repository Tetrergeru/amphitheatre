using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoodController : MonoBehaviour
{
    public Animator Animator;

    private const string MoodSlider = "_SadToHappy";
    private const string CutSlider = "_Cut";

    public enum State
    {
        Neutral = 0,
        Sad = 1,
        Happy = 2,
        Transit,
    }
    State state = State.Neutral;

    HashSet<Collider2D> collisions = new HashSet<Collider2D>();

    void Update()
    {
        var sprite = GetComponent<SpriteRenderer>();

        if (state == State.Neutral)
        {
            sprite.color = new Color(1, 1, 1, 0);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            foreach (var c in collisions)
                DisableState(c, state);
            switch (state)
            {
                case State.Neutral:
                    StartCoroutine(FirstState(sprite, State.Happy));
                    break;

                case State.Happy:
                    StartCoroutine(ChangeState(sprite, State.Sad));
                    break;

                case State.Sad:
                    StartCoroutine(ChangeState(sprite, State.Happy));
                    break;
            }
        }

        if (state != State.Transit)
        {
            SetCut(sprite, 0);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }
    }

    IEnumerator FirstState(SpriteRenderer sprite, State toState)
    {
        state = State.Transit;

        sprite.color = new Color(1, 1, 1, 1);
        sprite.material.SetFloat(MoodSlider, toState == State.Happy ? 1 : 0);

        var cutStart = 1f;
        SetCut(sprite, cutStart);

        for (var i = 0; i < 40; i++)
        {
            SetCut(sprite, Mathf.Pow(cutStart, 2));

            if (i == 10)
            {
                Animator.SetInteger("Mood", (int)toState);
            }

            cutStart -= (1f / 40);

            yield return new WaitForFixedUpdate();
        }

        state = toState;
        foreach (var c in collisions)
            EnableState(c, state);
    }

    IEnumerator ChangeState(SpriteRenderer sprite, State toState)
    {
        sprite.color = new Color(1, 1, 1, 1);
        var cutStart = 0.1f;
        state = State.Transit;
        for (var i = 0; i < 9; i++)
        {
            cutStart += 0.1f;
            SetCut(sprite, cutStart);
            yield return new WaitForFixedUpdate();
        }

        sprite.material.SetFloat(MoodSlider, toState == State.Happy ? 1 : 0);

        for (var i = 0; i < 9; i++)
        {
            cutStart -= 0.1f;
            SetCut(sprite, cutStart);
            yield return new WaitForFixedUpdate();
        }

        state = toState;
        Animator.SetInteger("Mood", (int)state);
        foreach (var c in collisions)
            EnableState(c, state);
    }

    private void SetCut(SpriteRenderer sprite, float cut)
    {
        sprite.material.SetFloat(CutSlider, cut + (Mathf.Sin(Time.time * 2) * 0.5f + 0.5f) * 0.1f + 0.1f);
    }

    void EnableState(Collider2D collision, State s)
    {
        var animator = collision.GetComponent<Animator>();
        if (animator != null)
        {
            var mood = s.ToString();
            if (animator.parameters.Select(a => a.name).Contains(mood))
                animator.SetTrigger(mood);
        }
        var intractable = collision.GetComponent<Intractable>();
        if (intractable != null)
            intractable.OnInteract(state);
    }

    void DisableState(Collider2D collision, State s)
    {
        var animator = collision.GetComponent<Animator>();
        if (animator != null)
        {
            var mood = s.ToString();
            if (animator.parameters.Select(a => a.name).Contains(mood))
                animator.ResetTrigger(mood);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        collisions.Add(collision);
        EnableState(collision, state);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        collisions.Remove(collision);
        DisableState(collision, state);
    }
}
