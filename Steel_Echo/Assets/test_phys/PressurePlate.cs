using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour
{
    public UnityEvent onPressed;
    public UnityEvent onReleased;

    private int objectsOnPlate = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        objectsOnPlate++;
        if (objectsOnPlate == 1)
        {
            sr.color = pressedColor;
            onPressed.Invoke();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        objectsOnPlate--;

        if(objectsOnPlate == 0)
        {
            sr.color = defaultColor;
            onReleased.Invoke();
        }
    }

    private SpriteRenderer sr;
    public Color pressedColor = Color.green;
    public Color defaultColor = Color.red;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = defaultColor;
    }

}
