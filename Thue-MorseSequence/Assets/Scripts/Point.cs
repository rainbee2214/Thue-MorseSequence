﻿using UnityEngine;
using System.Collections;

public class Point : MonoBehaviour
{
    public float delay = 0.1f;
    public bool batchDraw = true;
    public int batchSize = 100;
    public bool move;
    [Range(0,360)]
    public int startingAngle = 0;
   Vector2 direction = Vector2.right;

    bool moving;
    ulong count;

    float theta = 0;

    void Awake()
    {
        direction.Set(Mathf.Cos(startingAngle * Mathf.PI / 180f), Mathf.Sin(startingAngle * Mathf.PI / 180f));
        theta = startingAngle;
    }

    void Update()
    {
        if (move && !moving)
        {
            move = false;
            moving = true;
            StartCoroutine(StartMoving());
        }
    }
    IEnumerator StartMoving()
    {
        while (moving)
        {
            if (!batchDraw)
                batchSize = 1;
            for (int i = 0; i < batchSize; i++)
            {
                if (CountDigits(System.Convert.ToString((long)count, 2)))
                {
                    Rotate();
                }
                else
                {
                    StepForward();
                    //yield return null;
                }
                count++;
            }
            if (delay <= 0)
                yield return null;
            else
                yield return new WaitForSeconds(delay);
        }
        yield return null;
    }

    // A string is passed a string binary representation of an integer
    // If the number of "0"s in the integer is odd then we return true (because odd numbers are infinitely more interesting)
    // If true: Rotate
    // If false: StepForward
    bool CountDigits(string number)
    {
        int i = 0;
        foreach (char c in number)
            if (c == '0') i++;
        return i % 2 != 0;
    }
    void StepForward()
    {
        transform.position = (Vector2)transform.position + direction;
    }

    void Rotate()
    {
        theta += Mathf.PI / 3f;
        direction = new Vector2(Mathf.Cos(theta), Mathf.Sin(theta));
    }

}