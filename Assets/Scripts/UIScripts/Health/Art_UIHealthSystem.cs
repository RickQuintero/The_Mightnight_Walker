using UnityEngine;
using UnityEngine.UI;

public class Art_UIHealthSystem : MonoBehaviour
{
    public static Art_UIHealthSystem Instance {get;private set;}
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
    public float Health=100f;
    public float springForce=100f;
    public float batery=100f;

    public Slider HealthSlider;
    public Slider SprintSlider;
    public Slider BaterySlider;

    private float TimerForEffect=0f;
    private float TimerDestiny=5f;


    public GameObject DeathMesh;
    private GameObject CurrentDeathMesh;
    public GameObject DeadUI;
    public GameObject DeathParticle;
    public AudioSource DeathSound;
    private bool IsDying=false;
    private float TimerDeath=0f;
    private float TimerForDeath=5f;
    public bool IsOnDarkWorld=false;

    public Transform player;
    public ParticleSystem Blood;
    public AudioSource HurtSound;

    void Start()
    {
        DeadUI.SetActive(false);
        player = GameObject.FindGameObjectWithTag ("Player").transform;
    }
    public void HurtHealth(float damage)
    {
        if (!IsOnDarkWorld)return;
        Health -= damage;
        TimerForEffect +=Time.deltaTime;
        if (TimerForEffect>TimerDestiny)
        {
            EffectsON();
            TimerForEffect=0;
        }
    }
    public void HurtHealthPunch(float damage)
    {
        Health -= damage;
        EffectsON();
    }
    
    public void EffectsON()
    {
        Blood.Play();
        HurtSound.Play();
    }
    public void HurtSpring(float damage)
    {
        springForce -= damage;
        if (springForce<0f)
        {
            springForce = -10f;
        }
    }
    public void RespawnLife()
    {
        Destroy(CurrentDeathMesh);
        Health=100f;
        IsDying=false;
        TimerDeath=0f;
        DeadUI.SetActive(false);
    }
    void CheckLife()
    {
        if (Health<0f)
        {
            Health= (-1f);
            if (!IsDying)
            {
                IsDying=true;
                Art_GameController.Instance.status=4;
                CurrentDeathMesh = Instantiate(DeathMesh,transform.position,Quaternion.identity);  
                Instantiate(DeathParticle,transform.position,Quaternion.identity);
                DeathSound.Play();
            }
            TimerDeath +=Time.deltaTime;
            if (TimerDeath>TimerForDeath)
            {
                DeadUI.SetActive(true);
            }

        }
        else 
        {
            if (CurrentDeathMesh!=null)
            {
                Destroy(CurrentDeathMesh);
            }
        }
    }
    public void HurtBatery(float damage)
    {
        batery -= damage;
        if (batery<0f)
        {
            batery = 0f;
        }
    }
    public void RefreshSliders()
    {
        float temphealth = Health / 100;
        float tempSprint = springForce/100;
        float tempBatery = batery/100;

        HealthSlider.value = temphealth;
        SprintSlider.value = tempSprint;
        BaterySlider.value = tempBatery;
    }
    void Ubicated()
    {
        if (player==null)return;
        transform.position = player.position;
    }
    public void LateUpdate()
    {
        Ubicated();
        RefreshSliders();
        CheckLife();
    }
    public void HealhingHealth(float value)
    {
        Health += value;
        if (Health>=100f)
        {
            Health = 100f;
        }
    }
    public void HealhingSprint(float value)
    {
        springForce += value ;
        if (springForce>=100f)
        {
            springForce = 100f;
        }
    }
    public void HealingBatery(float value)
    {
        batery += value;
        if (batery>=100f)
        {
            batery = 100f;
        }
    }
}
