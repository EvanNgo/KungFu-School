using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogItem : MonoBehaviour {
    public GameObject dialogUI;
    private EquipmentManager eManager;
    private Item EquipingItem;
    private Item ShowingItem;
    [Header("Non Equip Item")]
    public GameObject NonEquipBox;
    public Image neIcon;
    public Text neName;
    public Text neType;
    public Text neDetail;
    [Header("Equip Item")]
    public GameObject EquipBox;
    public Text eName;
    public Text eType;
    public Text eDetail;
    public Text eDefaultTitle;
    public Text eDefaultPoint;
    public Text eDefaultUnit;
    public Text[] eOpTitles;
    public Text[] eOpPoints;
    public Text[] eOpUnits;
    [Header("Equiping Item")]
    public GameObject EquipingBox;
    public Text epName;
    public Text epType;
    public Text epDetail;
    public Text epDefaultTitle;
    public Text epDefaultPoint;
    public Text epDefaultUnit;
    public Text[] epOpTitles;
    public Text[] epOpPoints;
    public Text[] epOpUnits;

	// Use this for initialization
	void Start () {
        eManager = FindObjectOfType<EquipmentManager>();
        dialogUI.SetActive(false);
	}

    public void OnClick(){
        dialogUI.SetActive(false);
    }
    public void showItem(Item item){
        dialogUI.SetActive(true);
        EquipBox.SetActive(false);
        EquipingBox.SetActive(false);
        NonEquipBox.SetActive(false);
        if (!item.isEquipment)
        {
            neIcon.overrideSprite = item.icon;
            neName.text = item.name;
            neType.text = item.equipSlot + "";
            neDetail.text = item.details;
            NonEquipBox.SetActive(true);
        }
        else
        {
            eName.text = item.name;
            eType.text = item.equipSlot + "";
            eDetail.text = item.details;
            eDefaultTitle.text = item.defaultOption.title;
            eDefaultPoint.text = item.defaultPoint + "";
            eDefaultUnit.text = item.defaultOption.unit;
            for (int i = 0; i < eOpTitles.Length; i++)
            {
                if (i >= item.options.Length)
                {
                    eOpTitles[i].text = "";
                    eOpPoints[i].text = "";
                    eOpUnits[i].text = "";
                }
                else
                {
                    eOpTitles[i].text = item.options[i].title;
                    eOpPoints[i].text = item.points[i] + "";
                    eOpUnits[i].text = item.options[i].unit;
                }
            }
            EquipBox.SetActive(true);
            switch (item.equipSlot)
            {
                case Item.EquipmentSlot.Head:
                    if (eManager.equipings[0] != null)
                    {
                        EquipingItem = eManager.equipings[0];
                    }
                    break;
                case Item.EquipmentSlot.Armor:
                    if (eManager.equipings[1] != null)
                    {
                        EquipingItem = eManager.equipings[1];
                    }
                    break;
                case Item.EquipmentSlot.Gloves:
                    if (eManager.equipings[2] != null)
                    {
                        EquipingItem = eManager.equipings[2];
                    }
                    break;
                case Item.EquipmentSlot.Foot:
                    if (eManager.equipings[3] != null)
                    {
                        EquipingItem = eManager.equipings[3];
                    }
                    break;
                case Item.EquipmentSlot.Pant:
                    if (eManager.equipings[4] != null)
                    {
                        EquipingItem = eManager.equipings[4];
                    }
                    break;
                case Item.EquipmentSlot.Weapon:
                    if (eManager.equipings[5] != null)
                    {
                        EquipingItem = eManager.equipings[5];
                    }
                    break;
                case Item.EquipmentSlot.Shield:
                    if (eManager.equipings[6] != null)
                    {
                        EquipingItem = eManager.equipings[6];
                    }
                    break;
                case Item.EquipmentSlot.Pedan:
                    if (eManager.equipings[7] != null)
                    {
                        EquipingItem = eManager.equipings[7];
                    }
                    break;
                case Item.EquipmentSlot.Ring:
                    if (eManager.equipings[8] == null)
                    {
                        EquipingItem = eManager.equipings[8];
                    }
                    break;
                case Item.EquipmentSlot.Rare:
                    if (eManager.equipings[9] != null)
                    {
                        EquipingItem = eManager.equipings[9];
                    }
                    break;
            }

            if (EquipingItem == null)
            {   
                EquipingBox.SetActive(false);
                return;
            }
            epName.text = EquipingItem.name;
            epType.text =EquipingItem.equipSlot + "";
            epDetail.text = EquipingItem.details;
            epDefaultTitle.text = EquipingItem.defaultOption.title;
            epDefaultPoint.text = EquipingItem.defaultPoint + "";
            epDefaultUnit.text = EquipingItem.defaultOption.unit;
            for (int i = 0; i < eOpTitles.Length; i++)
            {
                if (i >= EquipingItem.options.Length)
                {
                    epOpTitles[i].text = "";
                    epOpPoints[i].text = "";
                    epOpUnits[i].text = "";
                }
                else
                {
                    epOpTitles[i].text = EquipingItem.options[i].title;
                    epOpPoints[i].text = EquipingItem.points[i] + "";
                    epOpUnits[i].text = EquipingItem.options[i].unit;
                }
            }
            EquipingItem = null;
            EquipingBox.SetActive(true);
        }
    }
}
