using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCatcher : MonoBehaviour
{
    public Item item;
    public GameObject ItemMesh;
    private bool Isdone=false;

    void OnTriggerStay(Collider other) 
	{
		if ((other.tag == "Player") && (!Isdone))
		{
			if (Input.GetKey(KeyCode.E))
			{
                Isdone=true;
                ItemMesh.SetActive(false);
                Art_InventoryManager.Instance.AddItem(item);
            }
        }
        
    }
    void OnTriggerExit(Collider other)
	{	
		if ((other.tag == "Player") && (Isdone))
		{
            Destroy(gameObject);
        }
    }
}
