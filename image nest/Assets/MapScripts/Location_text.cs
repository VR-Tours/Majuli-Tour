using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
public class Location_text : MonoBehaviour
{
    
	private float timeToAppear = 2f;
	private float timeWhenDisappear = 3;
	 
	//Call to enable the text, which also sets the timer
	public void EnableText()
	{
		gameObject.SetActive(true);
		timeWhenDisappear = Time.time + timeToAppear;
	}
	
	public void SetText(string text)
	{
		gameObject.GetComponent<TMP_Text>().text = text;
	}
	
	 
	//We check every frame if the timer has expired and the text should disappear
	void Update()
	{
		if (gameObject.activeSelf && (Time.time >= timeWhenDisappear))
		{
			gameObject.SetActive(false);
		}
	}
}
