using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinHandler : MonoBehaviour
{
    [SerializeField] Canvas youWinCanvas;
    void Start(){
        youWinCanvas.enabled = false;
    }

    public void HandleWin(){
        youWinCanvas.enabled = true;
        Time.timeScale = 0;
        FindObjectOfType<WeaponSwitcher>().enabled = false; 
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
