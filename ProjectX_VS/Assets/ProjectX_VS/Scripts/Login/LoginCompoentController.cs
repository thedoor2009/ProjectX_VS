using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoginCompoentController : MonoBehaviour {

	public GameObject UserButton;
	public Texture2D		CursorOnEnterTexture;

	public RectTransform Frame;
	public RectTransform InputField;
	public InputField inputField;
	public Text PasswordWrongMessage;

	public float TweenTime = 0.5f;
	public float TweenTimer = 0.0f;
	public float WaitPWTime = 20.0f;
	public float WaitPWTimer = 0.0f;
	public string state = "none";
	public string loginPassword = "";

	public AudioSource audioSource;
	public AudioSource hintAudioSource;

	void Start ()
	{
		int day = System.DateTime.Now.Day;
		int month = System.DateTime.Now.Month;
		int year = System.DateTime.Now.Year;
		loginPassword = year.ToString() + month.ToString() + day.ToString();

		audioSource.clip = this.GetComponent<AudioFilesReader>().AudioClipList[0];
		hintAudioSource.clip = this.GetComponent<AudioFilesReader>().AudioClipList[1];

		Debug.Log(loginPassword);
	}

	void Update () 
	{
		switch( state )
		{
			case "begin":
			{
				TweenTimer += Time.deltaTime / TweenTime;

				if( TweenTimer > 1.0f )
				{
					InputField.gameObject.SetActive(true);
					state = "wait_pw";
				}
				break;
			}
			case "wait_pw":
			{
				WaitPWTimer += Time.deltaTime;
				if( Input.GetKey(KeyCode.Return))
				{
					OnEnter();
				}

				if( WaitPWTimer > WaitPWTime )
				{
					hintAudioSource.Play();
					WaitPWTimer = 0.0f;
				}
				break;
			}
			default:
			{
				break;
			}
		}
	}

	public void OnEnter()
	{
		StartCoroutine(CheckPassword());
	}

	IEnumerator CheckPassword()
	{
		PasswordWrongMessage.gameObject.SetActive(false);
		audioSource.Play();

		yield return new WaitForSeconds( 0.2f );

		if( inputField.text == loginPassword )
		{ 
			Application.LoadLevel("Scene_2");
		}
		else
		{
			PasswordWrongMessage.gameObject.SetActive(true);
		}
	}

	public void OnUserIconClicked()
	{
		state = "begin";
		Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
		UserButton.SetActive(false);

	}

	public void OnUserIconHoverd()
	{
		Cursor.SetCursor( CursorOnEnterTexture, new Vector2( 10, 8 ), CursorMode.Auto);
		StartCoroutine( UserIconFadeIn() );
	}

	public void OnUserIconOut()
	{
		Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
		StartCoroutine( UserIconFadeOut() );
	}

	IEnumerator UserIconFadeIn()
	{
		CanvasGroup canvas = Frame.gameObject.GetComponent<CanvasGroup>(); 
		while(canvas.alpha < 1)
		{                   
			canvas.alpha += Time.deltaTime * 2.0f;   
			yield return null;
		}
	}

	IEnumerator UserIconFadeOut()
	{
		CanvasGroup canvas = Frame.gameObject.GetComponent<CanvasGroup>(); 
		while(canvas.alpha > 0.6f ) 
		{                   
			canvas.alpha -= Time.deltaTime * 2.0f;   
			yield return null;
		}
	}
}
