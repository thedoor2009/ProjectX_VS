using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoginManager : MonoBehaviour {

	public InputField 		InputField;
	public string 			LoginPassword;
	public Text		  		PasswordWrongMessage;

	private string 			m_loginPassword = "ebsn3dd9kr";

	void Start () 
	{
		m_loginPassword = LoginPassword;
	}

	void Update () 
	{
		if( Input.GetKey(KeyCode.Return))
		{
			OnReturn();
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
}
