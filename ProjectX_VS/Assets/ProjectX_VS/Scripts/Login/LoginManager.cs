using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LoginManager : MonoBehaviour {

	public float 			FirstPauseTime;
	public float			PhoneRingTime;
	public float			PhoneTalkTime;

	public RawImage			BlackScreen;
	public RawImage			LoginScreen;

	public InputField 		InputField;
	public string 			LoginPassword;
	public Text		  		PasswordWrongMessage;
	public AudioSource      AudioSource;
	public List<AudioClip>  AudioClipList;

	private string			m_loginState = "";
	private string 			m_loginPassword = "ebsn3dd9kr";

	void Start () 
	{
		m_loginPassword = LoginPassword;
		m_loginState =  "Black_Screen_No_Sound";
	}

	void Update () 
	{
		switch(m_loginState)
		{
		case "Black_Screen_No_Sound":
			StartCoroutine(FirstPause(FirstPauseTime));
			break;
		case "Black_Screen_Phone_Ring":
			StartCoroutine(FirstPause(PhoneRingTime));
			break;
		case "Black_Screen_Phone_Pick_Up":

			break;
		case "PC_Start_Up":
			
			break;
		case "PC_Login":
			if( Input.GetKey(KeyCode.Return))
			{
				OnReturn();
			}
			break;
		default:
			break;

		}


	}

	public void OnReturn()
	{
		PasswordWrongMessage.gameObject.SetActive(false);

		StartCoroutine(CheckPassword());
	}

	IEnumerator FirstPause( float time)
	{
		yield return new WaitForSeconds( time );

		AudioSource.loop = true;
		AudioSource.clip = AudioClipList[0];
		AudioSource.Play();
		m_loginState = "Black_Screen_Phone_Ring";
	}

	IEnumerator PhoneRing( float time)
	{
		yield return new WaitForSeconds( time );

		AudioSource.clip = AudioClipList[1];
		AudioSource.Play();
		m_loginState = "Black_Screen_Phone_Pick_Up";
	}

	IEnumerator CheckPassword()
	{
		yield return new WaitForSeconds( 0.2f );

		if( InputField.text == m_loginPassword )
		{ 
			Application.LoadLevel("Scene_OS");
		}
		else
		{
			PasswordWrongMessage.gameObject.SetActive(true);
		}
	}
}
