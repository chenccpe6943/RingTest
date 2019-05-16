using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    public ParticleSystem bloodEFX;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            playBloodEFX();
    }
    public void playBloodEFX()
    {
        bloodEFX.Play();
    }
    private void OnTriggerEnter(Collider col)
    {

        playBloodEFX();
        Vector3 lookDir = Camera.main.transform.position - transform.position;
        lookDir.y = 0;
        bloodEFX.transform.rotation = Quaternion.LookRotation(lookDir);

    }
}
