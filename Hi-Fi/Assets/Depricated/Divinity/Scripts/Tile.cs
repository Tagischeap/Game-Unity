using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
	Renderer rend;
	// Use this for initialization
	void Start () {
		rend = this.GetComponent<Renderer> ();
		rend.material.shader = Shader.Find("Specular");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnMouseEnter(){
		rend.material.SetColor("_SpecColor", Color.yellow);
		Debug.Log("Mouse Enter");
		
	}
	void OnMouseExit(){
		rend.material.SetColor("_SpecColor", Color.white);
	}

}
