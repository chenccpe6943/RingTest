using System.Collections;
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
        WeaponController targetWc = col.GetComponentInParent<WeaponController>();
        //print(col.name);
        GameObject attacker = targetWc.wm.am.gameObject;
        GameObject receiver = am.gameObject;

        Vector3 attackingdDir = receiver.transform.position - attacker.transform.position;
        Vector3 counterDir = attacker.transform.position - receiver.transform.position;

        float attackingAngle1 = Vector3.Angle(attacker.transform.forward, attackingdDir);
        float counterAngel1 = Vector3.Angle(receiver.transform.forward, counterDir);
        float counterAngel2 = Vector3.Angle(attacker.transform.forward, receiver.transform.forward);

        bool attackValid = (attackingAngle1 < 45);
        bool counterValid = (attackingAngle1 < 180 && Mathf.Abs(counterAngel2 - 180) < 180);

        if (col.tag == "weapon")
        {
            //if(attackingAngle1 <= 45)
            //{
           am.TryDoDamage(targetWc,attackValid,counterValid);
            //}
        }
    }
}
