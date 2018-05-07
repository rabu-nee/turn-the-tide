using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenDraw : MonoBehaviour {

	public Camera S1_CAM;
	public Camera S2_CAM;

	public Renderer S1_SCREEN;
	public Renderer S2_SCREEN;

	RenderTexture S1_TEX;
	RenderTexture S2_TEX;

	Material S1_MAT;
	Material S2_MAT;

	//BUILT-IN-FUNCTIONS===================================================================================================================

	void Start(){
		//Initialize render textures
		S1_TEX = new RenderTexture(Screen.width, Screen.height, 0);
		S2_TEX = new RenderTexture(Screen.width, Screen.height, 0);

		//Initialize materials
		S1_MAT = new Material(Shader.Find("Sprites/Default"));
		S2_MAT = new Material(Shader.Find("Sprites/Default"));

		//Set camera target texture
		S1_CAM.targetTexture = S1_TEX;
		S2_CAM.targetTexture = S2_TEX;

		//Apply render texture to materials
		S1_MAT.SetTexture ("_MainTex", S1_TEX);
		S2_MAT.SetTexture ("_MainTex", S2_TEX);

		//Apply materials to screen quads
		S1_SCREEN.material = S1_MAT;
		S2_SCREEN.material = S2_MAT;
	}
}
