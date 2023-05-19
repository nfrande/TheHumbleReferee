using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
//using UnityEditor.EditorTools;
//using UnityEditor.ShortcutManagement;

public class FieldInfo : MonoBehaviour
{
    public static FieldInfo instance;
    public Vector2 fieldSize;

    [Header("Goals")]
    public Vector2 team1Goal, team2Goal;
    [Header("PenaltyBenches")]
    public Vector2 team1Bench, team2Bench;

    public Transform BallPosition;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(transform.position, fieldSize);
        
        Gizmos.DrawCube(team1Goal, Vector3.one*0.25f);
        Gizmos.DrawCube(team2Goal, Vector3.one*0.25f);
    }

}

//[EditorTool("Soccer field tool", typeof(FieldInfo))]
//public class FieldTool : EditorTool, IDrawSelectedHandles
//{
//    void OnEnable()
//    {
//
//    }
//
//    void OnDisable()
//    {
//
//    }
//
//    public void OnDrawHandles()
//    {
//        FieldInfo[] infos = Selection.GetFiltered<FieldInfo>(SelectionMode.TopLevel);
//        if(infos.Length > 0)
//        {
//            infos[0].fieldSize = Handles.PositionHandle(infos[0].fieldSize, Quaternion.Euler(0,0,0));
//        }
//    }
//
//    [Shortcut("Enable soccer field tool",typeof(SceneView),KeyCode.B)]
//    public void Activate()
//    {
//        if(Selection.GetFiltered<FieldInfo>(SelectionMode.TopLevel).Length > 0)
//        Debug.Log("FieldTool");
//    }
//
//    public override void OnToolGUI(EditorWindow window)
//    {
//        Handles.BeginGUI();
//        Handles.EndGUI();
//    }
//}
//