using UnityEngine;
using System.Collections;

public class PlayerInput0 : MonoBehaviour {

	GridMove gridMove;
	bool isInput = true;
	bool[] isPlayer = {true, false, false, false};

	public GameObject PauseGUI;
	GameObject instance;
	GameObject player;

	SplitScreen splitScreen;

	int playerN;


	void Awake (){

	}

	void Start () {
		player = GameObject.Find("Player");
		gridMove = player.GetComponent<GridMove>();

		//PauseGUI = player.GetComponentInChildren<Pause_Menu>();
		instance = GameObject.Find("Intance");
		//splitScreen = instance.GetComponent<SplitScreen>();
	}

	void ToggleInput(){
		if (isInput){
			gridMove.canMove = false;
			isInput = false;
			//Time.timeScale = 0;
			PauseGUI.SetActive(true);
		}
		else if (!isInput){
			gridMove.canMove = true;
			isInput = true;
			//Time.timeScale = 1;
			PauseGUI.SetActive(false);
		}
	}

	void Update () {
		if (Input.GetButtonDown(gridMove.ControllerInput[0,4])){
			ToggleInput();
		}
		/*if (Input.GetButtonDown(gridMove.ControllerInput[1,4])){
			if (!isPlayer[1]){
				splitScreen.multiplayer = true;
				splitScreen.SpawnPlayer(1);
				isPlayer[1] = true;
			}
		}*/
	}

	void CharacterControls(){
	
	}
}