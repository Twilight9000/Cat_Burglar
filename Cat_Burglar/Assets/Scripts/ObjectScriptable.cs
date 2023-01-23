using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Object", menuName = "ScriptableObjects/Object", order = 1)]
public class ObjectScriptable : ScriptableObject
{
    public GameObject model;
    public Vector3 size = new Vector3(1,1,1);
    public bool canBePlacedOnTopOf = false;
    public Vector3 offset;
}
