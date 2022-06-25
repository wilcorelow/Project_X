using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WaveManager))]
public class WaveEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        WaveManager waveManager = (WaveManager)target;
        
        GUILayout.Space(10);
        waveManager.height = EditorGUILayout.Slider("Spawn Area", waveManager.height, 25, 14);

        waveManager.width = waveManager.height + 24;
        waveManager.northRight = waveManager.width - 19;
        waveManager.eastUp = waveManager.height - 9f;
    }
}
