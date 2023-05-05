using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private Slider slider = null;
	public AudioClip song1,song2,song3,song4;
	public AudioSource audioSource = null;

	// Start is called before the first frame update
	private void Start()
	{
		if(PlayerPrefs.GetInt("volumeischange")==0)
		{
			PlayerPrefs.SetInt("volumeischange", 1);
			float volumevalue =0.5f;
			slider.value = volumevalue;
			AudioListener.volume = volumevalue;
		}
		if(PlayerPrefs.GetInt("song")==0)
		{
			SwitchSound(1);
		}
		LoadValues();
		
	}
	
	//sound
	public void VolumeSlider()
	{
		PlayerPrefs.SetFloat("volume", slider.value);
		LoadValues();
	}
	public void SwitchSound(int index)
	{
		switch (index)
		{
			case 1:
				PlayerPrefs.SetInt("song",index);
				audioSource.clip = song1;
				
				break;
			case 2:
				PlayerPrefs.SetInt("song", index);
				audioSource.clip = song2;
				break;
			case 3:
				PlayerPrefs.SetInt("song", index);
				audioSource.clip = song3;
				break;
			case 4:
				PlayerPrefs.SetInt("song", index);
				audioSource.clip = song4;
				break;
		}
		audioSource.Play();
	}
	public void LoadValues()
	{
		
		float volumevalue= PlayerPrefs.GetFloat("volume"); 
		slider.value = volumevalue;
		AudioListener.volume = volumevalue;
		SwitchSound(PlayerPrefs.GetInt("song"));
	}
}
