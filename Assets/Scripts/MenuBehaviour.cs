using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehaviour : MonoBehaviour {

	public void LoadLevel (string levelName) {
		SceneManager.LoadScene (levelName);
	}

	public void QuitGame () {
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#else
			Application.QuitGame();
		#endif
	}
}
