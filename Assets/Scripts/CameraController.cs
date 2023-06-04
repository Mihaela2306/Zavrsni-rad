using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public Transform target;
    public float minHeight, maxHeight;

    public Transform farBackground, middleBackground;
    private Vector2 lastPos;

    public bool stopFollow;

    // Awake is used to initialize something before the game starts
    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        lastPos = transform.position;
    }

    // Update is called once per frame
    void Update() {
        if (!stopFollow) {
            // Camera following a specific target setup in Unity
            transform.position = new Vector3(target.position.x, Mathf.Clamp(target.position.y, minHeight, maxHeight), transform.position.z);

            // Making the far background move at the same speed as the camera and making the 
            // middle background move a little slower so that we get the effect of a dynamic background
            Vector2 amountToMove = new Vector2(transform.position.x - lastPos.x, transform.position.y - lastPos.y);
            farBackground.position += new Vector3(amountToMove.x, amountToMove.y, 0f);
            middleBackground.position += new Vector3(amountToMove.x, amountToMove.y, 0f) * .5f;
            lastPos = transform.position;
        }
    }
}
