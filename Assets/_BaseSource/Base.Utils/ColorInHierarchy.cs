#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[InitializeOnLoad]
public class ColorInHierarchy : MonoBehaviour
{
    #region [ static - Update editor ]
    private static readonly Dictionary<Object, Color> coloredObjects = new Dictionary<Object, Color>();
    private static readonly Vector2 offset = new Vector2(20, 1);

    static ColorInHierarchy() {
        EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyWindowItemOnGUI;
    }

    private static void HandleHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect) {
        var obj = EditorUtility.InstanceIDToObject(instanceID);

        if (obj != null && coloredObjects.TryGetValue(obj, out var o)) {
            if (((GameObject)obj).GetComponent<ColorInHierarchy>()) {
                HierarchyBackground(obj, selectionRect, o);
            }
            else {
                // the ColorInHierarchy component has been removed from the gameObject
                coloredObjects.Remove(obj);
            }
        }
    }

    private static void HierarchyBackground(Object obj, Rect selectionRect, Color backgroundColor) {
        var offsetRect = new Rect(selectionRect.position + offset, selectionRect.size);
        var bgRect = new Rect(selectionRect.x, selectionRect.y, selectionRect.width + 50, selectionRect.height);

        EditorGUI.DrawRect(bgRect, backgroundColor);
        EditorGUI.LabelField(offsetRect, obj.name, new GUIStyle() {
            normal = new GUIStyleState() { textColor = InvertColor(backgroundColor) },
            fontStyle = FontStyle.Bold
        });
    }

    private static Color InvertColor(Color toInvert) {
        Color.RGBToHSV(toInvert, out var h, out var s, out var v);
        h = (h + .5f) % 1;
        v = (v + .5f) % 1;
        return Color.HSVToRGB(h, s, v);
    }
    #endregion

    #region [ instanced - Set color to be used by the editor ]
    public Color colorInHierarchy = Color.grey;

    private void Reset() {
        OnValidate();
    }

    private void OnValidate() {
        if (!coloredObjects.ContainsKey(gameObject)) // notify editor of new color
        {
            coloredObjects.Add(gameObject, colorInHierarchy);
        }
        else if (coloredObjects != null && coloredObjects[gameObject] != colorInHierarchy) // color has changed
        {
            coloredObjects[gameObject] = colorInHierarchy;
        }
    }
    #endregion [ instanced -  Subscribe color to editor ]
}
#endif