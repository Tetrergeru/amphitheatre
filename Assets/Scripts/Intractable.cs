using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Intractable : MonoBehaviour
{
    public abstract void OnInteract(MoodController.State state);
}
