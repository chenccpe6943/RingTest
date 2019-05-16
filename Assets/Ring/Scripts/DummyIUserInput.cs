using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyIUserInput : IUserInput
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        while (true)
        { 
        //Dup = 1.0f;
       // Dright = 0;
            rb = true;
            yield return 0;

        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDmagDvec(Dup,Dright);
    }
}
