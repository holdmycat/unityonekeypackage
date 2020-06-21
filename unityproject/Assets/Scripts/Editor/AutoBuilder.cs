using UnityEngine;
using UnityEditor;
using System.IO;
using Debug = UnityEngine.Debug;
using System.Collections.Generic;

public static class AutoBuilder
{
	static string GetProjectName()
	{
		string[] s = Application.dataPath.Split('/');
		return s[s.Length - 2];
	}

	static string[] GetScenePaths()
	{
		List<string> scenes = new List<string>();
		for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
		{
			var scene = EditorBuildSettings.scenes[i];
			Debug.LogFormat("[Builder] Scenes [{0}]: {1}, [{2}]", i, scene.path, scene.enabled ? "x" : " ");

			if (scene.enabled)
			{
				scenes.Add(scene.path);
			}
		}

		return scenes.ToArray();
	}

	[MenuItem("Tools/Build For iOS: proj.ios")]
	static void BuildForiOS()
	{
		System.Console.WriteLine("[Builder] Starting to build iOS project ...");
		string projDir = Application.dataPath + "/../proj.ios";  // 这里是输出的目录， Output Project Path

		BuildOptions option = BuildOptions.None;
		if (Directory.Exists(projDir))
		{
			Debug.LogFormat("[Builder] project is existing: {0}", projDir);
			option = BuildOptions.AcceptExternalModificationsToPlayer;
		}
		else
		{
			Debug.LogFormat("[Builder] project is not existing: {0}", projDir);
		}

		var args = System.Environment.GetCommandLineArgs();
		Debug.LogFormat(args.ToString());

		if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.iOS)
		{
			Debug.LogFormat("[Builder] Current target is: {0}, switching to: {1}", EditorUserBuildSettings.activeBuildTarget, BuildTarget.iOS);
			if (!EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.iOS, BuildTarget.iOS))
			{
				Debug.LogFormat("[Builder] Switching to {0}/{1} failed!", BuildTargetGroup.iOS, BuildTarget.iOS);
				return;
			}
		}

		BuildPipeline.BuildPlayer(GetScenePaths(), projDir, BuildTarget.iOS, option);

		Debug.LogFormat("[Builder] Done: " + projDir);
	}
}