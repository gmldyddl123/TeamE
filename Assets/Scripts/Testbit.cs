using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testbit : MonoBehaviour
{
    
    
    void Start()
    {
        int data = 0b0000_1000;

        bool result = false;
        int checkPos = 1 << 3;
        int checkData = data & checkPos;

        if (checkData == checkPos)
        {
            result = true;
        }

        Debug.Log(result);
        //CheckBit(data, 4);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool CheckBit(int checkData, int checkPos)
    {
        bool result = false;

        checkPos = 1 << checkPos - 1;

        checkData = checkData & checkPos;

        if (checkData == checkPos)
        {
            result = true;
        }
        return result;
    }


}
