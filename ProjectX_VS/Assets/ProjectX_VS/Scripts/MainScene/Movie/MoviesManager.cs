using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MoviesManager : MonoBehaviour
{

	public List<MovieLogicManager> 		MovieLogicManagerList;
	public MovieLogicManager			CurrentMovieLogicManager;
	public RawImage						MovieDisplayUI;
	public int							CurrentMovieLogicManagerIndex	= 0;

	void Start ()
	{
		Init();
	}

	void Update ()
	{
		UpdateMovie();
	}

	private void Init()
	{
		CurrentMovieLogicManager = MovieLogicManagerList[CurrentMovieLogicManagerIndex];
	}

	private void UpdateMovie()
	{
		MovieDisplayUI.texture = CurrentMovieLogicManager.GetCurrentMovieMainTexture();
	}

	public void OnCam1()
	{
		CurrentMovieLogicManagerIndex = 0;;

		CurrentMovieLogicManager = MovieLogicManagerList[CurrentMovieLogicManagerIndex];
	}

	public void OnCam2()
	{
		CurrentMovieLogicManagerIndex = 1;

		CurrentMovieLogicManager = MovieLogicManagerList[CurrentMovieLogicManagerIndex];
	}

	public void OnCam3()
	{
		CurrentMovieLogicManagerIndex = 2;

		CurrentMovieLogicManager = MovieLogicManagerList[CurrentMovieLogicManagerIndex];
	}

	public void OnCam4()
	{
		CurrentMovieLogicManagerIndex = 3;

		CurrentMovieLogicManager = MovieLogicManagerList[CurrentMovieLogicManagerIndex];
	}
}
