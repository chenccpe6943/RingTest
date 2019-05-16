using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyboardinput : IUserInput
{
    public string keyUp = "";
    public string keyDown = "";
    public string keyLeft = "";
    public string keyRight = "";

    public string keyA;//run btn0
    public string keyB;//jump btn1
    public string keyLB;//attack btn2
    public string keyLT;//defense btn4
    public string keyRB;
    public string keyRT;
    public string keyJstick;

    public string keyJUp;
    public string keyJLeft;
    public string keyJDown;
    public string keyJRight;

    public Mybutton kkeyA = new Mybutton();
    public Mybutton kkeyB = new Mybutton();
    public Mybutton kkeyLB = new Mybutton();
    public Mybutton kkeyLT = new Mybutton();
    public Mybutton kkeyRB = new Mybutton();
    public Mybutton kkeyRT = new Mybutton();
    public Mybutton kkeyJstick = new Mybutton();
    

    //[Header("***** Mouse set *****")]
    public bool mouseEnable = false;
    public float mouseSensitivityX = 1.0f;
    public float mouseSensitivityY = 1.0f;

    //[Header("***** Ouput signals *****")]
    //public float Dup;
    //public float Dright;
    //public float Dmag;
    //public Vector3 Dvec;
    //public float Jup;//
    //public float Jright;//

    //public bool run;
    //public bool jump;
    //public bool lastJump;
    //public bool attack;
    //public bool lastAttack;

    //[Header("***** Others *****")]

    //public bool inputEnabled = true;

    //private float targetDup;
    //private float targetDright;
    //private float velocityDup;
    //private float velocityDright;

    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    void Update()
    {
        kkeyA.Tick(Input.GetKey(keyA));
        kkeyB.Tick(Input.GetKey(keyB));
        kkeyLB.Tick(Input.GetKey(keyLB));
        kkeyLT.Tick(Input.GetKey(keyLT));
        kkeyRB.Tick(Input.GetKey(keyRB));
        kkeyRT.Tick(Input.GetKey(keyRT));
        kkeyJstick.Tick(Input.GetKey(keyJstick));
        //print(kkeyJstick.OnPressed);
        //print(kkeyB.IsExtending && kkeyB.IsPressing);

        if (mouseEnable == true)
        {
            Jup = Input.GetAxis("Mouse Y") * 3.0f * mouseSensitivityY;
            Jright = Input.GetAxis("Mouse X") * 2.5f * mouseSensitivityX;

        }
        else
        {
            Jup = (Input.GetKey(keyJUp) ? 1.0f : 0) - (Input.GetKey(keyJDown) ? 1.0f : 0);
            Jright = (Input.GetKey(keyJRight) ? 1.0f : 0) - (Input.GetKey(keyJLeft) ? 1.0f : 0);

        }


        targetDup = (Input.GetKey(keyUp) ? 1.0f : 0) - (Input.GetKey(keyDown) ? 1.0f : 0);
        targetDright = (Input.GetKey(keyRight) ? 1.0f : 0) - (Input.GetKey(keyLeft) ? 1.0f : 0);

        if (inputEnabled == false)
        {
            targetDup = 0;
            targetDright = 0;
        }
        Dup = Mathf.SmoothDamp(Dup, targetDup, ref velocityDup, 0.1f);
        Dright = Mathf.SmoothDamp(Dright, targetDright, ref velocityDright, 0.1f);

        Vector2 tempOAxis = SquareToCircle(new Vector2(Dright, Dup));
        float Dright2 = tempOAxis.x;
        float Dup2 = tempOAxis.y;


        Dmag = Mathf.Sqrt(Dup2 * Dup2 + Dright2 * Dright2);
        Dvec = Dright2 * transform.right + Dup2 * transform.forward;

        run = (kkeyA.IsPressing && !kkeyA.IsDelaying) || kkeyA.IsExtending;
        defense = kkeyLB.IsPressing;
        jump = kkeyB.OnPressed;
        //attack = kkeyC.OnPressed;
        rb = kkeyRB.OnPressed;
        rt = kkeyRT.OnPressed;
        lb = kkeyLB.OnPressed;
        lt = kkeyLT.OnPressed;
        lockon = kkeyJstick.OnPressed;
    }
    //run = Input.GetKey(keyA);
    //defense = Input.GetKey(keyD);

    //bool newJump = Input.GetKey(keyB);

    //if (newJump != lastJump && newJump == true)
    //{
    //    jump = true;

    //}
    //else
    //{
    //    jump = false;
    //}
    //lastJump = newJump;


    //bool newAttack = Input.GetKey(keyC);

    //if (newAttack != lastAttack && newAttack == true)
    //{
    //    attack = true;

    //}
    //else
    //{
    //    attack = false;
    //}
    //lastAttack = newAttack;

    //private Vector2 SquareToCircle(Vector2 input)
    //{
    //    Vector2 output = Vector2.zero;

    //    output.x = input.x * Mathf.Sqrt(1 - (input.y * input.y) / 2.0f);
    //    output.y = input.y * Mathf.Sqrt(1 - (input.x * input.x) / 2.0f);

    //    return output;
    //}
}
