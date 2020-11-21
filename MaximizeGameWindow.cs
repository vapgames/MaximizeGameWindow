using UnityEditor;
using UnityEngine;
using System.Reflection;
using System;

//EDITOR script
//toggles game window maximize in playmode
//default hotkey: Shift + ESC
//change the hotkey to your liking.

[InitializeOnLoad]
static public class MaximizeGameWindow
{
	
	static MaximizeGameWindow() {
		EditorApplication.update -= OnGameUpdate;
		EditorApplication.update += OnGameUpdate;

		SceneView.duringSceneGui -= OnSceneGUI;
		SceneView.duringSceneGui += OnSceneGUI;
	}

	static public void OnGameUpdate() {
		//TOGGLE GAME WINDOW MAXIMIZED WHILE GAME WINDOW IS FOCUSED
		if ((Input.GetKey(KeyCode.Escape) && Input.GetKey(KeyCode.LeftShift))) {
			ToggleGameViewMaximize();
		}
	}
	static public void OnSceneGUI(SceneView scene_view) {
		//MAXIMIZE GAME WINDOW WHILE OTHER WINDOWS ARE FOCUSED
		if (Event.current.shift && Event.current.keyCode == KeyCode.Escape && Application.isPlaying) {
			ToggleGameViewMaximize();
		}
	}

	static public void ToggleGameViewMaximize() {
		EditorWindow[] windows = (EditorWindow[])Resources.FindObjectsOfTypeAll(typeof(UnityEditor.EditorWindow));
		for (int i = 0; i < windows.Length; ++i) {
			EditorWindow window = windows[i];
			if (window != null && window.GetType().FullName == "UnityEditor.GameView") {
				window.maximized = !window.maximized;
				break;
			}
		}
	}
}