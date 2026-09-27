using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : UIElement
{
    public List<GameObject> hideWhenBehold;
    public string menuSceneName;

    bool beholding;

    public override void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>(); 
        Hide();
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            Debug.Log("test");
        }
        if (beholding && Input.anyKeyDown)
        {
            UnBehold();
        }
    }

    void OnEnable()
    {
        Player.PlayerHealth.OnPlayerDeath.AddListener(OnPlayerDeath);
    }

    private void OnDisable()
    {
        Player.PlayerHealth.OnPlayerDeath.RemoveListener(OnPlayerDeath);
    }

    void OnPlayerDeath(float f)
    {
        Show();
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
