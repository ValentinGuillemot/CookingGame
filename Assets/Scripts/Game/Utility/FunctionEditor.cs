using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Function), true)]
[CanEditMultipleObjects]
public class FunctionEditor : Editor
{
    private Material _mat;

    private void OnEnable()
    {
        _mat = new Material(Shader.Find("Hidden/Internal-Colored"));
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        // Drawing zone to display what the function looks like on a scale from 0 to 1
        Rect rect = GUILayoutUtility.GetRect(10, 1000, 150, 150);
        if (Event.current.type == EventType.Repaint)
        {
            // Set up drawing zone
            GUI.BeginClip(rect);
            GL.PushMatrix();
            GL.Clear(true, false, Color.black);
            _mat.SetPass(0);

            // Draw background quad
            GL.Begin(GL.QUADS);
            GL.Color(Color.white);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(rect.width, 0, 0);
            GL.Vertex3(rect.width, rect.height, 0);
            GL.Vertex3(0, rect.height, 0);
            GL.End();

            // Draw lines for function values
            GL.Begin(GL.LINES);
            GL.Color(Color.black);
            GL.Vertex3(50f, 125f, 0f);
            GL.Vertex3(150f, 125f, 0f);
            GL.Vertex3(50f, 125f, 0f);
            GL.Vertex3(50f, 25f, 0f);
            GL.Color(Color.blue);

            Function f = (Function)target;
            float previousY = f.GetValue(0f);
            float currentY = 0;
            for (float i = 0.01f; i < 1.0f; i += 0.01f)
            {
                currentY = f.GetValue(i);
                GL.Vertex3(50f + (i - 0.01f) * 100f, 25f + 100f - previousY * 100f, 0);
                GL.Vertex3(50f + i * 100f, 25f + 100f - currentY * 100f, 0);
                previousY = currentY;
            }
            
            GL.End();

            // End drawing
            GL.PopMatrix();
            GUI.EndClip();
        }
    }
}
