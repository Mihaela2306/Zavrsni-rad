using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform[] points;
    public float moveSpeed;
    private int currentPoint;
    public Transform platform;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        // Logic for moving the platform
        platform.position = Vector3.MoveTowards(platform.position, points[currentPoint].position, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(platform.position, points[currentPoint].position) < .05f) {
            currentPoint++;
            if (currentPoint >= points.Length) {
                currentPoint = 0;
            }
        }
    }
}
