using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoodController : MonoBehaviour
{
    public enum State { Neutral, Sad, Happy }
    State state = State.Neutral;

    HashSet<Collider2D> collisions = new HashSet<Collider2D>();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            foreach (var c in collisions)
                DisableState(c, state);
            switch (state)
            {
                case State.Neutral: state = State.Happy; break;
                case State.Happy: state = State.Sad; break;
                case State.Sad: state = State.Happy; break;
            }
            foreach (var c in collisions)
                EnableState(c, state);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        var sprite = GetComponent<SpriteRenderer>();
        switch (state)
        {
            case State.Neutral: sprite.color = new Color(0, 0, 0, 0); break;
            case State.Happy: sprite.color = new Color(1, 1, 0.5f, 0.5f); break;
            case State.Sad: sprite.color = new Color(0, 0, 0); break;
        }
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
