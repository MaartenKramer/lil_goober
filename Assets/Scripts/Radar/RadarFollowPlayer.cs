using UnityEngine;

public class RadarFollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform playerIconTransform;

    [SerializeField] private bool rotateRadar;

    private void Update()
    {
        gameObject.transform.position = playerTransform.transform.position;

        if (rotateRadar)
            gameObject.transform.rotation = playerTransform.rotation;
        else
            playerIconTransform.rotation = playerTransform.rotation;
    }
}
