using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [RequireComponent(typeof(Rigidbody))]
public class CharactorManager : MonoBehaviour
{
    public float walkSpeed = 3;
    public float runSpeed = 5;
    public float jumpHeight = 3;
    private Rigidbody rb;
    private float inputVertical;
    private float inputHorizontal;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        inputVertical = Input.GetAxis("Vertical");
        inputHorizontal = Input.GetAxis("Horizontal");

        rb.AddForce(new Vector3(inputVertical, 0, inputHorizontal) * walkSpeed);
    }
}
