using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;
    public float pauseTime = 2f;

    private Transform target;
    private bool isWaiting = false;

    void Start()
    {
        target = pointB;
    }

    void FixedUpdate()
    {
        if (pointA == null || pointB == null || isWaiting)
            return;

        Vector3 lastPosition = transform.position;

        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            speed * Time.fixedDeltaTime
        );

        Vector3 movement = transform.position - lastPosition;

        if (movement != Vector3.zero)
        {
            CarryPlayer(movement);
        }

        if (Vector3.Distance(transform.position, target.position) < 0.01f)
        {
            transform.position = target.position;
            StartCoroutine(PauseAtPoint());
        }
    }

    IEnumerator PauseAtPoint()
    {
        isWaiting = true;

        yield return new WaitForSeconds(pauseTime);

        target = target == pointA ? pointB : pointA;
        isWaiting = false;
    }

    void CarryPlayer(Vector3 movement)
    {
        Collider[] hits = Physics.OverlapBox(
            transform.position + Vector3.up * 0.1f,
            new Vector3(
                transform.localScale.x * 0.45f,
                0.15f,
                transform.localScale.z * 0.45f
            ),
            transform.rotation
        );

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                CharacterController controller =
                    hit.GetComponent<CharacterController>();

                if (controller == null)
                    controller = hit.GetComponentInParent<CharacterController>();

                if (controller != null)
                {
                    controller.Move(movement);
                }
                else
                {
                    hit.transform.root.position += movement;
                }
            }
        }
    }
}