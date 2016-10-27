using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MovieLogicManager : MonoBehaviour {

	public bool isReadyToCall;
	//public AudioDataManager AudioDataManagerObject;

	public AudioSource audio;
	public List<AudioClip> AudipClipList;
	public List<MovieController> MovieControllerList;

	protected int m_totalDuration;

	protected string m_currentMovieMode;
	protected string m_previousMovieMode;
	protected int m_currentDuration;
	protected int m_currentIndex;
	protected int m_nextIndex;
	protected bool m_initFlag = false;

	protected void Start ()
	{
		isReadyToCall = false;

		m_currentMovieMode = "";
		m_currentIndex = 0;
		if(MovieControllerList.Count > 0 )
		{
			MovieControllerList[m_currentIndex].PlayMovie();
		}
		AudipClipList = this.GetComponent<AudioFilesReader>().AudioClipList;
	}

	protected void Update ()
	{
		UpdateMovieLogic();
	}

	protected virtual void UpdateMovieLogic()
	{

	}

	protected void PlayNextMovie( int index )
	{
		m_nextIndex = index;

		MovieControllerList[index].PlayMovie();

		if( index + 1 < MovieControllerList.Count )
		{
			MovieControllerList[index+1].gameObject.SetActive(true);
		}
	}

	protected bool SetUpMovie( int index )
	{
		if(!MovieControllerList[index].IsReady() )
		{
			return false;
		}

		if(!m_initFlag)
		{
			m_totalDuration = MovieControllerList[index].MovieTexture.GetDuration();
			m_initFlag = true;
		}
		return true;
	}

	public Texture GetCurrentMovieMainTexture()
	{
		return MovieControllerList[m_currentIndex].ControllerMaterial.mainTexture;
	}
}
