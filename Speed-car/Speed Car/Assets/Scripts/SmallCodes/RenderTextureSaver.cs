using UnityEngine;
using System.IO;

public class RenderTextureSaver : MonoBehaviour
{
    public RenderTexture renderTexture;

    public void SaveToPNG()
    {
        RenderTexture currentRT = RenderTexture.active;
        RenderTexture.active = renderTexture;

        Texture2D tex = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
        tex.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        tex.Apply();

        byte[] bytes = tex.EncodeToPNG();
        string path = Path.Combine(Application.dataPath, "SavedCityImage.png");
        File.WriteAllBytes(path, bytes);
        Debug.Log("PNG сохранено: " + path);

        RenderTexture.active = currentRT;
    }
}
