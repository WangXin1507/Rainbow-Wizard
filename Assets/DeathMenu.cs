using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : UIElement
{
    public List<GameObject> hideWhenBehold;
    public List<Star> stars;
    public string menuSceneName;
    public List<int> starThresholds;

    bool beholding;

    public override void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>(); 
        Hide();
    }

    private void Update()
    {
        //if (Input.anyKeyDown)
        //{
        //    Debug.Log("test");
        //}
        //if (beholding && Input.anyKeyDown)
        //{
        //    UnBehold();
        //}
    }

    void OnEnable()
    {
        Player.PlayerHealth.OnPlayerDeath.AddListener(OnPlayerDeath);
        Player.PlayerInput.OnEscapePressed.AddListener(TryUnBehold);
    }

    private void OnDisable()
    {
        Player.PlayerHealth.OnPlayerDeath.RemoveListener(OnPlayerDeath);
        Player.PlayerInput.OnEscapePressed.RemoveListener(TryUnBehold);
    }

    void OnPlayerDeath(float f)
    {
        Show();
        float points = GameManager.Instance.points;
        int c = 0;
        foreach(var t in starThresholds)
        {
            if (points >= t)
            {
                c++;
            }
        }
        SpawnStars(c).Forget();
    }

    void TryUnBehold()
    {
        if (beholding)
        {
            UnBehold();
        }
    }

    public void Behold()
    {
        if (beholding)
        {
            return;
        }
        beholding = true;
        Hide();
        if (hideWhenBehold != null)
        {
            foreach(GameObject go in hideWhenBehold)
            {
                if (go != null)
                {
                    go.SetActive(false);
                }
            }
        }
    }

    public async UniTask SpawnStars(int count)
    {
        int spawnCount = Mathf.Min(count, stars.Count);
        for (int i = 0; i < spawnCount; i++)
        {
            await stars[i].Init();
        }
    }

    public void UnBehold()
    {
        beholding = false;
        Show();
        if (hideWhenBehold != null)
        {
            foreach (GameObject go in hideWhenBehold)
            {
                if (go != null)
                {
                    go.SetActive(true);
                }
            }
        }
    }

    public void TryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        SceneManager.LoadScene(menuSceneName);
    }
}
