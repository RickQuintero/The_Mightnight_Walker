using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerNPC : MonoBehaviour
{
    public DialogueWithNPC dialogue;
	public bool Started=false;
	public bool SelfSpeaker=false;
	private float Timer=0f;
	public float TimeForNextSentence=0.2f;
	
	void FixedUpdate()
	{
		if ((Started) && (Input.GetKey(KeyCode.E)) && (Timer>TimeForNextSentence))
		{
			Timer=0f;
			DialogManager.Instance.DisplayNextSentenceNPC();
		}
	}
	void OnTriggerEnter(Collider other)
	{	
		if ((other.tag == "Player") && (!SelfSpeaker))
		{
			DialogManager.Instance.EnabledInformator(true);
		}
        if ((other.tag == "Player") && (!Started) && (SelfSpeaker))
		{
			Timer=0f;
			Started=true;
			DialogManager.Instance.StartDialogWithNPC(dialogue);
		}
	}
	void OnTriggerExit(Collider other)
	{	
		if (other.tag == "Player")
		{
			if (!SelfSpeaker)
			{
				DialogManager.Instance.EnabledInformator(false);
				Started=false;
				DialogManager.Instance.EndDialog();
			}
		}
	}
	void OnTriggerStay(Collider other) 
	{
		Timer += Time.deltaTime;
		if (Timer>5f)
 		{
			 Timer=0f;
		}
		if ((other.tag == "Player") && (!Started) && (!SelfSpeaker) && (Input.GetKey(KeyCode.E)))
		{
			Timer=0f;
			Started=true;
			DialogManager.Instance.StartDialogWithNPC(dialogue);
		}
		
	}
}
