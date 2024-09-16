using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public static class CustomAssert
{
    public static void Ensure(bool condition, string message)
    {
        if (!condition)
        {
            Debug.LogError(message);

#if UNITY_EDITOR
            EditorUtility.DisplayDialog("Error!", message, "OK");

            EditorApplication.isPlaying = false;
#endif
        }
    }
}
