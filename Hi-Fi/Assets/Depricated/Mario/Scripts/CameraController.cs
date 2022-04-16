using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController1 : MonoBehaviour
{
    public GameObject objectToFollow;
    public float speed = 2.0f;
    Vector3 position;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float interpolation = speed * Time.deltaTime;
        position = this.transform.position;
        
        //poisition.y - Mathf.Lerp(position.y, interpolation);
        //poisition.x - Mathf.Lerp(position.x, interpolation);

        transform.position = position;
    }
}
