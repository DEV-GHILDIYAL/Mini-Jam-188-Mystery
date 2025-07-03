using UnityEngine;

public class Fan : MonoBehaviour
{
    public float maxSpeed = 300f;
    public float acceleration = 50f;
    public float deceleration = 50f;
    float currentSpeed = 0f;
    bool isOn = false;

    private void Update() {
        if (isOn) {
            currentSpeed = Mathf.Lerp(currentSpeed, maxSpeed, Time.deltaTime * acceleration);
        }
        else {
            currentSpeed = Mathf.Lerp(currentSpeed, 0f, Time.deltaTime * deceleration);
        }

        transform.Rotate(Vector3.up * currentSpeed * Time.deltaTime);
    }

    public void ToggleFan() {
        isOn = !isOn;
    }
}
