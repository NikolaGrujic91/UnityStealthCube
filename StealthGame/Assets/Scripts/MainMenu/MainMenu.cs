using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartLevel1()
    {
        Application.LoadLevel(1);
    }

    public void StartLevel2()
    {
        Application.LoadLevel(2);
    }
     
    public void StartLevel3()
    {
        Application.LoadLevel(3);
    }

    public void Quit()
    {
        Application.Quit();
    }

}
