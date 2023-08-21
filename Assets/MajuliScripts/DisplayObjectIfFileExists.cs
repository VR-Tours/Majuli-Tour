using UnityEngine;
using System.IO;

public class DisplayObjectIfFileExists : MonoBehaviour
{
    public Material skyboxMaterial;
    public GameObject arrowLeft;
    public GameObject arrowRight;
    public GameObject arrowFront;
    public GameObject arrowBack;
    public string locName = "StartScreen";
    private string textureName = "";
    

    void Update()
    {
        
        Texture currentTexture=skyboxMaterial.GetTexture("_MainTex");
        // UnityEngine.Debug.Log("Current texture: " + currentTexture.name);
        int textureX = int.Parse(currentTexture.name.Split(',')[0]);
        int textureY = int.Parse(currentTexture.name.Split(',')[1]);
                

        string frontTextureName = textureName + textureX + "," + (textureY + 1);
        string backTextureName = textureName + textureX + "," + (textureY - 1);
        string leftTextureName = textureName + (textureX - 1) + "," + textureY;
        string rightTextureName = textureName + (textureX + 1) + "," + textureY;

        Texture frontTexture = Resources.Load<Texture>(locName + "/" + frontTextureName);
        Texture backTexture = Resources.Load<Texture>(locName + "/" + backTextureName);
        Texture leftTexture = Resources.Load<Texture>(locName + "/" + leftTextureName);
        Texture rightTexture = Resources.Load<Texture>(locName + "/" + rightTextureName);
        // Update the file path
        // filePath = "C:/path/to/your/new/file.txt";
    

        if (frontTexture)
        {
            arrowFront.SetActive(true); // Show the object
        }
        else{
            arrowFront.SetActive(false);
        }
        if (backTexture)
        {
            arrowBack.SetActive(true); // Show the object
        }
        else{
            arrowBack.SetActive(false);
        }
        if (leftTexture)
        {
            arrowLeft.SetActive(true); // Show the object
        }
        else{
            arrowLeft.SetActive(false);
        }
        if (rightTexture)
        {
            arrowRight.SetActive(true); // Show the object
        }
        else{
            arrowRight.SetActive(false);
        }

        
    }
}