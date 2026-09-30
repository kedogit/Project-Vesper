using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

[CustomEditor(typeof(Player))]
public class PlayerInspectorEditor : Editor
{
    private float m_amountDealt = 25f;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        Player playerScript = (Player)target;

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Debug", EditorStyles.boldLabel);

        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Amount Dealt/Healed");
        m_amountDealt = EditorGUILayout.FloatField(m_amountDealt);

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Heal"))
        {
            playerScript.Heal(m_amountDealt);
        }

        if (GUILayout.Button("Hurt"))
        {
            playerScript.Hurt(m_amountDealt);
        }

        EditorGUILayout.EndHorizontal();
    }
}
