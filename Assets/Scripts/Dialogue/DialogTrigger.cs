 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour {

	public Dialogue dialogue;
	public Transform CameraUbication;
	public bool Started=false;
	public bool isActioner=false;
	public float TimeForNextSentence=0.2f;
	public float Timer=0f;
	
	void FixedUpdate()
	{
		if ((Started) && (Input.GetKey(KeyCode.E)) && (Timer>TimeForNextSentence))
		{
			Timer=0f;
			DialogManager.Instance.DisplayNextSentence();
		}
	}
	void OnTriggerEnter(Collider other)
	{	
		if (other.tag == "Player")
		{
			if (!isActioner)
			{
				DialogManager.Instance.EnabledInformator(true);
				DialogManager.Instance.EnabledJohn_UI(false);
				DialogManager.Instance.SetPosition(CameraUbication);
			}
			else
			{
				DialogManager.Instance.EnabledJohn_UI(true);
				DialogManager.Instance.EnabledActioner(true);
			}
		}
	}
	void OnTriggerExit(Collider other)
	{	
		if (other.tag == "Player")
		{
			if (!isActioner)
			{
				DialogManager.Instance.EnabledInformator(false);
				Started=false;
				DialogManager.Instance.EndDialog();
			}
			else
			{
				DialogManager.Instance.EnabledActioner(false);
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
		if ((other.tag == "Player") && (!Started) && (Input.GetKey(KeyCode.E)))
		{
			Timer=0f;
			Started=true;
			if (isActioner)
			{
				dialogue.ObjectName = "John";
				DialogManager.Instance.EnabledJohn_UI(true);
			}
			DialogManager.Instance.StartDialog(dialogue);
		}
	}

}
