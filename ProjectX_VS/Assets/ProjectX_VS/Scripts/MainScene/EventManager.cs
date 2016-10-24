using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class EventManager : MonoBehaviour {

	#region Data Memeber
	public UIMananger UIManager;

	public EventTriggerNotebook 	NotebookTrigger;
	public EventTriggerSkype 		SkypeTrigger;
	public EventTriggerMovie 		MovieTrigger;

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

	}

	void OnEableListening()
	{
		EventManager.StartListening ("notebookFinishReading", EventTriggerNotebookFinishReading);
		EventManager.StartListening ("skypeFinishAnswering", EventTriggerSkypeFinishCalling);
	}

	void Update()
	{

	}
	#endregion

	#region Event Trigger Function

	void EventTriggerNotebookFinishReading()
	{
		SkypeTrigger.SetPhoneCall();
		UIManager.UpdateAppUIDepth(SkypeTrigger.gameObject.transform);
	}

	void EventTriggerSkypeFinishCalling()
	{
		//UIManager.MovieIcon.gameObject.SetActive(true);
	}
	#endregion
}
