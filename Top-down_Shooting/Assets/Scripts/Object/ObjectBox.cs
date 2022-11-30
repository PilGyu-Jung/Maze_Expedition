using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBox : MonoBehaviour
{
    public bool isOverlaped = false;

    private Renderer myRenderer;

    public Color touchColor;
    private Color originColor;

    // Start is called before the first frame update
    void Start()
    {
        myRenderer = GetComponent<Renderer>();
        originColor = myRenderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Enter 충돌을 한 순간.
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EndPoint")
        {
            isOverlaped = true;
            myRenderer.material.color = touchColor;
        }
    }

    // Exit 붙어있다가 떼어질 때
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "EndPoint")
        {
            isOverlaped = false;
            myRenderer.material.color = originColor;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "EndPoint")
        {
            isOverlaped = true;
            myRenderer.material.color = touchColor;
        }

    }
}
