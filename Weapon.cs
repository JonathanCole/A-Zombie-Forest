using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] float range = 100;
    [SerializeField] float damage = 30f;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitFX;
    [SerializeField] Ammo ammoSlot;
    [SerializeField] AmmoType ammoType;
    [SerializeField] float fireDelay = 0.5f;
    [SerializeField] TextMeshProUGUI ammoText;
    bool canShoot = true;


    void OnEnable(){
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        DisplayAmmo();
        if(Input.GetMouseButtonDown(0) && canShoot == true){
            StartCoroutine(Shoot());
        }
    }

    void DisplayAmmo(){
        int currentAmmo = ammoSlot.GetCurrentAmmo(ammoType);
        ammoText.text = ammoType.ToString() + ": " + currentAmmo.ToString();
    }

    IEnumerator Shoot(){
        canShoot = false;
        if(ammoSlot.GetCurrentAmmo(ammoType) > 0){
            ProcessHit();
            PlayMuzzleFlash();
            ammoSlot.ReduceCurrentAmmo(ammoType);
        }
        yield return new WaitForSeconds(fireDelay);
        canShoot = true;
    }

    void ProcessHit(){
        RaycastHit hit;
        if(Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range)){
            CreateHitImpact(hit);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if(target == null){return;}
            target.TakeDamage(damage);
        }
        else{
            return;
        }
        
    }

    void CreateHitImpact(RaycastHit hit){
        GameObject impact = Instantiate(hitFX, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, .1f);
    }

    void PlayMuzzleFlash(){
        muzzleFlash.Play();
    }
}
