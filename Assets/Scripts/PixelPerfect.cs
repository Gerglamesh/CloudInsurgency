using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelPerfect : MonoBehaviour {
    public int PixelsPerUnit = 32;
    public int PixelsPerUnitScale = 2;
    public int VerticalResolution = 768;

	// Use this for initialization
	void Start () {
        Camera.main.orthographicSize = ((VerticalResolution) / (PixelsPerUnitScale * PixelsPerUnit)) * 0.5f;
    }
	
	// Update is called once per frame
	void Update () {
        
    }
}
