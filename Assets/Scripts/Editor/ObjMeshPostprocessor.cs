using UnityEngine;
using UnityEditor;

public class ObjMeshPostprocessor : AssetPostprocessor
{
    void OnPreprocessModel()
    {
        if (assetPath.EndsWith(".obj", System.StringComparison.OrdinalIgnoreCase))
        {
            try
            {
                var importer = (ModelImporter)assetImporter;
                importer.isReadable = true;
                importer.importNormals = ModelImporterNormals.Import;
                importer.importTangents = ModelImporterTangents.Import;
                Debug.Log($"Successfully applied settings to {assetPath}");
            }
            catch (System.Exception ex)
            {
                Debug.Log($"Error processing {assetPath}: {ex.Message}");
            }
        }
    }
}
