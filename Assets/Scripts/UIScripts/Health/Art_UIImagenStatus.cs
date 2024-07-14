using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Art_UIImagenStatus : MonoBehaviour
{
    public RawImage UIToChange;
    public float Life =100f;
    public Texture[] LifeStatus;
    void LateUpdate()
    {
        Life = Art_UIHealthSystem.Instance.Health;
        if (Life>80f)
        {
        UIToChange.enabled = false; 
        }
        else
        {
        UIToChange.enabled = true; 
        }

        if ((Life<80f) && (Life>60))
        {
            UIToChange.texture = LifeStatus[0]; 
        }
        if ((Life<80f) && (Life>60))
        {
            UIToChange.texture = LifeStatus[1]; 
        }
        if ((Life<60f) && (Life>40))
        {
            UIToChange.texture = LifeStatus[2]; 
        }
        if ((Life<40f) && (Life>20))
        {
            UIToChange.texture = LifeStatus[3]; 
        }
        if (Life<6f)
        {
            UIToChange.texture = LifeStatus[4]; 
        }

    }
}
