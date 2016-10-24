using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UISystemTime : MonoBehaviour {

	public Text timeText;

	void Start () 
	{

	}

	void Update () 
	{
		System.DateTime localDate = System.DateTime.Now;
		timeText.text = localDate.ToString();
	}
}
