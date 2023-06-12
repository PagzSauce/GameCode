using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeScript : MonoBehaviour
{
    private Vector3 cameraInitialPosition;
    [SerializeField] private float shakeMagnitude = 0.05f;
   // [SerializeField] private float shakeTime = 0.05f;
    [SerializeField] private Camera mainCamera;


   
    private void ShakeIt()
    {
        cameraInitialPosition = mainCamera.transform.position;
        InvokeRepeating("StartCameraShake", 0f, 0.005f);
        //Invoke("StopCameraShaking", shakeTime);
    }

    private void StartCameraShake()
    {
        float cameraShakingOffsetX = Random.value * shakeMagnitude * 2 - shakeMagnitude;
        float cameraShakingOffsetY = Random.value * shakeMagnitude * 2 - shakeMagnitude;
        Vector3 cameraIntermediatePosition = mainCamera.transform.position;
        cameraIntermediatePosition.x += cameraShakingOffsetX;
        cameraIntermediatePosition.y += cameraShakingOffsetY;
        mainCamera.transform.position = cameraIntermediatePosition;
    }

    private void StopCameraShaking()
    {
        CancelInvoke("StartCameraShake");
        mainCamera.transform.position = cameraInitialPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        cameraInitialPosition = mainCamera.transform.position;

        if(collision.gameObject.name.Contains("EQLevel1"))
        {
            shakeMagnitude = 0.02f;
        }
        else if (collision.gameObject.name.Contains("EQLevel2"))
        {
            shakeMagnitude = 0.1f;
        }

        if (collision.gameObject.CompareTag("EQStart"))
        {
            ShakeIt();
        }
        else if (collision.gameObject.CompareTag("EQEnd"))
        {
            StopCameraShaking();
        }
    }
}
