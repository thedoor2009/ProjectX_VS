using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIMananger : MonoBehaviour
{
	public List<GameObject> UIControllerList;

	void Start ()
	{
	
	}
	

	void Update ()
	{
	
	}

	public void UpdateAppUIDepth(Transform topApp)
	{
		int maxIndex = 0;

		for( int index = 0; index < UIControllerList.Count; index++ )
		{
			int current_index = UIControllerList[index].transform.GetSiblingIndex();
			if( current_index > maxIndex )
			{
				maxIndex = current_index;
			}
		}

		topApp.SetSiblingIndex(maxIndex + 2);

		if(maxIndex > 1000 )
		{
			for( int index = 0; index < UIControllerList.Count; index++ )
			{
				int current_index = UIControllerList[index].transform.GetSiblingIndex();
				current_index -= 100;
				UIControllerList[index].transform.SetSiblingIndex(current_index);
			}
		}
	}
}
