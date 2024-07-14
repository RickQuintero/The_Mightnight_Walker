using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtMainMenu : MonoBehaviour
{
    public GameObject MenuSettings;
    public GameObject ExitMenu;
    public GameObject MenuAboutUS;
    public void Start()
    {
        AudioManager.Instance.PlaySong("NowhereTheme");
    }
    public void BTN_NewGame()
    {
        PlaySound();
        ArtSceneManager.Instance.LoadSceneByNumber(2);
    }
    public void BTN_LoadGame()
    {
        //We will see
        PlaySound();
    }
    //Configuration.......
    public void BTN_Config()
    {
        PlaySound();
        MenuSettings.SetActive(true);
    }
    public void BTN_ExitConfig()
    {
        PlaySound();
        MenuSettings.SetActive(false);
    }
    public void BTN_Facebook()
    {
        PlaySound();
        LoadURL("https://www.facebook.com/ArtSystemCompanyRick/");
    }
    public void BTN_Patreon()
    {
        PlaySound();
        LoadURL("https://www.patreon.com/artsystemcompany");
    }
    public void BTN_ABOUTUS()
    {
        PlaySound();
        MenuAboutUS.SetActive(true);
    }
    public void BTN_ABOUTUS_Exit()
    {
        PlaySound();
        MenuAboutUS.SetActive(false);
    }
    //------------EXIT OPTIONS
    public void BTN_EXIT()
    {
        PlaySound();
        ExitMenu.SetActive(true);
    }
    public void BTN_EXIT_TRUE()
    {
        PlaySound();
        Application.Quit();
    }
    public void BTN_EXIT_FALSE()
    {
        PlaySound();
        ExitMenu.SetActive(false);
    }
    private void LoadURL(string url)
	{
		Application.OpenURL(url);
	}
    void PlaySound()
    {
        AudioManager.Instance.PlayEffect("ButtonKey");
    }
}
