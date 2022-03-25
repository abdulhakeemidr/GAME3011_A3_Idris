using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Peripheral
{
    TOP,
    BOTTOM,
    LEFT,
    RIGHT,
    TOPLEFT,
    TOPRIGHT,
    BOTTOMLEFT,
    BOTTOMRIGHT
}

public class ImgProbability
{
    public int resourceValue;
    
    public int minProbability = 0;
    public int maxProbability = 100;

    public ImgProbability(int resource)
    {
        resourceValue = resource;
    }

}