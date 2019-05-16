using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    private float HP;
    private float MP;
    float recoverTime;
    float recoverDuration;
    public Slider HPslider;
    void Start()
    {
        HP = 100;
        MP = 0;
        HPslider.value = HP;
    }

    public void TakeDamege(float loss)
    {
        HP -= loss;
        HPslider.value = HP;
        if (HP <= 0)
        {
            // SendMessage("Die");
        }
    }
}