using UnityEngine;
using UnityEngine.UI;

public class ZoneInfo : MonoBehaviour
{
    public string ZoneName ="Zone Name";
    public string ZoneSong = "";

    void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            if (DialogManager.Instance.GetZone()==ZoneName)return;
            AudioManager.Instance.ChangeSong(ZoneSong);
            DialogManager.Instance.EnabledZone(ZoneName);
        }
    }
}
