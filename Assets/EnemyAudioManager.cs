using UnityEngine;

public class EnemyAudioManager : MonoBehaviour
{
    [SerializeField] private float minPitch = 0.85f;
    [SerializeField] private float maxPitch = 1.0f;

    [SerializeField] private AudioSource damageTaken;
    [SerializeField] private AudioSource damageIgnored;
    [SerializeField] private AudioSource death;
    [SerializeField] private AudioSource attackWindup;

    private float pitch = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeAudioManager(float enemyWeight)
    {
        pitch = Mathf.Lerp(minPitch, maxPitch, enemyWeight);
    }

    public void PlayDamageTaken()
    {
        damageTaken.pitch = pitch;
        damageTaken.Play();
    }

    public void PlayDamageIgnored()
    {
        damageIgnored.pitch = pitch;
        damageIgnored.Play();
    }

    public void PlayDeath()
    {
        death.pitch = pitch;
        death.Play();
    }

    public void PlayAttackWindup()
    {
        attackWindup.pitch = pitch;
        attackWindup.Play();
    }

    public void StopAttackWindup()
    {
        attackWindup.Stop();
    }
}
