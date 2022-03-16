using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BundleBuilder : Editor
{
    [MenuItem("Assets/ Build AssetBundle")]
    static void buildAsset()
    {
        BuildPipeline.BuildAssetBundles(@"C:\Users\SteveP1\Desktop\AssetBundles", BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.WebGL);
    }
}