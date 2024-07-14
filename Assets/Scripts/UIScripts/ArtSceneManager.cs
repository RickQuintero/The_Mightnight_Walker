using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ArtSceneManager : MonoBehaviour
{
    public static ArtSceneManager Instance {get;private set;}
    public GameObject Loading_UI;
    public Slider Loading_slider;
    public Text Loading_text;
    public bool fadingSystem=true;
    public bool fade=false;
    public float alpha = 1;
    public float fadeTime = 0.3f;
    private void Awake ()
    {
        
        if (Instance==null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Loading_UI.SetActive(false);
    }
    IEnumerator LoadAsunchronously (int lvload)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(lvload);
        while (!operation.isDone)
        {
            float progresstext = Mathf.Round(operation.progress *100);
            Loading_text.text = progresstext.ToString() + "%";
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            Loading_slider.value = progress;
            yield return null;  
         }
         if (operation.isDone)
         {  
             StopAllCoroutines();
             if (fadingSystem)
             {
                 alpha=1f;
                 fade=false;
                 Loading_UI.SetActive(false);
             }
             
         }
    }
    public void RestartLevel()
    {
        LoadSceneByNumber(SceneManager.GetActiveScene().buildIndex);
    }
    public void NextLevel()
    {
        LoadSceneByNumber(SceneManager.GetActiveScene().buildIndex+1);
    }
    public void LoadSceneByNumber(int lvload)
    {
        if (fadingSystem)
        {
            Loading_UI.SetActive(true);
        }
        StartCoroutine(LoadAsunchronously(lvload));
    }
    private void OnGUI()
    {
        
            GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
            Texture2D tex;
            tex = new Texture2D(1, 1);
            tex.SetPixel(0, 0, Color.black);
            tex.Apply();
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), tex);
            if (fade)
            {
                if(alpha<1) 
                {
                    alpha = Mathf.Lerp(alpha, 1.1f, fadeTime * Time.deltaTime);
                }
            }
            else
            {
                if(alpha>0) 
                {
                    alpha = Mathf.Lerp(alpha, -0.1f, fadeTime * Time.deltaTime);
                }
                
            }
        

    }
}
