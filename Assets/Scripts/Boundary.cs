using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour {
    public Rect rect;
    public float bufferZone = 0.2f;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        rect.y = transform.position.y - (rect.height / 2);
	}

    public float GetXMin() { return rect.xMin + bufferZone; }
    public float GetYMin() { return rect.yMin + bufferZone; }
    public float GetXMax() { return rect.xMax - bufferZone; }
    public float GetYMax() { return rect.yMax - bufferZone; }

    // Draw bounding box in editor 
    void OnDrawGizmosSelected()
    {
        Vector2 topLeft = new Vector2(rect.min.x, rect.max.y);
        Vector2 bottomRight = new Vector2(rect.max.x, rect.min.y);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(rect.min, topLeft);
        Gizmos.DrawLine(topLeft, rect.max);
        Gizmos.DrawLine(rect.max, bottomRight);
        Gizmos.DrawLine(bottomRight, rect.min);
    }

    public bool OutsideAndBelowBoundary(Vector2 point)
    {
        return !rect.Contains(point) && (point.y < (rect.y - (rect.height / 2.0f)));
    }
}
