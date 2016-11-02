using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LoginLoadingVFX : MonoBehaviour
{
	public List<GameObject> loadingCubeList;
	public Text 	LoadingDescText;
	public float 	LoadingTime = 2.0f;
	public bool  	IsLoadingFinished = false;

	private float m_timer;
	private float m_loadingGapTime = 0.0f;
	private int m_index = 0;
	void Start ()
	{
		if( loadingCubeList.Count == 0 )
		{
			return;
		}
		IsLoadingFinished = false;
		m_loadingGapTime = LoadingTime / loadingCubeList.Count;
	}

	void Update ()
	{
		if( m_index >= loadingCubeList.Count )
		{
			IsLoadingFinished = true;
			Close();

			return;
		}

		m_timer += Time.deltaTime;
		if( m_timer > m_loadingGapTime )
		{
			m_timer = 0.0f;
			loadingCubeList[m_index++].SetActive(true);
		}
	}

	void Close()
	{
		LoadingDescText.gameObject.SetActive(false);
		this.GetComponent<RawImage>().enabled = false;
		foreach( GameObject loadingCube in loadingCubeList )
		{
			loadingCube.SetActive(false);
		}
	}
}
