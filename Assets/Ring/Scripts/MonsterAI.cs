using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterAI : MonoBehaviour
{
    public int hp;
    [Range(0,100)]
    public int hpMax = 100;

    public ParticleSystem bloodEFX;
    public Text hpText;
    public Slider hpSlider;
    
    private Animator anim;
    private Collider col;
    private Collider atkSphereEnemy;

    // Start is called before the first frame update
    void Awake ()
    {
        hp = hpMax;
        anim=GetComponent<Animator>();
        col=GetComponent<CapsuleCollider>();
        atkSphereEnemy = GetComponentInChildren<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        hp = Mathf.Clamp(hp, 0, 100);
        hpText.text = hp + "/" + hpMax;
        hpSlider.value = (float)hp / (float)hpMax;

        if (hp <= 0)
        {
            DieStart ();
        }
        
    }
    public void playBloodEFX()
    {
        bloodEFX.Play();
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "weapon")
        {
            playBloodEFX();
            Vector3 lookDir = Camera.main.transform.position - transform.position;
            lookDir.y = 0;
            bloodEFX.transform.rotation = Quaternion.LookRotation(lookDir);

            playBloodEFX();
            hp = hp - 40;
            anim.SetTrigger("hit"); 
        }
       
    }
    void DieStart()
    {
        //Destroy(gameObject);
        anim.SetTrigger("die");
        col.enabled = false;
    }
    public void DieTween()
    {
        iTweenEvent.GetEvent(gameObject, "DieTween").Play(); 
    }
    public void DieEnd()
    {
        Destroy(gameObject);
    }
}
 