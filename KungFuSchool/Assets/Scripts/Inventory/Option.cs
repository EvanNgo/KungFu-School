using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Option", menuName = "Inventory/Option")]
public class Option : ScriptableObject {
    public string title;
    public string tag;
    public int maxPoint;
    public int point;
    public string unit;
}
