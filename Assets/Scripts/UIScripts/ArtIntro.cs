using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtIntro : MonoBehaviour
{
    public float animationIntroTimer=10f;
    private float Timer=0f;
    private bool done=false;
    void Update()
    {
        Timer+=Time.deltaTime;
        if ((Timer>animationIntroTimer) && (done==false))
        {
            ArtSceneManager.Instance.LoadSceneByNumber(1);
            done=true;
        }
    }
}
