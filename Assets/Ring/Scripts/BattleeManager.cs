﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class BattleeManager : IActorManagerInterface
{
    // Start is called before the first frame update
    private CapsuleCollider defCol;

    //public ActorManager am;
    void Start()
    {
        defCol = GetComponent<CapsuleCollider>();
        defCol.center = Vector3.up* 1.0f;
        defCol.height = 2.0f;
        defCol.radius = 0.5f;
        defCol.isTrigger = true;

    }

    //// Update is called once per frame
    //void Update()
    //{

    //}

    private void OnTriggerEnter(Collider col)
    {
        //print(col.name);
        if(col.tag == "weapon")
        {
            am.TryDoDamage();
        }
    }
}
