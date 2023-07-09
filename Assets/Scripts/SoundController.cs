using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MoodController;

public class SoundController : MonoBehaviour
{
    public GameObject MusicSourcePrefab;

    private SoundScript _sound;

    public void SetMood(State mood, float percent)
    {
        switch (mood)
        {
            case State.Happy:
                _sound.Source.pitch = 0.8f + percent * 0.4f;
                break;

            case State.Sad:
                _sound.Source.pitch = 1.2f - percent * 0.4f;
                break;

            case State.Neutral:
                _sound.Source.pitch = 1;
                break;
        }
    }

    public void Normalize()
    {
        StartCoroutine(NormalizationCoroutine());
    }

    private IEnumerator NormalizationCoroutine()
    {
        var start = _sound.Source.pitch;
        var step = (start - 1) / 9;

        for (var i = 0; i < 9; i++)
        {
            _sound.Source.pitch -= step;
            yield return new WaitForFixedUpdate();
        }

        _sound.Source.pitch = 1;
    }

    private void Start()
    {
        _sound = FindObjectOfType<SoundScript>();
        if (_sound == null)
        {
            var obj = Instantiate(MusicSourcePrefab);
            DontDestroyOnLoad(obj);
            _sound = obj.GetComponent<SoundScript>();
        }
        Normalize();
    }
}
