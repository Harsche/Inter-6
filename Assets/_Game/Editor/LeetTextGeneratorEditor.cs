using UnityEditor;
using UnityEngine;

namespace LeetTextGenerator{
    [CustomEditor(typeof(LeetTextGenerator))]
    public class LeetTextGeneratorEditor : Editor{
        public override void OnInspectorGUI(){
            DrawDefaultInspector();

            var script = (LeetTextGenerator) target;
            if (GUILayout.Button("Gerar Texto")){
                script.GenerateEncryptedText();
            }
        }
    }
}