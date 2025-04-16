using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using StarterAssets;
using UnityEngine;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] GameObject playerFollowCamera;
    [SerializeField] Camera hitImpactCamera;
    [SerializeField] Camera weaponCamera;
    [SerializeField][Range(0,80)] float zoomInFOV = 20f;
    [SerializeField][Range(0,80)] float zoomOutFOV = 60f;
    [SerializeField][Range(0,80)] float overlayCamerasFOV = 40f;
    CinemachineVirtualCamera mainCamera;
    FirstPersonController FPSController;
    float FPSControllerLookSpeed;
    float FPSControllerLookSpeedZoomed;

    void Start()
    {
        mainCamera = playerFollowCamera.GetComponent<CinemachineVirtualCamera>();

        FPSController = GetComponent<FirstPersonController>();
        FPSControllerLookSpeed = FPSController.RotationSpeed;
        FPSControllerLookSpeedZoomed = FPSControllerLookSpeed * 0.5f;

        // To ensure that they have the same values
        hitImpactCamera.fieldOfView = overlayCamerasFOV;
        weaponCamera.fieldOfView = overlayCamerasFOV;
    }

    private void Update() {
        if(Input.GetMouseButtonDown(1))
        {
            StopAllCoroutines();
            FPSController.RotationSpeed = FPSControllerLookSpeedZoomed;
            StartCoroutine(ChangeFOV(zoomInFOV, 0.1f));
        }
        else if(Input.GetMouseButtonUp(1)){
            StopAllCoroutines();
            FPSController.RotationSpeed = FPSControllerLookSpeed;
            StartCoroutine(ChangeFOV(zoomOutFOV, 0.1f));
        }
    }

    IEnumerator ChangeFOV(float endFOV, float duration)
    {
        float startFOV = mainCamera.m_Lens.FieldOfView;

        int multiplier = endFOV == zoomInFOV ? -1 : 1;

        // The hitImpact & Weapon Overlay cameras share same FOV values.
        float overlayStartFOV = weaponCamera.fieldOfView;
        float overlayEndFOV = overlayCamerasFOV += zoomInFOV * multiplier;

        float time = 0;

        while(time < duration)
        {
            mainCamera.m_Lens.FieldOfView = Mathf.Lerp(startFOV, endFOV, time / duration);
            hitImpactCamera.fieldOfView = Mathf.Lerp(overlayStartFOV, overlayEndFOV, time / duration);
            weaponCamera.fieldOfView = Mathf.Lerp(overlayStartFOV, overlayEndFOV, time / duration);

            yield return null;
            time += Time.deltaTime;
        }
    }
}
