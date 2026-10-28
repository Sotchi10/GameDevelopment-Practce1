using System.Collections.Generic;
using System.IO;
using GameDevelopmentPractice.Gameplay;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameDevelopmentPractice.Editor
{
    public static class ObstacleShowcaseBuilder
    {
        private const string ScenePath = "Assets/_Game/Scenes/Levels/ObstacleShowcase.unity";
        private const string PrefabFolder = "Assets/_Game/Prefabs/Gameplay/Obstacles";
        private const string MaterialFolder = "Assets/_Game/Art/Materials/Obstacles";

        [MenuItem("Tools/Game Development/Build Obstacle Showcase")]
        public static void Build()
        {
            EnsureFolder(PrefabFolder);
            EnsureFolder(MaterialFolder);

            Material blue = GetOrCreateMaterial("Track_Blue", new Color(0.10f, 0.28f, 0.46f));
            Material green = GetOrCreateMaterial("Safe_Green", new Color(0.18f, 0.65f, 0.34f));
            Material yellow = GetOrCreateMaterial("Rotator_Yellow", new Color(1.00f, 0.66f, 0.08f));
            Material orange = GetOrCreateMaterial("Mover_Orange", new Color(1.00f, 0.32f, 0.08f));
            Material red = GetOrCreateMaterial("Spikes_Red", new Color(0.78f, 0.06f, 0.08f));
            Material white = GetOrCreateMaterial("Marker_White", new Color(0.88f, 0.92f, 0.96f));

            GameObject rotatingPrefab = CreateRotatingBarPrefab(yellow);
            GameObject movingPrefab = CreateMovingWallPrefab(orange);
            GameObject spikePrefab = CreateSpikeGatePrefab(red);

            Scene scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

            GameObject environment = new GameObject("--- ENVIRONMENT ---");
            GameObject gameplay = new GameObject("--- GAMEPLAY ---");
            GameObject obstacleRoot = new GameObject("Obstacles (3)");
            obstacleRoot.transform.SetParent(gameplay.transform);

            CreatePrimitive("Track", PrimitiveType.Cube, environment.transform,
                new Vector3(0f, -0.5f, 8f), new Vector3(9f, 1f, 42f), blue);
            CreatePrimitive("Start Zone", PrimitiveType.Cube, environment.transform,
                new Vector3(0f, 0.08f, -10f), new Vector3(8.5f, 0.15f, 2f), green);
            CreatePrimitive("Finish Zone", PrimitiveType.Cube, environment.transform,
                new Vector3(0f, 0.08f, 26f), new Vector3(8.5f, 0.15f, 2f), green);

            for (int z = -8; z <= 24; z += 4)
            {
                CreatePrimitive($"Lane Marker {z}", PrimitiveType.Cube, environment.transform,
                    new Vector3(0f, 0.03f, z), new Vector3(0.12f, 0.06f, 1.5f), white);
            }

            InstantiatePrefab(rotatingPrefab, obstacleRoot.transform, "01 - Rotating Bar", new Vector3(0f, 0f, 0f));
            InstantiatePrefab(movingPrefab, obstacleRoot.transform, "02 - Moving Wall", new Vector3(0f, 0f, 9f));
            InstantiatePrefab(spikePrefab, obstacleRoot.transform, "03 - Rising Spikes", new Vector3(0f, 0f, 18f));

            GameObject player = CreatePrimitive("Player Preview", PrimitiveType.Capsule, gameplay.transform,
                new Vector3(0f, 1f, -10f), new Vector3(1f, 1f, 1f), white);
            Object.DestroyImmediate(player.GetComponent<Collider>());

            CreateLighting(environment.transform);
            CreateCamera(environment.transform);

            EditorSceneManager.SaveScene(scene, ScenePath);
            AddSceneToBuildSettings();
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Selection.activeGameObject = obstacleRoot;
            Debug.Log($"Obstacle showcase created at {ScenePath}");
        }

        private static GameObject CreateRotatingBarPrefab(Material material)
        {
            GameObject root = new GameObject("RotatingBar");
            root.AddComponent<RotatingObstacle>();
            CreatePrimitive("Center Post", PrimitiveType.Cylinder, root.transform,
                new Vector3(0f, 0.65f, 0f), new Vector3(0.45f, 0.65f, 0.45f), material);
            CreatePrimitive("Rotating Beam", PrimitiveType.Cube, root.transform,
                new Vector3(0f, 1.15f, 0f), new Vector3(8f, 0.35f, 0.45f), material);
            return SaveAndDestroy(root, $"{PrefabFolder}/RotatingBar.prefab");
        }

        private static GameObject CreateMovingWallPrefab(Material material)
        {
            GameObject root = new GameObject("MovingWall");
            root.AddComponent<PingPongObstacle>();
            CreatePrimitive("Sliding Wall", PrimitiveType.Cube, root.transform,
                new Vector3(0f, 1.25f, 0f), new Vector3(3f, 2.5f, 0.8f), material);
            return SaveAndDestroy(root, $"{PrefabFolder}/MovingWall.prefab");
        }

        private static GameObject CreateSpikeGatePrefab(Material material)
        {
            GameObject root = new GameObject("RisingSpikes");
            PingPongObstacle mover = root.AddComponent<PingPongObstacle>();
            SerializedObject serializedMover = new SerializedObject(mover);
            serializedMover.FindProperty("localDirection").vector3Value = Vector3.up;
            serializedMover.FindProperty("distance").floatValue = 0.65f;
            serializedMover.FindProperty("speed").floatValue = 1.1f;
            serializedMover.ApplyModifiedPropertiesWithoutUndo();

            for (int i = 0; i < 7; i++)
            {
                GameObject spike = CreatePrimitive($"Spike {i + 1}", PrimitiveType.Cube, root.transform,
                    new Vector3(-3f + i, 0.55f, 0f), new Vector3(0.65f, 1.1f, 0.65f), material);
                spike.transform.localRotation = Quaternion.Euler(0f, 0f, 45f);
            }
            return SaveAndDestroy(root, $"{PrefabFolder}/RisingSpikes.prefab");
        }

        private static GameObject SaveAndDestroy(GameObject instance, string path)
        {
            GameObject prefab = PrefabUtility.SaveAsPrefabAsset(instance, path);
            Object.DestroyImmediate(instance);
            return prefab;
        }

        private static void InstantiatePrefab(GameObject prefab, Transform parent, string name, Vector3 position)
        {
            GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
            instance.name = name;
            instance.transform.SetParent(parent);
            instance.transform.position = position;
        }

        private static GameObject CreatePrimitive(string name, PrimitiveType type, Transform parent,
            Vector3 localPosition, Vector3 localScale, Material material)
        {
            GameObject item = GameObject.CreatePrimitive(type);
            item.name = name;
            item.transform.SetParent(parent);
            item.transform.localPosition = localPosition;
            item.transform.localScale = localScale;
            item.GetComponent<Renderer>().sharedMaterial = material;
            return item;
        }

        private static void CreateLighting(Transform parent)
        {
            GameObject lightObject = new GameObject("Directional Light");
            lightObject.transform.SetParent(parent);
            lightObject.transform.rotation = Quaternion.Euler(48f, -32f, 0f);
            Light light = lightObject.AddComponent<Light>();
            light.type = LightType.Directional;
            light.intensity = 1.25f;
            light.shadows = LightShadows.Soft;
        }

        private static void CreateCamera(Transform parent)
        {
            GameObject cameraObject = new GameObject("Main Camera");
            cameraObject.tag = "MainCamera";
            cameraObject.transform.SetParent(parent);
            cameraObject.transform.position = new Vector3(15f, 18f, -20f);
            cameraObject.transform.LookAt(new Vector3(0f, 1f, 8f));
            Camera camera = cameraObject.AddComponent<Camera>();
            camera.fieldOfView = 46f;
            camera.clearFlags = CameraClearFlags.Skybox;
            cameraObject.AddComponent<AudioListener>();
        }

        private static Material GetOrCreateMaterial(string name, Color color)
        {
            string path = $"{MaterialFolder}/{name}.mat";
            Material material = AssetDatabase.LoadAssetAtPath<Material>(path);
            if (material != null)
            {
                return material;
            }

            Shader shader = Shader.Find("Universal Render Pipeline/Lit") ?? Shader.Find("Standard");
            material = new Material(shader) { name = name, color = color };
            AssetDatabase.CreateAsset(material, path);
            return material;
        }

        private static void AddSceneToBuildSettings()
        {
            List<EditorBuildSettingsScene> scenes = new List<EditorBuildSettingsScene>(EditorBuildSettings.scenes);
            if (scenes.Exists(scene => scene.path == ScenePath))
            {
                return;
            }
            scenes.Add(new EditorBuildSettingsScene(ScenePath, true));
            EditorBuildSettings.scenes = scenes.ToArray();
        }

        private static void EnsureFolder(string path)
        {
            string current = "Assets";
            string[] parts = path.Split('/');
            for (int i = 1; i < parts.Length; i++)
            {
                string next = $"{current}/{parts[i]}";
                if (!AssetDatabase.IsValidFolder(next))
                {
                    AssetDatabase.CreateFolder(current, parts[i]);
                }
                current = next;
            }
        }
    }
}
