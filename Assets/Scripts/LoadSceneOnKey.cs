using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadSceneOnKey : MonoBehaviour 
{
	public KeyCode key;
	[Tooltip("Exact name of the scene to load upon the button press")]
	public string sceneName;


	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyUp (key)) 
		{
			SceneManager.LoadScene (sceneName);
		}
	}
}
