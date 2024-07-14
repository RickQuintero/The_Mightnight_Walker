using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogManager : MonoBehaviour {
	
	public static DialogManager Instance {get;private set;}
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

	public Text Txt_DialogueName;
	public Text Txt_DialogueSencence;
	public GameObject UI_informar;
	public GameObject UI_Actioner;
	public GameObject UI_EnterImg;
	public GameObject UI_JonImage;
	public GameObject UI_NPCimage;
	public RawImage UI_NPCRawimage;
	public Animator ZoneInfoAnim;
	public Text ZoneInfoTXT;

	public GameObject UI_DialogueBox;
	private bool[] JohnTemp; // MAX NPC DIALOGUES
	private string NPCnameTemp;
	private int currentSpeaker=0;

	private string Name;
	private Queue <string> sentences;

	void Start () 
	{
		sentences = new Queue<string> ();
		JohnTemp = new bool[32];
		for (int i=0;i<JohnTemp.Length;i++)
		{
			JohnTemp[i]=false;
		}
	}
	#region Enabled Zone

	public void EnabledInformator(bool status)
	{
		UI_informar.SetActive(status);
	}
	public void EnabledActioner(bool status)
	{
		UI_Actioner.SetActive(status);
		UI_JonImage.SetActive(status);
	}
	public void EnabledJohn_UI(bool status)
	{
		UI_JonImage.SetActive(status);
	}
	public void EnabledEnter(bool status)
	{
		UI_EnterImg.SetActive(status);
	}
	public void EnabledZone(string name)
	{
		ZoneInfoTXT.text = name;
		ZoneInfoAnim.SetTrigger("ShowZone");
	}


	#endregion
	
	public string GetZone()
	{
		return ZoneInfoTXT.text;
	}
	public void SetPosition(Transform InfoPos)
	{
		transform.position = InfoPos.position;
		transform.rotation = InfoPos.rotation;
	}
	
	public void StartDialog(Dialogue dialogue)
	{
		Time.timeScale=0.1f;
		UI_DialogueBox.SetActive(true);
		Txt_DialogueName.text = dialogue.ObjectName;
		Art_GameController.Instance.status=2;
		sentences.Clear ();
		foreach (string sentence in dialogue.sentences) 
			{
			sentences.Enqueue (sentence);
			}
		DisplayNextSentence ();
	}
	public void StartDialogWithNPC(DialogueWithNPC dialogue)
	{
		Time.timeScale=0.1f;
		UI_NPCRawimage.texture = dialogue.ObjectImage;
		UI_DialogueBox.SetActive(true);
		NPCnameTemp=dialogue.ObjectName;
		if (!dialogue.John[currentSpeaker])
		{
			Txt_DialogueName.text = NPCnameTemp;
		}
		else
		{
			Txt_DialogueName.text = "John";
		}
		Art_GameController.Instance.status=2;
		sentences.Clear ();
		foreach (string sentence in dialogue.sentences) 
			{
			sentences.Enqueue (sentence);
			}

		for (int i=0;i<dialogue.John.Length;i++)
		{
			if(dialogue.John[i]==true)
			{
				JohnTemp[i]=true;
			}
			else
			{
				JohnTemp[i]=false;
			}
		}
		currentSpeaker=0;
		DisplayNextSentenceNPC();
	}
	public void DisplayNextSentenceNPC()
	{	
		if (!JohnTemp[currentSpeaker])
		{
			Txt_DialogueName.text = NPCnameTemp;
			UI_NPCimage.SetActive(true);
			UI_JonImage.SetActive(false);
		}
		else
		{
			Txt_DialogueName.text = "John";
			UI_JonImage.SetActive(true);
			UI_NPCimage.SetActive(false);
		}
		currentSpeaker+=1;
		if (sentences.Count == 0) 
		{
			EndDialog ();
			return;
		}
		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}
	public void DisplayNextSentence ()
	{	
		if (sentences.Count == 0) 
		{
			EndDialog ();
			return;
		}
		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}
	IEnumerator TypeSentence(string sentence)
	{
		
		Txt_DialogueSencence.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			Txt_DialogueSencence.text += letter;
			AudioManager.Instance.PlayEffect("Effect_LetterPut");
			yield return null;
		}
	}
	public void EndDialog()
	{
		Time.timeScale=1f;
		StopAllCoroutines ();
		sentences.Clear ();
		UI_DialogueBox.SetActive (false);
		UI_NPCimage.SetActive(false);
		EnabledInformator(false);
		Art_GameController.Instance.status=1;
		currentSpeaker=0;
	}







}
