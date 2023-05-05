using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChecklistObjectTut:MonoBehaviour
{
	public int Index;
	public string Name;
	public string Type;
	private TMP_Text itemText;

	private void Start()
	{
		itemText = GetComponentInChildren<TMP_Text>();
		itemText.text = Name;
	}
	public void SetObjectInfo(string name,string type,int index)
	{
		this.Name= name;
		this.Type= type;
		this.Index = index;
	}
}