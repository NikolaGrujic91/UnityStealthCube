using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour {

    public float speed = 3.0f;
    public float rotateSpeed = 3.0f;
    private CharacterController controller;

    void Start()
    {
        //Get controller component on the beginning
        controller = GetComponent<CharacterController>();
    }

	void Update ()
    {
        //Rotate player on A,D
        transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);

        //Move player with W,S
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        float curSpeed = speed * Input.GetAxis("Vertical");
        controller.SimpleMove(forward * curSpeed);
	}
}
