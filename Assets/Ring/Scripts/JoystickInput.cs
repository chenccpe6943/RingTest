using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickInput : IUserInput
{ // Start is called before the first frame update

    public string axisX = "axisX";
    public string axisY = "axisY";
    public string axisJright = "axis3";
    public string axisJup = "axis5";
    public string btnA = "btn0";//run
    public string btnB = "btn1";//jump
    public string btnC = "btn2";//attack
    public string btnD = "btn3";
    public string btnLB = "btn4";//defense
    public string btnLT = "btn6";
    public string btnRB = "btn5";
    public string btnRT = "btn7";
    public string btnJstick = "btn11";

    public Mybutton buttonA = new Mybutton();
    public Mybutton buttonB = new Mybutton();
    public Mybutton buttonC = new Mybutton();
    public Mybutton buttonD = new Mybutton();
    public Mybutton buttonLB = new Mybutton();
    public Mybutton buttonLT = new Mybutton();
    public Mybutton buttonRB = new Mybutton();
    public Mybutton buttonRT = new Mybutton();
    public Mybutton buttonJstick = new Mybutton();

    //[Header("***** Ouput signals *****")]
    //public float Dup;
    //public float Dright;
    //public float Dmag;
    //public Vector3 Dvec;
    //public float Jup;//
    //public float Jright;//

    //public bool run;
    //public bool jump;
    //protected bool lastJump;
    //public bool attack;
    //protected bool lastAttack;

    //[Header("***** Others *****")]

    //public bool inputEnabled = true;

    //protected float targetDup;
    //protected float targetDright;
    //protected float velocityDup;
    //protected float velocityDright;

    //void start()
    //{

    //}

    // Update is called once per frame

    void Update()
    {
        buttonA.Tick(Input.GetButton(btnA));
        buttonB.Tick(Input.GetButton(btnB));
        buttonC.Tick(Input.GetButton(btnC));
        buttonD.Tick(Input.GetButton(btnD));
        buttonLB.Tick(Input.GetButton(btnLB));
        buttonLT.Tick(Input.GetButton(btnLT));
        buttonRB.Tick(Input.GetButton(btnRB));
        buttonRT.Tick(Input.GetButton(btnRT));
        buttonJstick.Tick(Input.GetButton(btnJstick));

        Jup = -1 * Input.GetAxis(axisJup);
        Jright = Input.GetAxis(axisJright);

        targetDup = Input.GetAxis(axisY);
        targetDright = Input.GetAxis(axisX);

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

        run = (buttonA.IsPressing && !buttonA.IsDelaying) || buttonA.IsExtending;
        jump = buttonA.OnPressed && buttonA.IsExtending;


        defense = buttonLB.IsPressing;
        //attack = buttonC.OnPressed;
        rb = buttonRB.OnPressed;
        rt = buttonRT.OnPressed;
        lb = buttonLB.OnPressed;
        lt = buttonLB.OnPressed;
        lockon = buttonJstick.OnPressed;
    }
    //defense = Input.GetButton(btnLB);

    //run = Input.GetButton(btnA);

    //bool newjump = input.getbutton(btnb);
    //if (newjump != lastjump && newjump == true)
    //{
    //    jump = true;
    //}
    //else
    //{
    //    jump = false;
    //}
    //lastjump = newjump;

    //bool newAttack = Input.GetButton(btnC);

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
