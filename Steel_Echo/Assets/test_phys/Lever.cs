using UnityEngine;
using UnityEngine.Events;

public class Lever : MonoBehaviour, IInteractable
{
    public bool isOn;

    public UnityEvent onTurnOn;
    public UnityEvent onTurnOff;

    private SpriteRenderer sr;

    public Color onColor = Color.green;
    public Color offColor = Color.red;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        UpdateVisual();
    }

    public void Interact()
    {
        isOn = !isOn;

        UpdateVisual();

        if (isOn)
            onTurnOn.Invoke();
        else
            onTurnOff.Invoke();
    }

    void UpdateVisual()
    {
        sr.color = isOn ? onColor : offColor;
    }
}