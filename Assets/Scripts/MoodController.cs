using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoodController : MonoBehaviour
{
    public enum State { Neutral, Sad, Happy }
    State state = State.Neutral;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            switch(state) {
                case State.Neutral: state = State.Sad; break;
                case State.Happy: state = State.Sad; break;
                case State.Sad: state = State.Happy; break;
            }
        }
        var sprite = GetComponent<SpriteRenderer>();
        switch(state) {
            case State.Neutral: sprite.color = new Color(0, 0, 0, 0); break;
            case State.Happy: sprite.color = new Color(1, 1, 0.5f, 0.5f); break;
            case State.Sad: sprite.color = new Color(0, 0, 0); break;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        var animator = collision.GetComponent<Animator>();
        if (animator != null)
        {
            var mood = state.ToString();
            if (animator.parameters.Select(a => a.name).Contains(mood))
                animator.SetTrigger(mood);
        }
        var intractable = collision.GetComponent<Intractable>();
        if (intractable != null)
            intractable.OnInteract(state);
    }
}
