using UnityEditor;
using UnityEngine;

namespace HaniJahanDesign.FreePack
{
    public class MyAssetWelcome : EditorWindow
    {
        Texture2D headerTexture;

        [MenuItem("Window/My Assets/Hani Jahan Design/Free Pack/Show Welcome")]
        public static void ShowWindow()
        {
            // Opens the window or focuses it if it's already open
            var window = GetWindow<MyAssetWelcome>("Free Modular 3D Platformer Pack");
            window.Show();
        }

        void OnEnable()
        {
            headerTexture = Resources.Load<Texture2D>("header"); // Make sure your image is in the right place!
        }

        void OnGUI()
        {
            // Outer padding
            GUILayout.BeginVertical();
            GUILayout.Space(10);

            // Main content with more padding on sides
            GUILayout.BeginHorizontal();
            GUILayout.Space(10); // Left padding

            GUILayout.BeginVertical();

            // --- HEADER IMAGE ---
            if (headerTexture != null)
            {
                float aspect = (float)headerTexture.width / headerTexture.height;
                float width = position.width - 40; // Padding on both sides
                float height = width / aspect;
                Rect rect = GUILayoutUtility.GetRect(width, height, GUILayout.ExpandWidth(true));
                GUI.DrawTexture(rect, headerTexture, ScaleMode.ScaleAndCrop);
            }

            GUILayout.Space(20);

            // --- TEXT SECTION ---
            GUILayout.Label("Welcome!", EditorStyles.boldLabel);
            GUILayout.Label(
                "Thanks for choosing Modular 3D Platformer Pack! Hope it sparks awesome ideas for your next game.",
                EditorStyles.wordWrappedLabel
            );
            GUILayout.Space(5);
            GUILayout.Label("Version: 1.0", EditorStyles.label);

            GUILayout.Space(15); // Section break

            // --- SUPPORT SECTION ---
            GUILayout.Label(
                "Need help or want to suggest a feature? We're here for you!",
                EditorStyles.wordWrappedLabel
            );
            if (GUILayout.Button("Get Support"))
            {
                Application.OpenURL("https://discord.gg/xpcfCyaycx");
            }

            GUILayout.Space(15); // Section break

            // --- REVIEW SECTION ---
            GUILayout.Label(
                "Enjoying this asset? A review helps us keep improving and means a lot to us. Thank you!",
                EditorStyles.wordWrappedLabel
            );
            if (GUILayout.Button("Leave a Review"))
            {
                Application.OpenURL("https://assetstore.unity.com/packages/slug/319018#reviews");
            }

            GUILayout.Space(15); // Section break

            // --- RESOURCES SECTION ---
            GUILayout.Label(
                "Quick links to help you get started:",
                EditorStyles.wordWrappedLabel
            );
            if (GUILayout.Button("What's New"))
            {
                Application.OpenURL("https://hanijahan.com/products/unity/free-pack/#what-s-new-changelog");
            }

            if (GUILayout.Button("Documentation"))
            {
                Application.OpenURL("https://hanijahan.com/products/unity/free-pack/#documentation");
            }

            GUILayout.Space(15); // Extra space above the footer

            GUILayout.Label(
                "Join our Discord! Show off what you're making, and connect with other creators",
                EditorStyles.wordWrappedLabel
            );
            if (GUILayout.Button("Join Discord"))
            {
                Application.OpenURL("https://discord.gg/xpcfCyaycx");
            }

            GUILayout.FlexibleSpace();
            GUILayout.Label("Made by Hani Jahan Design", EditorStyles.centeredGreyMiniLabel);

            GUILayout.EndVertical();

            GUILayout.Space(10); // Right padding
            GUILayout.EndHorizontal();

            GUILayout.Space(10); // Bottom padding
            GUILayout.EndVertical();
        }
    }
}