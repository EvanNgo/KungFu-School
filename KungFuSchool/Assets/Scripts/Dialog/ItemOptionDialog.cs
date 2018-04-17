using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemOptionDialog : MonoBehaviour {
    public Text Title;
    public Text Point;
    public Text Unit;
	
    public void SetText(Option option,int point,Color color){
        Title.text = option.title;
        Point.text = point + "";
        Unit.text = option.unit;
        Title.color = color;
        Point.color = color;
        Unit.color = color;
    }
}
