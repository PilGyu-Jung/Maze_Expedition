using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AIWandering))]
public class AIWanderingEditor : Editor
{
    private void OnSceneGUI()
    {
        AIWandering aiWandering = (AIWandering)target;
        Handles.color = Color.blue;
        Handles.DrawWireArc(aiWandering.transform.position, Vector3.up,Vector3.forward, 360, aiWandering.walkRadius);

        Handles.color = Color.red;
        Handles.DrawWireArc(aiWandering.transform.position, Vector3.up, Vector3.forward, 360, aiWandering.attackRadius);

        Handles.color = Color.yellow;
        Handles.DrawWireArc(aiWandering.transform.position, Vector3.up, Vector3.forward, 360, aiWandering.chaseRadius);

    }
}
