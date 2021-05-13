using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character : MonoBehaviour {
	public GameObject gamemanager=null;

	// Use this for initialization
	void Start () {
		gamemanager=GameObject.Find("GameManager");
		if(gamemanager!=null)
		{
			Debug.Log("gamemanager mounted");
		}
		else
		{
			Debug.Log("Failed to mount gamemanager");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.LeftArrow)){ 
			gamemanager.GetComponent<generator>().MovePlayer(-1,0);
		}
		if(Input.GetKeyDown(KeyCode.RightArrow)){ 
			gamemanager.GetComponent<generator>().MovePlayer(1,0);
		}
		if(Input.GetKeyDown(KeyCode.UpArrow)){ 
			gamemanager.GetComponent<generator>().MovePlayer(0,1);
		}
		if(Input.GetKeyDown(KeyCode.DownArrow)){ 
			gamemanager.GetComponent<generator>().MovePlayer(0,-1);
		}	
	}
}
