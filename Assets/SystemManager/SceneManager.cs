#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class CustomSceneManager : MonoBehaviour
{
#if UNITY_EDITOR
    [System.Serializable]
    public class SceneReference
    {
        public SceneAsset sceneAsset;  // Scene reference from the editor
    }

    // Scene references that you can assign in the editor
    public SceneReference mainMenuScene;
    public SceneReference sceneEvent;
    public SceneReference exploreScene;
    public SceneReference mgReelingScene;
    public SceneReference mgEndlessRunScene;
    public SceneReference mgBossFightScene;
    public SceneReference sellerScene;
    public SceneReference dayResultScene;
#endif

#if UNITY_EDITOR
    private string GetScenePath(SceneReference sceneReference)
    {
        return sceneReference.sceneAsset != null
            ? AssetDatabase.GetAssetPath(sceneReference.sceneAsset)
            : null;
    }
#endif

    private void LoadSceneByReference(SceneReference sceneReference)
    {
#if UNITY_EDITOR
        string scenePath = GetScenePath(sceneReference);
        if (!string.IsNullOrEmpty(scenePath))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(scenePath);
        }
        else
        {
            Debug.LogError("SceneAsset is null or not assigned!");
        }
#else
        Debug.LogError("Scene loading only works in the editor mode for this setup.");
#endif
    }

    // Specific scene load methods that call the generic LoadSceneByReference method
    public void LoadMainMenuScene() => LoadSceneByReference(mainMenuScene);
    public void LoadSceneEvent() => LoadSceneByReference(sceneEvent);
    public void Load2DExploreScene() => LoadSceneByReference(exploreScene);
    public void LoadMgReelingScene() => LoadSceneByReference(mgReelingScene);
    public void LoadMgEndlessRunScene() => LoadSceneByReference(mgEndlessRunScene);
    public void LoadMgBossFightScene() => LoadSceneByReference(mgBossFightScene);
    public void LoadSellerScene() => LoadSceneByReference(sellerScene);
    public void LoadDayResultScene() => LoadSceneByReference(dayResultScene);
}
