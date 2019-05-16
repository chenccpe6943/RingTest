using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum State { Idle, Locomotion, Jump, Die, Reset, Attack, Patrol }

public class actorControl : MonoBehaviour
{
    [Header("Player UI Compinents")]
    public Image HpBar;

    public GameObject model;
    public cameracontrol camcon;
    public Camera cam;
    public IUserInput pi;
    public float walkspeed = 2.4f;
    public float runMultiplier = 2.0f;
    public float jumpVelocity = 5.0f;
    public float rollVelocity = 1.0f;

    [Space(10)]
    [Header("***** friction Settings *****")]
    public PhysicMaterial frictionOne;
    public PhysicMaterial frictionZero;

    private Animator anim;
    private Rigidbody rigid;
    private Vector3 planerVec;
    private Vector3 thrustVec;
    private bool canAttack;
    private bool lockPlaner = false;
    private bool trackDirection = false;
    private CapsuleCollider col;
    //private float lerpTarget;
    private Vector3 deltaPos;

    public bool leftIsShield = true;
    // Start is called before the first frame update
    void Awake()
    {
        cam = Camera.main;
        IUserInput[] inputs = GetComponents<IUserInput>();
        foreach (var input in inputs)
        {
            if (input.enabled == true)
            {
                pi = input;
                break;
            }
        }
        anim = model.GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pi.lockon)
        {
            camcon.LockUnlock();
        }
        if (camcon.lockState == false)
        {
            anim.SetFloat("forword", pi.Dmag * Mathf.Lerp(anim.GetFloat("forword"), ((pi.run) ? 2.0f : 1.0f), 0.5f));
            anim.SetFloat("right", 0);
        }
        else
        {
            Vector3 localDVec = transform.InverseTransformVector(pi.Dvec);
            anim.SetFloat("forword", localDVec.z * ((pi.run) ? 2.0f : 1.0f));
            anim.SetFloat("right", localDVec.x * ((pi.run) ? 2.0f : 1.0f));
        }

        //anim.SetBool("defense", pi.defense);

        if (pi.jump || rigid.velocity.magnitude > 7f)  //rol or jab
        {
            anim.SetTrigger("roll");
            canAttack = false;
        }
        if (pi.jump)
        {
            anim.SetTrigger("jump");
            canAttack = false;
        }
        if ((pi.rb || pi.lb) && (CheckState("ground") || CheckStateTag("attackR") || CheckStateTag("attackL")) && canAttack)
        {
            if (pi.rb)
            {
                anim.SetBool("R0L1", false);
                anim.SetTrigger("attack");
            }
            else if (pi.lb && !leftIsShield)
            {
                anim.SetBool("R0L1", true);
                anim.SetTrigger("attack");
            }

        }

        if (leftIsShield)
        {
            if (CheckState("ground")||CheckState("blocked"))
            {
                anim.SetBool("defense", pi.defense);
                anim.SetLayerWeight(anim.GetLayerIndex("defense"), 1);
            }
            else
            {
                anim.SetBool("defense", false);
                //anim.SetLayerWeight(anim.GetLayerIndex("defense"), 0);
            }
        }
        else
        {
            anim.SetLayerWeight(anim.GetLayerIndex("defense"), 0);
        }


        if (camcon.lockState == false)
        {
            if (pi.Dmag > 0.1f)
            {
                model.transform.forward = Vector3.Slerp(model.transform.forward, pi.Dvec, 0.3f);
            }
            if (lockPlaner == false)
            {
                planerVec = pi.Dmag * model.transform.forward * walkspeed * ((pi.run) ? runMultiplier : 1.0f);
            }

        }
        else
        {
            if (trackDirection == false)
            {
                model.transform.forward = transform.forward;

            }
            else
            {
                model.transform.forward = planerVec.normalized;
            }

            if (lockPlaner == false)
            {
                planerVec = pi.Dvec * walkspeed * ((pi.run) ? runMultiplier : 1.0f);
            }
        }
        // print(CheckState("idle","attack"));
    }
    private void FixedUpdate()
    {
        // rigid.position += planerVec * Time.fixedDeltaTime;
        rigid.position += deltaPos;
        rigid.velocity = new Vector3(planerVec.x, rigid.velocity.y, planerVec.z) + thrustVec;
        thrustVec = Vector3.zero;
        deltaPos = Vector3.zero;
    }
    public bool CheckState(string stateName, string layerName = "Base Layer")
    {
        return anim.GetCurrentAnimatorStateInfo(anim.GetLayerIndex(layerName)).IsName(stateName);
    }
    public bool CheckStateTag(string tagName, string layerName = "Base Layer")
    {
        return anim.GetCurrentAnimatorStateInfo(anim.GetLayerIndex(layerName)).IsTag(tagName);
    }





    public void onjumpEnter()
    {
        pi.inputEnabled = false;
        thrustVec = new Vector3(0, jumpVelocity, 0);
        lockPlaner = true;
        trackDirection = true;
    }
    public void IsGround()
    {
        anim.SetBool("isGround", true);
    }
    public void IsNotGround()
    {
        anim.SetBool("isGround", false);
    }
    public void ongroundenter()
    {
        pi.inputEnabled = true;
        lockPlaner = false;
        canAttack = true;
        col.material = frictionOne;
        trackDirection = false;
    }
    public void OnGroundExit()
    {
        col.material = frictionZero;
    }
    public void OnFallEnter()
    {
        pi.inputEnabled = false;
        lockPlaner = true;
    }
    public void OnrollEnter()
    {
        pi.inputEnabled = false;
        thrustVec = new Vector3(0, rollVelocity, 0);
        lockPlaner = true;
        trackDirection = true;
    }
    public void OnJabEnter()
    {
        pi.inputEnabled = false;
        lockPlaner = true;
    }
    public void OnJabUpdate()
    {
        thrustVec = model.transform.forward * anim.GetFloat("jabVelocity");

    }
    public void Onattack1hAEnter()
    {
        pi.inputEnabled = false;
        //lockPlaner = true;
        //lerpTarget = 1.0f;
    }
    public void OnAttack1hAUpdate()
    {
        thrustVec = model.transform.forward * anim.GetFloat("attack1hAVelocity");
        //anim.SetLayerWeight(anim.GetLayerIndex("attack"), Mathf.Lerp(anim.GetLayerWeight(anim.GetLayerIndex("attack")), lerpTarget, 0.4f));
    }
    public void OnAttackExit()
    {
        model.SendMessage("WeaponDisable");
    }




    public void OnHitEnter()
    {
        pi.inputEnabled = false;
        planerVec = Vector3.zero;

    }

    public void OnDieEnter()
    {
        pi.inputEnabled = false;
        planerVec = Vector3.zero;
    }

    public void OnBlockedEnter()
    {
        pi.inputEnabled = false;
    }

    public void OnUpdateRM(object _deltaPos)
    {
        if (CheckState("attack1hC"))
        {
            deltaPos += (0.8f * deltaPos + 0.2f * (Vector3)_deltaPos) / 1.0f;

        }
    }
    public void IssueTrigger(string triggerName)
    {
        anim.SetTrigger(triggerName);
    }
}
