using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPosition : MonoBehaviour {

    public int X { get; set; }
    public int Z { get; set; }
    public int Counter { get; set; }
    public GameObject ObjectInstance { get; set; }
    public bool IsEdge { get; set; }
    public int? EdgeStance { get; set; } //North 0, East 1, South 2, West 3
    public Vector3 ItsVector { get; set;}
    public bool HasBeenSelected { get; set; }

    //public _EdgeStance EdgeStance { get; set; }

    //public enum _EdgeStance { North, East, South, West }
}
