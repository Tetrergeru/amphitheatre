using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(BoxCollider2D))]
public class CameraPoint : MonoBehaviour
{
    [NonSerialized]
    public BoxCollider2D colliderComponent;
    public bool Enter {get; private set;}

    void Start() {
        colliderComponent = GetComponent<BoxCollider2D>();
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        Enter = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        Enter = false;
    }
}
