using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[System.Serializable]
public class csvInit : MonoBehaviour
{
    public Inventory playerbag; 
    csvController csv;
    // Start is called before the first frame update
    List<Item> csvInitItem = new List<Item>();
    private void Awake()
    {
        csv=csvController.GetInstance();
        refresh();
    }
    void refresh()
    {
        csv.loadFile(Application.dataPath + "//Resources", "slime.csv");
        for (int i = 1; i < csv.arrayData.Count; i++)
        {
            Item newItem = ScriptableObject.CreateInstance<Item>();
            newItem.name = csv.getString(i, 0);
            newItem.itemNumber = csv.getInt(i, 1);
            newItem.Genes = csv.getString(i, 2);
            Debug.Log(newItem.name);
            WWW www = new WWW(Application.dataPath + "//Resources//"+csv.getString(i, 3));
            newItem.itemImage=Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0.5f, 0.5f));
            playerbag.itemList.Add(newItem);
            Debug.Log("initSucess");
        }
       /* AssetDatabase.CreateAsset(playerbag, "Assets//slime.asset");

        BuildPipeline.BuildAssetBundle(null, new[]
            {
                AssetDatabase.LoadAssetAtPath("Assets/slime.asset", typeof(Item))
            },
            Application.streamingAssetsPath + "/Config.assetbundle",
            BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets | BuildAssetBundleOptions.UncompressedAssetBundle,
            BuildTarget.StandaloneWindows64);*/
    }
}
