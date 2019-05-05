using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class UnityPackageExporter
{
    [MenuItem("Assets/Export as Unitypackage")]
    public static void ExportUnityPackage ()
    {
        Debug.Log ("Export as Unitypackage");
        AssetDatabase.ExportPackage ("Assets/ImportedLinq", "ImportedLinq.unitypackage", ExportPackageOptions.Recurse);
    }
}
