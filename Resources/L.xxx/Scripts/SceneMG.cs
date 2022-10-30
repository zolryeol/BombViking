using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// æ¿∏≈¥œ¿˙¿Ã¥Ÿ ΩÃ±€≈Ê¿∏∑Œ æµ ∞Õ¿Ã¥Ÿ.
/// </summary>
public class SceneMG : MonoBehaviour
{
    private static SceneMG instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        if (SceneManager.GetActiveScene().name == "00.Logo") // «ˆ¿Á æ¿¿Ã ∑Œ∞Ìæ¿¿Ã∏È ∆‰¿ÃµÂæ∆øÙ«œ∞Ì ≥ÿΩ∫∆Ææ¿¿∏∑Œ
        {
            this.gameObject.GetComponent<FadeInOut>().FadeIn();

            Invoke("FadeOut", 1.5f);

            Invoke("LoadTitle", 3.5f);

        }

        Screen.SetResolution(1920, 1080, true);

    }

    void FadeOut()
    {
        this.gameObject.GetComponent<FadeInOut>().FadeOut();
    }

    public static SceneMG Instance
    {
        get { return instance; }
    }

    public void LoadLogo()
    {
        Screen.SetResolution(1920, 1080, true);
        SceneManager.LoadScene("00.Logo");

    }

    public void LoadGame()
    {
        Screen.SetResolution(1920, 1080, true);
        SceneManager.LoadScene("02.MainGame");
        Time.timeScale = 1;

    }

    public void LoadTitle()
    {
        Screen.SetResolution(1920, 1080, true);
        SceneManager.LoadScene("01.Title");
        Time.timeScale = 1;

    }

    public void LoadGameOver()
    {
        SceneManager.LoadScene("03.GameOver");
        Screen.SetResolution(1920, 1080, true);

    }

    //     private void Update()
    //     {
    //         if (SceneManager.GetActiveScene().buildIndex == 3)
    //         {
    //             StartCoroutine(Wait());
    //         }
    //     }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.5f);
        LoadTitle();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Alpha0))
        {
            NextScene();
        }
    }

    void NextScene()
    {
        int _thisScene = SceneManager.GetActiveScene().buildIndex;

        if (3 < _thisScene + 1)
        {
            _thisScene = 0;
        }
        SceneManager.LoadScene(_thisScene + 1);
    }
    void PreviousScene()
    {

    }

}
