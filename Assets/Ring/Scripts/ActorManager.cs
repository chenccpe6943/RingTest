using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : MonoBehaviour
{
    public actorControl ac;

    [Header("=== Auto Generate if Null ===")]
    public BattleeManager bm;
    public WeaponManager wm;
    public StateManager sm;
    // Start is called before the first frame update
    void Start()
    {
        ac = GetComponent<actorControl>();
        GameObject model = ac.model;
        GameObject sencer = transform.Find("sencer").gameObject;

        bm = Bind<BattleeManager>(sencer);
        wm = Bind<WeaponManager>(model);
        sm = Bind<StateManager>(gameObject);
        sm.test();
    }

    private T Bind<T>(GameObject go) where T : IActorManagerInterface
    {
        T tempInstance;
        tempInstance = go.GetComponent<T>();
        if(tempInstance == null)
        {
            tempInstance = go.AddComponent<T>();
        }
        tempInstance.am = this;
        return tempInstance;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetIsCounterBack(bool vaule)
    {
        sm.isCounterBackEnable = vaule;
    }

    public void TryDoDamage(WeaponController targetWc,bool attackValid,bool counterValid)
    {
        ////sm.HP -= 5;
        //if (sm.HP > 0)
        //{
        //sm.AddHP(-5);

        //}
        if (sm.isCounterBackSuccess)
        {
            if (counterValid)
            {
            targetWc.wm.am.Stunned();

            }
        }
        else if (sm.isCounterBackFailure)
        {
            if (attackValid)
            {

            HitOrDie(false);
            }
        }
        else if (sm.isImmortal)
        {

        }
        else if (sm.isDefense)
        {
            Blocked();
        }
        else
        {
            if (attackValid)
            {

                HitOrDie(true);
            }
        }
       
        

    }

    public void Stunned()
    {
        ac.IssueTrigger("stunned");
    }
    public void Blocked()
    {
        ac.IssueTrigger("blocked");
    }

    public void HitOrDie(bool doHitAnimation)
    {
        if (sm.HP <= 0)
        {
        }
        else
        {
            sm.AddHP(-5);
            if (sm.HP > 0)
            {
                if (doHitAnimation)
                {

                Hit();
                }
                //do some VFx blood
            }
            else
            {
                Die();
            }
        }
    }

    public void Hit()
    {
        ac.IssueTrigger("hit");
    }
    public void Die()
    {
        ac.IssueTrigger("die");
        ac.pi.inputEnabled = false;
        if(ac.camcon.lockState == true)
        {
            ac.camcon.LockUnlock();
        }
            ac.camcon.enabled = false;
    }
}
