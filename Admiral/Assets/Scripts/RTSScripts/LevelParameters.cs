
using System.Collections.Generic;
using UnityEngine;

public static class LevelParameters 
{
    public static List<Vector3> stationPoints = new List<Vector3> { 
        new Vector3(-172, 0, 0), 
        new Vector3(172, 0,0) , 
        new Vector3(0, 0, -100), 
        new Vector3(0, 0, 100),
        new Vector3(0, 0, 0)
    };

    //public static Dictionary<string, Vector3> level1Params = new Dictionary<string, Vector3>
    //{
    //    ["Station13"] = new Vector3 (0,0,0),
    //    ["Station23"] = new Vector3(0, 0, 0),
    //    ["Station33"] = new Vector3(0, 0, 0),
    //    ["Station43"] = new Vector3(0, 0, 0)
    //};
}
