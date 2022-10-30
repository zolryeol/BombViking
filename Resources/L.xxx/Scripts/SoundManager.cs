using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  사운드 매니저다 싱글톤으로 처리할 것이다.
/// </summary>
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource buttonCatch;
    public AudioSource buttonThrow;
    public AudioSource buttonHold;
    public AudioSource bgm;
    public AudioSource footSound;
    public AudioSource pressInputSound;
    public AudioSource[] gameOverSound = new AudioSource[4];
    public AudioSource dangerSound;
    public AudioSource boom;
    public AudioSource defuseBomb;
    public AudioSource defuseBombFinal;
    public AudioSource getButton;
    public AudioSource healSound;

    private void Awake()
    {
        buttonCatch = this.transform.Find("GrabButton").GetComponentInChildren<AudioSource>();
        buttonThrow = this.transform.Find("ThrowButton").GetComponentInChildren<AudioSource>();
        buttonHold = this.transform.Find("HoldButton").GetComponentInChildren<AudioSource>();
        bgm = this.transform.Find("BGM").GetComponentInChildren<AudioSource>();
        footSound = this.transform.Find("FootStep").GetComponentInChildren<AudioSource>();
        pressInputSound = this.transform.Find("PressButton").GetComponentInChildren<AudioSource>();

        gameOverSound[0] = this.transform.Find("GameOver1").GetComponentInChildren<AudioSource>();
        gameOverSound[1] = this.transform.Find("GameOver2").GetComponentInChildren<AudioSource>();
        gameOverSound[2] = this.transform.Find("GameOver3").GetComponentInChildren<AudioSource>();
        gameOverSound[3] = this.transform.Find("GameOver4").GetComponentInChildren<AudioSource>();
        dangerSound = this.transform.Find("Danger").GetComponentInChildren<AudioSource>();
        boom = this.transform.Find("Boom").GetComponentInChildren<AudioSource>();
        defuseBomb = this.transform.Find("DefuseBomb").GetComponentInChildren<AudioSource>();
        getButton = this.transform.Find("GetButton").GetComponentInChildren<AudioSource>();
        defuseBombFinal = this.transform.Find("DefuseBombFinal").GetComponentInChildren<AudioSource>();
        healSound = this.transform.Find("HealSound").GetComponent<AudioSource>();

        if (instance == null)
        {
            instance = this;
        }
        return;
    }

    public static SoundManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    public IEnumerator PlayDefuseButton()
    {
        defuseBomb.Play();
        yield return new WaitForSeconds(0.3f);
        defuseBomb.Play();
        yield return new WaitForSeconds(0.3f);
        defuseBomb.Play();
        yield return new WaitForSeconds(0.3f);

        defuseBombFinal.Play();
    }

}
