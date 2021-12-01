using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleManager : MonoBehaviour
{
    public bool solved = false;
    public GameObject doorNext;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (SolvedCandlePuzzle() && !solved)
        {
            Debug.Log("solved it!!");
            solved = true;
            // FIXME change scene here
            doorNext.SetActive(true);
        }
    }

    bool SolvedCandlePuzzle()
    {
        for (var i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);

            if (child.name.Contains("e2")) // is correct candle, must be lit
            {
                if(!child.GetComponent<CandleLight>().lit)
                    return false;
            }
            else // is not correct candle, should be not lit
            {
                if(child.GetComponent<CandleLight>().lit)
                    return false;
            }    
        }

        return true;
    }
}
