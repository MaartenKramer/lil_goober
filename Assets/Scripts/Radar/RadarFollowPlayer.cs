using UnityEngine;

public class RadarFollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;

    private void Update()
    {
        gameObject.transform.position = playerTransform.transform.position;
        gameObject.transform.rotation = playerTransform.rotation;
    }
}
