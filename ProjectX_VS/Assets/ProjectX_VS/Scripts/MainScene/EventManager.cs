﻿using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class EventManager : MonoBehaviour {

	#region Data Memeber

	public EventTriggerNotebook 		NotebookTrigger;
	public EventTriggerSkype 			SkypeTrigger;
	public EventTriggerNotification 	NotificationTrigger;
	public EventTriggerMovie 			MovieTrigger;

	public AudioSource					AudioSource;
	public List<AudioClip>				AudioClipList;

	private Dictionary <string, UnityEvent> eventDictionary;

	private static EventManager eventManager;

	public static EventManager instance
	{
		get
		{
			if (!eventManager)
			{
				eventManager = FindObjectOfType (typeof (EventManager)) as EventManager;

				if (!eventManager)
				{
					Debug.LogError ("There needs to be one active EventManger script on a GameObject in your scene.");
				}
				else
				{
					eventManager.Init (); 
				}
			}

			return eventManager;
		}
	}
	#endregion

	#region Trigger Functions
	void Init ()
	{
		if (eventDictionary == null)
		{
			eventDictionary = new Dictionary<string, UnityEvent>();
		}
		OnEableListening();
	}

	public static void StartListening (string eventName, UnityAction listener)
	{
		UnityEvent thisEvent = null;
		if (instance.eventDictionary.TryGetValue (eventName, out thisEvent))
		{
			thisEvent.AddListener (listener);
		} 
		else
		{
			thisEvent = new UnityEvent ();
			thisEvent.AddListener (listener);
			instance.eventDictionary.Add (eventName, thisEvent);
		}
	}

	public static void StopListening (string eventName, UnityAction listener)
	{
		if (eventManager == null) return;
		UnityEvent thisEvent = null;
		if (instance.eventDictionary.TryGetValue (eventName, out thisEvent))
		{
			thisEvent.RemoveListener (listener);
		}
	}

	public static void TriggerEvent (string eventName)
	{
		UnityEvent thisEvent = null;
		if (instance.eventDictionary.TryGetValue (eventName, out thisEvent))
		{
			thisEvent.Invoke ();
		}
	}
	#endregion

	#region Hook
	void Start()
	{
		if( AudioClipList.Count == 0 )
		{
			AudioClipList = this.GetComponent<AudioFilesReader>().AudioClipList;
		}
		AudioSource.PlayOneShot( AudioClipList[0]);
	}

	void OnEableListening()
	{
		EventManager.StartListening ("notebookFinishReading", EventTriggerNotebookFinishReading);
		EventManager.StartListening ("skypeFinishAnswering", EventTriggerSkypeFinishCalling);
		EventManager.StartListening ("notificationFinishClick", EventTriggerNotificationClick);
	}

	void Update()
	{

	}
	#endregion

	#region Event Trigger Function

	void EventTriggerNotebookFinishReading()
	{
		SkypeTrigger.SetPhoneCall();
		UIMananger.instance.UpdateAppUIDepth(SkypeTrigger.gameObject.transform);
	}

	void EventTriggerSkypeFinishCalling()
	{
		StartCoroutine(SetUpNofitication());
	}

	IEnumerator SetUpNofitication()
	{
		yield return new WaitForSeconds( 2.0f );
		NotificationTrigger.gameObject.SetActive(true);
		AudioSource.PlayOneShot( AudioClipList[1]);
	}

	void EventTriggerNotificationClick()
	{
		NotificationTrigger.NotificationFinishEvent();
		UIMananger.instance.UpdateAppUIDepth(NotificationTrigger.gameObject.transform);
	}
	#endregion
}
