using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class WorldScrolling : MonoBehaviour {

    public bool enableScrolling = false;
    public float vertScrollSpeed = 0.02f;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (enableScrolling)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).transform.Translate(new Vector3(.0f, vertScrollSpeed, .0f));
            }
        }

        if(Input.GetButtonDown("R"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (Input.GetButtonDown("E"))
        {
            enableScrolling = !enableScrolling;
        }
    }
}
