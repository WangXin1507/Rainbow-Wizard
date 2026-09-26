using TMPro;
using UnityEngine;

public class EnemyTestHealthUI : MonoBehaviour
{
    [SerializeField] private Enemy enemy;

    [SerializeField] private TextMeshProUGUI redHealth;
    [SerializeField] private TextMeshProUGUI yellowHealth;
    [SerializeField] private TextMeshProUGUI blueHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        redHealth.text = "" + enemy.enemyHealth.RedHealth;
        yellowHealth.text = "" + enemy.enemyHealth.YellowHealth;
        blueHealth.text = "" + enemy.enemyHealth.BlueHealth;
    }
}
