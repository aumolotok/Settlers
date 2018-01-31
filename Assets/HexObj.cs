using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexObj : MonoBehaviour {

    public List<TownPoint> neihboringTownsPoint = new List<TownPoint>();
    //public List<Village> neihboringVillages = new List<Village>();

    public ResourseTypes ResourseType;

    public void AddNeihboringPoint(TownPoint point)
    {
        neihboringTownsPoint.Add(point);
    } 
}
