using System.Collections;
using UnityEngine;

public class LookTarget : MonoBehaviour
{
    public static LookTarget instance;
    public Transform cameraTransform;
    public bool isLooking = false;
    private Transform target;
    private void Awake() {
        instance = this;
    }

    public void LookAt(Transform newTarget, float rotationSpeed = 2f) {
        target = newTarget;
        isLooking = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        FirstPersonController.instance.talking = true;
        StartCoroutine(RotateCamera(rotationSpeed));

    }

    public void StopLooking() {
        isLooking = false;
        FirstPersonController.instance.talking = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    IEnumerator RotateCamera(float rotationSpeed) {
        while(isLooking && target != null) {
            Debug.Log("Hello");
            Vector3 direction = target.position - cameraTransform.position;
            Quaternion targetRoattion = Quaternion.LookRotation(direction);
            cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, targetRoattion, rotationSpeed * Time.deltaTime);
            if(Quaternion.Angle(cameraTransform.rotation, targetRoattion) < 1f) {
                isLooking = false;
            }
        yield return null;
        }
    }
}
