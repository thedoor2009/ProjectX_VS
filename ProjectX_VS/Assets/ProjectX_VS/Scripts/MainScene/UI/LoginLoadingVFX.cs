using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoginLoadingVFX : MonoBehaviour
{
	public List<GameObject> loadingCubeList;
	public float LoadingTime = 2.0f;

	private float m_timer;
	private float m_loadingGapTime = 0.0f;
	private int m_index = 0;
	void Start ()
	{
		if( loadingCubeList.Count == 0 )
		{
			return;
		}

		m_loadingGapTime = LoadingTime / loadingCubeList.Count;
	}

	void Update ()
	{
		if( m_index >= loadingCubeList.Count )
		{
			return;
		}

		m_timer += Time.deltaTime;
		if( m_timer > m_loadingGapTime )
		{
			m_timer = 0.0f;
			loadingCubeList[m_index++].SetActive(true);
		}
	}
}
