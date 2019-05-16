using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHealth : MonoBehaviour
{
    float HP;
    float MP;
    bool PlayerInRange;
    [SerializeField] float hurtPower; //受害強度
    [SerializeField] float damagePower; //傷害威力
    float recoverTime; //恢復時間
    public Slider HPslider;
    public Slider MPslider;
    [SerializeField] float recoverDuration = 1f;  //復原所需期間

    void Start()
    {
        HP = 100;
        MP = 0;
        recoverTime = 0;
        PlayerInRange = false;
        HPslider.value = HP;
        MPslider.value = MP;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //怪物的能量隨時間增加
        recoverTime += Time.deltaTime;
        MP = Mathf.Clamp(recoverTime / recoverDuration * 100, 0, 100);
        MPslider.value = MP;
    }
    //損血
    public void TakeDamege()
    {
        HP -= hurtPower;
        HPslider.value = HP;
        if (HP <= 0)
        {
            SendMessage("Die");
        }
    }
    //檢查是否具有能量攻擊，有能力攻擊傳回true，並扣減能量
    public bool Attack()
    {
        if (MP >= 100)
        {
            MP = 0;
            recoverTime = 0;
            return true;
        }
        else
            return false;
    }
    //透過Trigger 判定是否傷害到玩家
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !PlayerInRange)
        {
            PlayerInRange = true;
            if (Attack())
                other.GetComponent<PlayerHealth>().TakeDamege(damagePower);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && PlayerInRange && Attack())
        {
            other.GetComponent<PlayerHealth>().TakeDamege(damagePower);
        }
    }
    //玩家逃離怪物Trigger，將PlayerInRange關閉
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerInRange = false;
        }
    }
}
