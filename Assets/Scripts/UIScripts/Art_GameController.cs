using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//public enum GameState {Playing,Convesation,Pause,Dead,Scene};
public class Art_GameController : MonoBehaviour
{
    public static Art_GameController Instance {get;private set;}
    public GameObject Player;
    private Transform SpawnPoint;
    public  int status = 1; 
    private bool inventoryStatus=false;
    private bool MapStatus=false;
    private Transform LastCheckpoint;
    public GameObject UI_pauseMenu;
    public GameObject UI_Inventory;
    public GameObject UI_Map;
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
        SpawnPoint=transform.GetChild(0).transform;
        LastCheckpoint=SpawnPoint;
        Player = Instantiate(Player,SpawnPoint.position,SpawnPoint.rotation); 
    }
    public void LateUpdate()
    {
        if ((Input.GetKeyUp(KeyCode.I)) && (!inventoryStatus))
        {
            BTN_enabledInventory();
        }
        else if ((Input.GetKeyUp(KeyCode.I)) && (inventoryStatus))
        {
            BTN_disableInventory();
        }
        if ((Input.GetKeyUp(KeyCode.M)) && (!MapStatus))
        {
            BTN_enableMap();
        }
        else if ((Input.GetKeyUp(KeyCode.M)) && (MapStatus))
        {
            BTN_disableMap();
        }
    }
    public void BTN_Retry()
    { 
        StartCoroutine(ReSpawn());
    }
    private IEnumerator ReSpawn()
    {
        ArtSceneManager.Instance.fade=true;
        yield return new WaitForSeconds(2);

        Player.transform.position=LastCheckpoint.position;
        Player.transform.rotation=LastCheckpoint.rotation;
        Art_UIHealthSystem.Instance.RespawnLife();
        status =1;
        Player.SetActive(true);

        ArtSceneManager.Instance.fade=false;
        yield return new WaitForSeconds(2);
        StopCoroutine(ReSpawn());
    }
    public void SetResPawnPoint(Transform checkPoint)
    {
        LastCheckpoint = checkPoint;
    }
    public void BTN_enabledInventory()
    {
        UI_Inventory.SetActive(true);
        inventoryStatus=true;
        Time.timeScale=0;
    }
    public void BTN_disableInventory()
    {
        UI_Inventory.SetActive(false);
        inventoryStatus=false;
        Time.timeScale=1;
    }
    public void BTN_enablePauseMenu()
    {
        UI_pauseMenu.SetActive(true);
        Time.timeScale=0;
    }
    public void BTN_disablePauseMenu()
    {
        UI_pauseMenu.SetActive(false);
        Time.timeScale=1;
    }
    public void BTN_enableMap()
    {
        UI_Map.SetActive(true);
        MapStatus=true;
        Time.timeScale=0;
    }
    public void BTN_disableMap()
    {
        UI_Map.SetActive(false);
        MapStatus=false;
        Time.timeScale=1;
    }
    void OnDrawGizmos()
	{
        Gizmos.color = Color.cyan;
		Gizmos.DrawWireSphere (transform.GetChild(0).transform.position, 2f);
	}
    #region PauseMenu

    #endregion

}
