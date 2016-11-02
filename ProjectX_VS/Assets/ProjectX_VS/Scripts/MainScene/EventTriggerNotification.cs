using UnityEngine;
using System.Collections;

public class EventTriggerNotification : EventTrigger 
{
	public GameObject InstalledApp;
	public LoginLoadingVFX LoadingVFX;

	void Start ()
	{
	
	}

	void Update ()
	{
		if( LoadingVFX.IsLoadingFinished )
		{
			InstalledApp.SetActive(true);
			this.gameObject.SetActive(false);
		}
	}

	public void NotificationFinishEvent()
	{
		LoadingVFX.gameObject.SetActive(true);
		UIMananger.instance.UpdateAppUIDepth(LoadingVFX.gameObject.transform);
	}
}
