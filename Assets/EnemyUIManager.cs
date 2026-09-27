using UnityEngine;

public class EnemyUIManager : MonoBehaviour
{
    [SerializeField] private HealthResourceBar topUIBar;
    [SerializeField] private HealthResourceBar middleUIBar;
    [SerializeField] private HealthResourceBar bottomUIBar;

    private EnemyHealth enemyHealth;

    [SerializeField] private PaintColor enemyColor = PaintColor.NONE;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        enemyHealth = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeUIManager(PaintColor color)
    {
        enemyColor = color;
        OnDamageUpdate();
    }

    public void OnDamageUpdate()
    {
        HealthResourceBar redBar = GetColorHealthBar(PaintColor.RED);
        redBar.UpdateColor(PaintColorUtil.GetPaintColorColor(PaintColor.RED));
        redBar.UpdateValue(Mathf.Lerp(0f, 1f, enemyHealth.RedHealth / enemyHealth.MaxHealth), enemyHealth.IsDamaged);
        HealthResourceBar yellowBar = GetColorHealthBar(PaintColor.YELLOW);
        yellowBar.UpdateColor(PaintColorUtil.GetPaintColorColor(PaintColor.YELLOW));
        yellowBar.UpdateValue(Mathf.Lerp(0f, 1f, enemyHealth.YellowHealth / enemyHealth.MaxHealth), enemyHealth.IsDamaged);
        HealthResourceBar blueBar = GetColorHealthBar(PaintColor.BLUE);
        blueBar.UpdateColor(PaintColorUtil.GetPaintColorColor(PaintColor.BLUE));
        blueBar.UpdateValue(Mathf.Lerp(0f, 1f, enemyHealth.BlueHealth / enemyHealth.MaxHealth), enemyHealth.IsDamaged);
    }

    private HealthResourceBar GetColorHealthBar(PaintColor color)
    {
        if (color == PaintColor.RED)
        {
            return enemyColor switch
            {
                PaintColor.RED => bottomUIBar,
                PaintColor.ORANGE => middleUIBar,
                PaintColor.YELLOW => topUIBar,
                PaintColor.GREEN => topUIBar,
                PaintColor.BLUE => topUIBar,
                PaintColor.PURPLE => middleUIBar,
                PaintColor.ALL => topUIBar,
                _ => topUIBar,
            };
        }

        else if (color == PaintColor.YELLOW)
        {
            return enemyColor switch
            {
                PaintColor.RED => topUIBar,
                PaintColor.ORANGE => bottomUIBar,
                PaintColor.YELLOW => bottomUIBar,
                PaintColor.GREEN => middleUIBar,
                PaintColor.BLUE => middleUIBar,
                PaintColor.PURPLE => topUIBar,
                PaintColor.ALL => middleUIBar,
                _ => middleUIBar,
            };
        }

        else if (color == PaintColor.BLUE)
        {
            return enemyColor switch
            {
                PaintColor.RED => middleUIBar,
                PaintColor.ORANGE => topUIBar,
                PaintColor.YELLOW => middleUIBar,
                PaintColor.GREEN => bottomUIBar,
                PaintColor.BLUE => bottomUIBar,
                PaintColor.PURPLE => bottomUIBar,
                PaintColor.ALL => bottomUIBar,
                _ => bottomUIBar,
            };
        }

        else
        {
            return null;
        }
    }
}
