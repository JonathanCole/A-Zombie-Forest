using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] Camera FPSCamera;
    [SerializeField] float zoomedOut = 60f;
    [SerializeField] float zoomedIn = 20f;
    bool isZoomedIn = false;
    [SerializeField] float zoomedOutSensitivity = 2f;
    [SerializeField] float zoomedInSensitivity = 1f;
    [SerializeField] RigidbodyFirstPersonController fpsController;

    private void OnDisable(){
        ZoomOut();
    }

    void Update(){
        if(Input.GetMouseButtonDown(1)){
            if(isZoomedIn == false){                
                ZoomIn();
            }
            else{              
                ZoomOut();
            }
        }
    }

    void ZoomIn(){
        isZoomedIn = true;
        FPSCamera.fieldOfView = zoomedIn;
        fpsController.mouseLook.XSensitivity = zoomedInSensitivity;
        fpsController.mouseLook.YSensitivity = zoomedInSensitivity;
    }

    void ZoomOut(){
        isZoomedIn = false;
        FPSCamera.fieldOfView = zoomedOut;
        fpsController.mouseLook.XSensitivity = zoomedOutSensitivity;
        fpsController.mouseLook.YSensitivity = zoomedOutSensitivity;
    }
}
