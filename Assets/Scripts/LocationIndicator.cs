using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationIndicator : MonoBehaviour {
    public float w = 0.05f;
    public float h = 0.05f;

    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {

	}

    // Draw bounding box in editor 
    void OnDrawGizmosSelected()
    {
        Vector2 tl = new Vector2(transform.position.x - (w / 2.0f), transform.position.y - (h / 2.0f));
        Vector2 tr = new Vector2(transform.position.x + (w / 2.0f), transform.position.y - (h / 2.0f));
        Vector2 bl = new Vector2(transform.position.x - (w / 2.0f), transform.position.y + (h / 2.0f));
        Vector2 br = new Vector2(transform.position.x + (w / 2.0f), transform.position.y + (h / 2.0f));

        Gizmos.color = Color.red;
        Gizmos.DrawLine(tl, tr);
        Gizmos.DrawLine(tr, br);
        Gizmos.DrawLine(br, bl);
        Gizmos.DrawLine(bl, tl);
    }
}
