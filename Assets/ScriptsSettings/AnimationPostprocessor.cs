using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class AnimationPostprocessor : AssetPostprocessor
{
    static AnimationPostProcessorSettings settings;
    static Avatar referenceAvatar;
    static GameObject referenceFBX;
    static ModelImporter referenceImporter;
    static bool settingsLoaded = false;

    // Caminho dinâmico da pasta de destino
    public static string dynamicTargetFolderPath = string.Empty;

    void OnPreprocessModel()
    {
        LoadSettings();
        if (!settingsLoaded || !settings.enabled) return;

        // Obtém o caminho da pasta de destino (dinâmico ou configurado no settings)
        string targetFolderPath = !string.IsNullOrEmpty(dynamicTargetFolderPath)
            ? dynamicTargetFolderPath
            : AssetDatabase.GetAssetPath(settings.targetFolderAsset);

        // Verifica se o caminho é válido
        if (string.IsNullOrEmpty(targetFolderPath)) return;

        // Verifica se o asset está dentro da pasta de destino configurada
        ModelImporter importer = assetImporter as ModelImporter;
        if (!importer.assetPath.StartsWith(targetFolderPath)) return;

        // Reimporta o asset para aplicar as configurações
        AssetDatabase.ImportAsset(importer.assetPath);

        // Extrai materiais e texturas, se configurado
        if (settings.extractTextures)
        {
            importer.ExtractTextures(Path.GetDirectoryName(importer.assetPath));
            importer.materialLocation = ModelImporterMaterialLocation.External;
        }

        // Configura o avatar de referência
        if (referenceAvatar == null)
        {
            referenceAvatar = referenceImporter.sourceAvatar;
        }

        // Configura o avatar e o tipo de rig do modelo importado
        importer.sourceAvatar = referenceAvatar;
        importer.animationType = settings.animationType;

        // Ajusta para Generic se houver problema com o avatar
        if (referenceAvatar == null || !referenceAvatar.isValid)
        {
            importer.animationType = ModelImporterAnimationType.Generic;
        }

        // Serializa o avatar para aplicar corretamente as configurações
        SerializedObject serializedObject = new SerializedObject((UnityEngine.Object)importer.sourceAvatar);
        using (SerializedObject sourceObject = new SerializedObject((UnityEngine.Object)referenceAvatar))
            CopyHumanDescriptionToDestination(sourceObject, serializedObject);
        serializedObject.ApplyModifiedProperties();
        importer.sourceAvatar = serializedObject.targetObject as Avatar;
        serializedObject.Dispose();

        // Ativa Translation DoF, se configurado
        if (settings.enableTranslationDoF)
        {
            var importerHumanDescription = importer.humanDescription;
            importerHumanDescription.hasTranslationDoF = true;
            importer.humanDescription = importerHumanDescription;
        }

        // Usa reflexão para chamar o método Apply do Editor
        if (settings.forceEditorApply)
        {
            var editorType = typeof(Editor).Assembly.GetType("UnityEditor.ModelImporterEditor");
            var nonPublic = BindingFlags.NonPublic | BindingFlags.Instance;
            var editor = Editor.CreateEditor(importer, editorType);
            editorType.GetMethod("Apply", nonPublic).Invoke(editor, null);
            UnityEngine.Object.DestroyImmediate(editor);
        }
    }

    void OnPreprocessAnimation()
    {
        LoadSettings();
        if (!settingsLoaded || !settings.enabled) return;

        // Obtém o caminho da pasta de destino (dinâmico ou configurado no settings)
        string targetFolderPath = !string.IsNullOrEmpty(dynamicTargetFolderPath)
            ? dynamicTargetFolderPath
            : AssetDatabase.GetAssetPath(settings.targetFolderAsset);

        if (string.IsNullOrEmpty(targetFolderPath)) return;

        // Verifica se o asset está dentro da pasta de destino configurada
        ModelImporter importer = assetImporter as ModelImporter;
        if (!importer.assetPath.StartsWith(targetFolderPath)) return;

        // Copia configurações do modelo de referência para o importador atual
        ModelImporter modelImporter = CopyModelImporterSettings(importer);

        // Força a reimportação do asset
        AssetDatabase.ImportAsset(modelImporter.assetPath, ImportAssetOptions.ForceUpdate);
    }

    ModelImporter CopyModelImporterSettings(ModelImporter importer)
    {
        // Copia as configurações do importador de referência
        importer.globalScale = referenceImporter.globalScale;
        importer.useFileScale = referenceImporter.useFileScale;
        importer.meshCompression = referenceImporter.meshCompression;
        importer.isReadable = referenceImporter.isReadable;
        importer.optimizeMeshPolygons = referenceImporter.optimizeMeshPolygons;
        importer.optimizeMeshVertices = referenceImporter.optimizeMeshVertices;
        importer.importBlendShapes = referenceImporter.importBlendShapes;
        importer.keepQuads = referenceImporter.keepQuads;
        importer.indexFormat = referenceImporter.indexFormat;
        importer.weldVertices = referenceImporter.weldVertices;
        importer.importVisibility = referenceImporter.importVisibility;
        importer.importCameras = referenceImporter.importCameras;
        importer.importLights = referenceImporter.importLights;
        importer.preserveHierarchy = referenceImporter.preserveHierarchy;
        importer.swapUVChannels = referenceImporter.swapUVChannels;
        importer.generateSecondaryUV = referenceImporter.generateSecondaryUV;
        importer.importNormals = referenceImporter.importNormals;
        importer.normalCalculationMode = referenceImporter.normalCalculationMode;
        importer.normalSmoothingAngle = referenceImporter.normalSmoothingAngle;
        importer.importTangents = referenceImporter.importTangents;

        // Copia configurações de animação
        importer.animationType = referenceImporter.animationType;
        importer.optimizeGameObjects = referenceImporter.optimizeGameObjects;

        // Copia materiais
        importer.materialImportMode = referenceImporter.materialImportMode;
        importer.materialLocation = referenceImporter.materialLocation;
        importer.materialName = referenceImporter.materialName;

        return importer;
    }

    void CopyHumanDescriptionToDestination(SerializedObject sourceObject, SerializedObject serializedObject)
    {
        serializedObject.CopyFromSerializedProperty(sourceObject.FindProperty("m_HumanDescription"));
    }

    static void LoadSettings()
    {
        var guids = AssetDatabase.FindAssets("t:AnimationPostProcessorSettings");
        if (guids.Length > 0)
        {
            var path = AssetDatabase.GUIDToAssetPath(guids[0]);
            settings = AssetDatabase.LoadAssetAtPath<AnimationPostProcessorSettings>(path);

            referenceAvatar = settings.referenceAvatar;
            referenceFBX = settings.referenceFBX;
            referenceImporter = AssetImporter.GetAtPath(AssetDatabase.GetAssetPath(referenceFBX)) as ModelImporter;
            settingsLoaded = true;
        }
        else
        {
            settingsLoaded = false;
        }
    }
}
