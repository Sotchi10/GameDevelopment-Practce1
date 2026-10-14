using UnityEditor;

namespace HaniJahanDesign.FreePack
{
    [InitializeOnLoad]
    public class MyAssetWelcomeAuto
    {
        static MyAssetWelcomeAuto()
        {
            if (!EditorPrefs.HasKey("MyAssetWelcomeShown"))
            {
                MyAssetWelcome.ShowWindow();
                EditorPrefs.SetBool("MyAssetWelcomeShown", true);
            }
        }
    }
}
