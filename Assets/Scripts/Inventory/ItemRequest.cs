using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRequest : MonoBehaviour
{
    public GameObject MeshRequest;
    public GameObject MeshDone;
    public string itemNeededName;
    public bool Isdone=false;
    public bool status=false;
    public Dialogue dialogueDone;
    void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Player") && (!Isdone))
		{
            status = Art_InventoryManager.Instance.CompareItem(itemNeededName);
            if (status==true)
            {
                Isdone=true;
                DialogManager.Instance.EnabledJohn_UI(true);
                DialogManager.Instance.StartDialog(dialogueDone);
                MeshRequest.SetActive(false);
                MeshDone.SetActive(true);
            }
        }
    }
    void OnTriggerStay(Collider other)
    {
        if ((other.tag == "Player") && (Isdone))
        {
            if (Input.GetKey(KeyCode.E))
            {
                DialogManager.Instance.DisplayNextSentence();
            }
        }
    }
}
