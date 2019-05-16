using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : IActorManagerInterface
{
    private Collider weaponColL;
    private Collider weaponColR;
    //public ActorManager am;

    public GameObject whL;
    public GameObject whR;

    private void Start()
    {
        whL = transform.DeepFind("weaponhandleL").gameObject;
        whR = transform.DeepFind("weaponhandleR").gameObject;
        //weaponCol = whR.GetComponentInChildren<Collider>();
        //transform.DeepFind("123");

        weaponColL = whL.GetComponentInChildren<Collider>();
        weaponColR = whR.GetComponentInChildren<Collider>();

    }

    public void WeaponEnable()
    {
        if (am.ac.CheckStateTag("attackL"))
        {

        weaponColL.enabled = true;
        }
        else
        {
            weaponColR.enabled = true;
        }
    }
    public void WeaponDisable()
    {
        //print("WeaponDisable");
        weaponColR.enabled = false;
        weaponColL.enabled = false;
    }
}
