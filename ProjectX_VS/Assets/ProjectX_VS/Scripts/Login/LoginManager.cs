using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LoginManager : MonoBehaviour {

	public float 			FirstPauseTime;
	public float			PhoneRingTime;
	public float			PhoneTalkTime;
	public float			PowerOnTime;
	public float			PowerOnFadeInTime;
	public float			PowerOnFadeOutTime;

	public RawImage			BlackScreen;
	public RawImage			LoginScreen;

	public InputField 		InputField;
	public string 			LoginPassword;
	public Text		  		PasswordWrongMessage;
	public AudioSource      AudioSource;
	public List<AudioClip>  AudioClipList;

	private string			m_loginState = "";
	private string 			m_loginPassword = "ebsn3dd9kr";
	private float			m_timer;

	void Start () 
	{
		m_loginPassword = LoginPassword;
		m_loginState =  "Black_Screen_No_Sound";
		m_timer = 0.0f;

		Cursor.visible = false;
	}

	void Update () 
	{
		m_timer += Time.deltaTime;
		switch(m_loginState)
		{
		case "Black_Screen_No_Sound":
			if( m_timer > FirstPauseTime )
			{
				m_timer = 0.0f;
				AudioSourceUpdate( true, 0 );
				m_loginState = "Black_Screen_Phone_Ring";
			}
			break;
		case "Black_Screen_Phone_Ring":
			if( m_timer > PhoneRingTime )
			{
				m_timer = 0.0f;
				AudioSourceUpdate( false, 1 );
				m_loginState = "Black_Screen_Phone_Pick_Up";
			}
			break;
		case "Black_Screen_Phone_Pick_Up":
			if( !AudioSource.isPlaying )
			{
				m_timer = 0.0f;
				AudioSourceUpdate( false, 2 );
				m_loginState = "PC_Start_Up";
			}
			break;
		case "PC_Start_Up":
			if( m_timer > PowerOnTime )
			{
				m_timer = 0.0f;
				m_loginState = "PC_Login";
			}

			if( m_timer < PowerOnFadeInTime )
			{
				AudioSource.volume = m_timer / PowerOnFadeInTime;
			}
			else if( m_timer > PowerOnTime - PowerOnFadeOutTime && m_timer < PowerOnTime )
			{
				AudioSource.volume = ( PowerOnTime - m_timer ) / PowerOnFadeOutTime;
			}
			break;
		case "PC_Login":
			BlackScreen.CrossFadeAlpha( 0.0f, 0.3f, false);
			Cursor.visible = true;

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

	private void AudioSourceUpdate( bool loop, int index )
	{
		AudioSource.Stop();
		AudioSource.loop = loop;
		AudioSource.clip = AudioClipList[index];
		AudioSource.Play();
	}
}
