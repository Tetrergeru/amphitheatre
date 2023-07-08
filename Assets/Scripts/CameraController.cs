using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    public float speed = 1;
    public float sizeSpeed = 1;
    public float defaultSize = 5;
    public Transform player;
    public CameraPoint[] points;

    private Camera cameraComponent;

    void Start()
    {
        cameraComponent = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        Vector2 target = player.transform.position;
        float size = defaultSize;
        foreach (var point in points) {
            if (!point.Enter) continue;
            var p = point.transform.position;
            var offset = point.colliderComponent.offset;
            target = offset + new Vector2(p.x, p.y);
            var pointSize = point.colliderComponent.size;
            var aspect = pointSize.x / pointSize.y;
            if (aspect < cameraComponent.aspect)
                size = pointSize.y;
            else
                size = pointSize.x / cameraComponent.aspect;
            break;
        }

        Vector3 target3 = new Vector3(target.x, target.y, transform.position.z);
        var delta = transform.position - target3;
        var distance = speed * Time.deltaTime;
        transform.position = delta.magnitude < distance ?
            target3 : transform.position - delta.normalized * distance;

        size = size / 2;
        var sizeDistance = sizeSpeed * Time.deltaTime;
        var deltaSize = cameraComponent.orthographicSize - size;
        cameraComponent.orthographicSize = Mathf.Abs(deltaSize) < sizeDistance ?
            size : cameraComponent.orthographicSize - sizeDistance * Mathf.Sign(deltaSize);
    }
}
