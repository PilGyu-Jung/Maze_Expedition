using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHold : MonoBehaviour
{
    public Transform m_target;
    public float m_Height = 10f;
    public float m_Distance = 20f;
    public float m_Angle = 45f;

    // Start is called before the first frame update
    void Start()
    {
        HandleCamera();
    }

    // Update is called once per frame
    void Update()
    {
        HandleCamera();
    }

    private void HandleCamera()
    {
        if(!m_target)
        {
            return;
        }

        Vector3 worldPostiion = (Vector3.forward * -m_Distance) + (Vector3.up * m_Height);
        Debug.DrawLine(m_target.position, worldPostiion, Color.red);

        Vector3 rotatedVector3 = Quaternion.AngleAxis(m_Angle, Vector3.up) * worldPostiion;
        Debug.DrawLine(m_target.position, rotatedVector3, Color.green);

        Vector3 flatTargetPosition = m_target.position;
        flatTargetPosition.y = 0f;
        Vector3 finalPosition = flatTargetPosition + rotatedVector3;
        Debug.DrawLine(m_target.position, finalPosition, Color.blue);
            
        transform.position = finalPosition;
        transform.LookAt(flatTargetPosition);
    }
}
