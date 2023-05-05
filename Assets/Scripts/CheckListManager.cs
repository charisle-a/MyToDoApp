using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Mime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class CheckListManager : MonoBehaviour
{
	public Transform content;
	public GameObject addPanel;
	public GameObject settingsPanel;
	public Button createButton;
	public GameObject checkListItemPrefab;
	public GameObject todoListPanel;
	public AudioSource soundPlayer;
	string filePath;
	private List<ChecklistObjectTut> checklistObjects = new List<ChecklistObjectTut>();
	public TMP_InputField[] addInputFields;
	public class ChecklistItem
	{

		public string objName;
		public string type;
		public int index;
		public ChecklistItem(string name, string type, int index)
		{
			this.objName = name;
			this.type = type;
			this.index = index;
		}
	}

	private void Start()
	{
		filePath = Application.persistentDataPath + "/checklist.txt";
		LoadJSONData();
		addInputFields = addPanel.GetComponentsInChildren<TMP_InputField>();
		createButton.onClick.AddListener(delegate { CreateChecklistItem(addInputFields[0].text); });

	}
	
	public void playthisSound()
	{
		soundPlayer.Play();
	}
	public void SwitchMode(int mode)
	{
		switch(mode)
			{
			    //Regular mode
			case 0:
				addPanel.SetActive(false);
				todoListPanel.SetActive(true);
				settingsPanel.SetActive(false);
				
				break;
				//Adding a new checklist item
			case 1:
				addPanel.SetActive(true);
				
				
				break;
				//settings panel
			case 3:
				addPanel.SetActive(false);
				todoListPanel.SetActive(false);
				settingsPanel.SetActive(true);
				break;
			
				

		}
	}
	public void CreateChecklistItem(string name,string type="",int loadIndex=0,bool loading=false)
	{
		GameObject item = Instantiate(checkListItemPrefab);
		item.transform.SetParent(content);
		ChecklistObjectTut itemObject = item.GetComponent<ChecklistObjectTut>();
		int index = loadIndex;
		if(!loading)
		 index = checklistObjects.Count - 1;
		
		itemObject.SetObjectInfo(name,type, index);
		checklistObjects.Add(itemObject);
		ChecklistObjectTut temp = itemObject;
		itemObject.GetComponent<Toggle>().onValueChanged.AddListener(delegate { CheckItem(temp); });
		if(!loading)
		{
			SaveJSONData();
			addInputFields[0].text = "";
			SwitchMode(0);
		}
		
	}

	void CheckItem(ChecklistObjectTut item)
	{
		checklistObjects.Remove(item);
		SaveJSONData();
		playthisSound();
		Destroy(item.gameObject);
	}
	

	void SaveJSONData()
	{
		string contents="";

		for(int i=0;i<checklistObjects.Count;i++)
		{
			ChecklistItem temp = new ChecklistItem(checklistObjects[i].Name, checklistObjects[i].Type, checklistObjects[i].Index);

			contents += JsonUtility.ToJson(temp) + "\n" ;
		}
		File.WriteAllText(filePath, contents);

		
	}
	void LoadJSONData()
	{
		if(File.Exists(filePath))
		{
			string contents=File.ReadAllText(filePath);
			string[] splitContents = contents.Split("\n");
			foreach(string content in splitContents)
			{
				if(content.Trim()!="")
				{
					ChecklistItem temp = JsonUtility.FromJson<ChecklistItem>(content.Trim());
					CreateChecklistItem(temp.objName, temp.type, temp.index, true);
				}
				
			}
			

		}
	}
}
