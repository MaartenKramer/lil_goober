using UnityEngine;

public class Radar : MonoBehaviour
{
    [SerializeField] private Transform sweep;

    [SerializeField] private float rotationSpeed = 150f;
    [SerializeField] private float radarRange;

    void Start()
    {
        
    }
    
    void Update()
    {
        sweep.eulerAngles += new Vector3(0f, rotationSpeed * Time.deltaTime, 0f);
    }
}
