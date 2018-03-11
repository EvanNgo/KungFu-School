using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SantaObject : MonoBehaviour {
    public bool talks;
    public string messages;

    public void Talk() {
        Debug.Log(messages);
    }
}
