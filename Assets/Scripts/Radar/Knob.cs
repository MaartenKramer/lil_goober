using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Knob : MonoBehaviour
{
    [SerializeField] private Transform handle;
    [SerializeField] private Image fill;
    public float knobValue;

    [SerializeField] private InputActionReference mouseInput;

    private Vector3 mousePos;

    public void OnKnobDrag()
    {
        mousePos = new Vector3(Mouse.current.position.x.ReadValue(), Mouse.current.position.y.ReadValue(), 0f);
        Vector2 direction = mousePos - handle.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle = (angle <= 0) ? (360 + angle) : angle;

        if (angle <= 225 || angle >= 315)
        {
            Quaternion rotation = Quaternion.AngleAxis(angle + 135f, Vector3.forward);
            handle.localRotation = rotation;
            angle = ((angle >= 315) ? (angle - 360) : angle) + 45;
            fill.fillAmount = 0.75f - (angle / 360f);
            knobValue = Mathf.Round((fill.fillAmount * 100) / 0.75f) / 100;
        }
    }
}
