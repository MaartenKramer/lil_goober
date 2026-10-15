using UnityEngine;

public class FrequencyLine : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private int pointAmount;

    public float amplitude = 1;
    public float wavelength = 1;
    [SerializeField] private Vector2 xLimits = new Vector2(0, 1);
    [SerializeField] private float moveSpeed = 1;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        DrawLine();
    }

    private void DrawLine()
    {
        float xStart = xLimits.x;
        float xFinish = xLimits.y;
        float tau = Mathf.PI * 2;

        lineRenderer.positionCount = pointAmount;

        for (int currentPoint = 0; currentPoint < pointAmount; currentPoint++)
        {
            float progress = (float)currentPoint / (pointAmount - 1);
            float x = Mathf.Lerp(xStart, xFinish, progress);
            float y = amplitude * Mathf.Sin((tau * wavelength * x) + (Time.timeSinceLevelLoad * moveSpeed));
            lineRenderer.SetPosition(currentPoint, new Vector3(x, y, 0));
        }
    }
}
