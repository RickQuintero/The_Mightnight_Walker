using UnityEngine;
using UnityEngine.UI;

public class Art_InventoryManager : MonoBehaviour
{   
    public static Art_InventoryManager Instance {get;private set;}
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
        //DontDestroyOnLoad(gameObject);
    }
    public int CurrentFlashlight=0;
    public int CurrentSelection=0;
    public bool[] N_flashlights;
    public RawImage[] N_SlotFlashlighs;
    public RawImage[] N_SlotItems;
    public Texture EmptyImg;
    public Item[] CurrentFlashlighs;
    public Item[] CurrentItems;
    private int itemIndex=0;
    public Text Txt_NameItem;
    public Text Txt_DescriptionItem;
    void Start()
    {
        N_flashlights = new bool[4];
        CurrentFlashlighs = new Item[4];
        CurrentItems = new Item[8];
        for (int i=0;i<N_flashlights.Length;i++)
        {
            N_flashlights[i]=false;
            N_SlotFlashlighs[i].texture=EmptyImg;
        }
    }
    public void AddItem(Item itemToAdd)
    {
        if (itemToAdd.IsFlashlight)
        {
            N_flashlights[itemToAdd.FlashlightNumber]=true;
            N_SlotFlashlighs[itemToAdd.FlashlightNumber].texture = itemToAdd.ItemImage;
            CurrentFlashlighs[itemToAdd.FlashlightNumber] = itemToAdd;
        }
        else
        {
            N_SlotItems[itemIndex].texture = itemToAdd.ItemImage;
            CurrentItems[itemIndex]=itemToAdd;
            itemIndex+=1;
        }
    }
    public void BTN_Equip()
    {
        if (N_flashlights[CurrentSelection]==true)
        {
            CurrentFlashlight=CurrentSelection;
        }
    }
    public void BTN_selectinoFlash(int selection)
    {
        CurrentSelection=selection;
        if (N_flashlights[CurrentSelection]==true)
        {
            RefreshData(CurrentFlashlighs[CurrentSelection]);
        }
    }
    public void BTN_selectionItem(int selection)
    {
        RefreshData(CurrentItems[selection]);
    }
    private void RefreshData(Item item)
    {
        Txt_NameItem.text = item.ItemName;
        Txt_DescriptionItem.text = item.Description;
    }
    public bool CompareItem(string ItemToCompareName)
    {
        foreach (Item item in CurrentItems)
        {
            if (item.ItemName!=null)
            {
                if (item.ItemName==ItemToCompareName)
                {
                return true;
                }
            }
            
        }
        return false;
    }
}
