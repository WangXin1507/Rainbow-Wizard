using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class GameAudioManager : MonoBehaviour
{
    public static GameAudioManager Instance;

    [SerializeField] private AudioSource pointEarned;
    [SerializeField] private AudioSource victoryHorn;
    [SerializeField] private AudioSource shoot;
    [SerializeField] private AudioSource noAmmoShoot;
    [SerializeField] private AudioSource damageTaken;
    [SerializeField] private AudioSource death;
    [SerializeField] private AudioSource bucketHit;
    [SerializeField] private AudioSource bucketPour;
    [SerializeField] private AudioSource paintSwitch;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayPointEarned()
    {
        pointEarned.pitch = Mathf.Lerp(0.9f, 1.1f, Random.value);
        pointEarned.PlayOneShot(pointEarned.clip);
    }

    public void PlayVictoryHorn()
    {
        victoryHorn.Play();
    }

    public void PlayShoot()
    {
        shoot.pitch = Mathf.Lerp(0.9f, 1.1f, Random.value);
        shoot.PlayOneShot(shoot.clip);
    }

    public void PlayNoAmmoShoot()
    {
        noAmmoShoot.pitch = Mathf.Lerp(0.9f, 1.1f, Random.value);
        noAmmoShoot.PlayOneShot(noAmmoShoot.clip);
    }

    public void PlayDamageTaken()
    {
        damageTaken.Play();
    }

    public void PlayDeath()
    {
        death.Play();
    }

    public void PlayBucketHit(float pitch)
    {
        bucketHit.pitch = pitch;
        bucketHit.Play();
    }

    public void PlayBucketPour()
    {
        bucketPour.Play();
    }

    public void PlayPaintSwitch()
    {
        paintSwitch.pitch = Mathf.Lerp(0.9f, 1.1f, Random.value);
        paintSwitch.PlayOneShot(paintSwitch.clip);
    }
}
