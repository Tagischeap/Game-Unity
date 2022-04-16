using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SideMenu : MonoBehaviour {

	public GameObject backdrop;
	public Button menuButton;
	public Canvas store;
	public Canvas playsetEditor;
	//public GameObject gameManager;
	public MonoBehaviour game;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	bool menuOpen = false;
	public void onMenuButton(){
		//Debug.Log ("Menu Button Clicked");
		menuOpen = !menuOpen;
		backdrop.SetActive (menuOpen);

		playsetEditor.enabled = false;
		store.enabled = false;
	}

	public void onGameButton(){
		//gameManager.GetComponent(MonoBehaviour).enabled = true;
		game.enabled = true;
		menuOpen = false;
		backdrop.SetActive (menuOpen);

	}

	public void onPlaysetButton(){
		playsetEditor.enabled = !playsetEditor.enabled;

		menuOpen = false;
		backdrop.SetActive (false);
	}

	public void onStoreButton(){
		store.enabled = !store.enabled;


		menuOpen = false;
		backdrop.SetActive (false);
	}

}
