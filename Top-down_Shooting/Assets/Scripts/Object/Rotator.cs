using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public bool rotator_x;
    public bool rotator_y;
    public bool rotator_z;

    public float rotate_speed = 0;

    int return_Int(bool rotateVectorValue)
    {

        if (rotateVectorValue == true)
            return 1;
        else
            return 0;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotate_speed * Time.deltaTime * return_Int(rotator_x),
                        rotate_speed * Time.deltaTime * return_Int(rotator_y), 
                        rotate_speed * Time.deltaTime * return_Int(rotator_z),Space.Self);

        //Time.deltaTime 은 화면이 한번 깜박이는 시간 = 한 프레임의 시간
    }
}
