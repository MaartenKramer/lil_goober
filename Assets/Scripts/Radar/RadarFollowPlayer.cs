using UnityEngine;

public class RadarFollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform playerIconTransform;

    private void Update()
    {
        gameObject.transform.position = playerTransform.transform.position;
        playerIconTransform.rotation = playerTransform.rotation;
    }
}
