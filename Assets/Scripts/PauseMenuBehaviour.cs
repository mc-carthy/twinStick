using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuBehaviour : MenuBehaviour {

	public static bool isPaused;
	public GameObject pauseMenu;
	public GameObject optionsMenu;

	public void Start () {
		isPaused = false;
		pauseMenu.SetActive (isPaused);
		optionsMenu.SetActive (false);
		UpdateQualityLevel ();
		UpdateVolumeLevel ();
	}
	
	public void Update () {
		if (Input.GetKeyUp (KeyCode.Escape)) {
			if (!optionsMenu.activeInHierarchy) {
				isPaused = !isPaused;
				Time.timeScale = (isPaused) ? 0 : 1;
				pauseMenu.SetActive (isPaused);
			} else {
				OpenPauseMenu ();
			}
		}
	}

	public void ResumeGame() {
		isPaused = false;
		pauseMenu.SetActive (isPaused);
		Time.timeScale = 1;
	}

	public void RestartGame() {
		SceneManager.LoadScene (SceneManager.GetActiveScene().name);
	}

	public void IncreaseQuality() {
		QualitySettings.IncreaseLevel ();
		UpdateQualityLevel ();
	}

	public void DecreaseQuality() {
		QualitySettings.DecreaseLevel();
		UpdateQualityLevel ();
	}

	public void SetVolume(float value) {
		AudioListener.volume = value;
		UpdateVolumeLevel ();
	}

	public void OpenOptions()
	{
		optionsMenu.SetActive(true);
		pauseMenu.SetActive(false);
	}

	public void OpenPauseMenu()
	{
		optionsMenu.SetActive(false);
		pauseMenu.SetActive(true);
	}

	private void UpdateQualityLevel() {
		int currentQuality = QualitySettings.GetQualityLevel ();
		string qualityName = QualitySettings.names [currentQuality];

		optionsMenu.transform.FindChild ("Quality Level").GetComponent<UnityEngine.UI.Text> ().text = "Quality Level - " + qualityName;
	}

	private void UpdateVolumeLevel() {
		optionsMenu.transform.FindChild ("Master Volume").GetComponent<UnityEngine.UI.Text> ().text = "Master Volume - " + (AudioListener.volume * 100).ToString ("f2") + "%";
	}
}
