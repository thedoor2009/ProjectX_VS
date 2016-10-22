using UnityEngine;
using System.Collections;

public class NoteController : MonoBehaviour {

	private bool 		onFirstClick 			= false;
	private bool 		onSecondClick 			= false;
	private float 		onFirstClickTime 		= 0.0f;
	private float 		onClickGapTime 			= 0.3f;

	void Start ()
	{
	
	}

	void Update ()
	{
	
	}

	public void OnDoubleClicked()
	{
		if( !onFirstClick )
		{
			onFirstClick = true;
			onFirstClickTime = Time.time;
		}
		else
		{
			float currentClickTime = Time.time;
			if( currentClickTime - onFirstClickTime < onClickGapTime )
			{
				Debug.Log("Double Click");
			}
			else
			{
				Debug.Log("One Click");
			}
			onFirstClick = false;
			onFirstClickTime = Time.time;
		}
	}
}
