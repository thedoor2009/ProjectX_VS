﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class AudioDataManager : MonoBehaviour {

	public AudioSource audioSource;
	public GameObject LoadingSreenVFX;
	public Text	AnsweringDisplayTime;

	public Dictionary<string, string> AudioDataDictionary = new Dictionary<string, string>()
	{
		{"OneCall_C00", 						"I’m in trouble. I need your help."},
		{"OneCall_C01", 						"Aloha! How’s everything going?"},
		{"OneCall_C02", 						"What’s your opinion about election this year?"},
		{"OneCall_C03", 						"Do you want another road trip late this month?"},
		{"OneCall_C04", 						"Did you watch the new HBO horror show?"},
		{"OneCall_C05", 						"I need the password of our email account."},
		{"OneCall_C06", 						"For Sure."},
		{"OneCall_C07", 						"Yes."},
		{"OneCall_C08", 						"Thanks!"},
		{"OneCall_C09", 						"No."}
	};
	public List<AudioObjectController> AudioObjectsList;
	public List<string> AudioNamesList;
	public int AvailableAudioCount = 0;
	public bool  audioStartPlayingFlag = false;

	private float answeringTimer = 0.0f;

	void Start () 
	{
		RefreshAvailableAudio(true);
	}

	void Update () 
	{
		UpdateDisplayTime();
		if( audioStartPlayingFlag )
		{
			if(!audioSource.isPlaying)
			{
				audioStartPlayingFlag = false;
			}
		}
	}

	public void PlayAudio( GameObject audioObject )
	{
		string audioName = audioObject.GetComponent<AudioObjectController>().AudioName;
		AudioClip clip = AudioDataSource.instance.FindAudio( audioName );
		audioSource.clip = clip;
		audioSource.Play();
		audioStartPlayingFlag = true;

		for( int i = 0; i < AvailableAudioCount; i++ )
		{
			AudioObjectsList[i].SetButtonState(false);
		}
	}

	public void EnableLoadingVFX()
	{
		LoadingSreenVFX.SetActive(true);
	}

	public void DisableLoadingVFX()
	{
		LoadingSreenVFX.GetComponent<LoadingVFX>().Reset();
		LoadingSreenVFX.SetActive(false);
	}

	public void InsertAvailableAudio( List<string> audioNames )
	{
		AvailableAudioCount = audioNames.Count;
		foreach( string audioName in audioNames )
		{
			int oldIndex = AudioNamesList.IndexOf(audioName);
			AudioNamesList.RemoveAt(oldIndex);
			AudioNamesList.Insert( 0, audioName );
		}
	}

	public void RefreshAvailableAudio(bool init)
	{
		for( int i = 0; i < AudioNamesList.Count; i++ )
		{
			AudioObjectController item = AudioObjectsList[i];

			string audioName = AudioNamesList[i];
			string audioContent = AudioDataDictionary[audioName];

			item.SetAudioName(audioName);
			item.SetContent(audioContent);

			AudioObjectsList[i].SetButtonState(false);
		}

		if(!init)
		{
			for( int i = 0; i < AvailableAudioCount; i++ )
			{
				AudioObjectsList[i].SetButtonState(true);
			}
		}
	}

	private void UpdateDisplayTime()
	{
		answeringTimer += Time.deltaTime;
		string showAnsweringTime = "";
		if( answeringTimer < 60.0f )
		{
			if( answeringTimer < 10.0f )
			{
				showAnsweringTime = "00 : " + "0" + ((int)(answeringTimer)).ToString();
			}
			else
			{
				showAnsweringTime = "00 : " + ((int)(answeringTimer)).ToString();
			}
		}
		else 
		{
			float min = answeringTimer / 60.0f;
			if( min < 10 )
			{
				showAnsweringTime = "0" + ((int)(min)).ToString() + " : ";
			}
			else
			{
				showAnsweringTime = ((int)(min)).ToString() + " : ";
			}

			float seconds = answeringTimer % 60.0f;
			if( seconds < 10.0f )
			{
				showAnsweringTime = showAnsweringTime + "0" + ((int)(seconds)).ToString();
			}
			else
			{
				showAnsweringTime = showAnsweringTime + ((int)(seconds)).ToString();
			}
		}
		AnsweringDisplayTime.text = showAnsweringTime;
	}
}
