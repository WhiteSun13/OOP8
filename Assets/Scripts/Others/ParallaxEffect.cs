using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] Transform followingTarget;
    [SerializeField, Range(0f,1f)] float parallaxSrenght = 0.1f;
    [SerializeField] float YparallaxSrenght = 0f;
    [SerializeField] bool VerticalParallax;
    Vector3 targetPreviousPosition;

    void Start()
    {
        if (!followingTarget) followingTarget = Camera.main.transform;

        targetPreviousPosition = followingTarget.position;
    }

    void Update()
    {
        var delta = followingTarget.position - targetPreviousPosition;

        if (VerticalParallax) delta.y = delta.y * YparallaxSrenght;

        targetPreviousPosition = followingTarget.position;
        transform.position += delta * parallaxSrenght;
    }
}
