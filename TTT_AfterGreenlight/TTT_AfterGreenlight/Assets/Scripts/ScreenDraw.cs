using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenDraw : MonoBehaviour {
	public Camera CAM;
	public Vector2 TextureSize;
	public int antiAliasing = 0;
	Renderer SCREEN;
	RenderTexture TEX;
	Material MAT;

	void Start() {
		//Initialize Screen
		SCREEN = GetComponent<Renderer>();
		//Initialize render textures
		TEX = new RenderTexture((int)TextureSize.x, (int)TextureSize.y, 0);
		//Apply anti aliasing settings to texture
		TEX.antiAliasing = antiAliasing;
		//Initialize materials
		MAT = new Material(Shader.Find("Sprites/Default"));
		//Set camera target texture
		CAM.targetTexture = TEX;
		//Apply render texture to materials
		MAT.SetTexture ("_MainTex", TEX);
		//Apply materials to screen
		SCREEN.material = MAT;
	}
}
