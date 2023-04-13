using System;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

// Build �� ���������� �ڵ������Ͽ� Resources ������ ����
public class BuildIncrementor : IPreprocessBuildWithReport
{
    public int callbackOrder => 1;

    public void OnPreprocessBuild(BuildReport report)
    {
        var obj = ScriptableObject.CreateInstance<BuildScriptableObject>();
        obj.platform = report.summary.platform.ToString();
        obj.date = DateTime.Now.ToString("yyyy.MM.dd.HHmmss");
        obj.buildNumber = PlayerSettings.Android.bundleVersionCode++;
        
        AssetDatabase.DeleteAsset("Assets/Resources/Build.asset");
        AssetDatabase.CreateAsset(obj, "Assets/Resources/Build.asset");
        AssetDatabase.SaveAssets();
    }

}
