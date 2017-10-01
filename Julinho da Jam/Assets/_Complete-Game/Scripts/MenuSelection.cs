using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuSelection : MonoBehaviour {

    public GameObject[] highLighted;
    public EventSystem eventSystem;
    public GameObject selectedObject;
    private bool buttonSelected;
    public AudioSource efxSource;
    public AudioClip cursor;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Cancel") > 0)
        {
            SceneManager.LoadScene("Menu");
        }

        if (Input.GetAxisRaw("Vertical") != 0 && buttonSelected == false)
        {
            PlaySingle(cursor);
            eventSystem.SetSelectedGameObject(selectedObject);
            buttonSelected = true;
        }
        if (Input.GetAxisRaw("Horizontal") != 0 && buttonSelected == false)
        {
            PlaySingle(cursor);
            eventSystem.SetSelectedGameObject(selectedObject);
            buttonSelected = true;
        }
    }

    private void OnDisable()
    {
        buttonSelected = false;
    }

    public void level1()
    {
        SceneManager.LoadScene("_Complete-Game");
    }
    public void level2()
    {
        SceneManager.LoadScene("_Complete-Game");
    }
    public void level3()
    {
        SceneManager.LoadScene("_Complete-Game");
    }
    public void level4()
    {
        SceneManager.LoadScene("_Complete-Game");
    }
    public void level5()
    {
        SceneManager.LoadScene("_Complete-Game");
    }
    public void level6()
    {
        SceneManager.LoadScene("_Complete-Game");
    }
    public void level7()
    {
        SceneManager.LoadScene("_Complete-Game");
    }
    public void level8()
    {
        SceneManager.LoadScene("_Complete-Game");
    }
    public void level9()
    {
        SceneManager.LoadScene("_Complete-Game");
    }
    public void level10()
    {
        SceneManager.LoadScene("_Complete-Game");
    }
    public void Volta()
    {
        SceneManager.LoadScene("Menu");
    }

    public void PlaySingle(AudioClip clip)
		{
			//Set the clip of our efxSource audio source to the clip passed in as a parameter.
			efxSource.clip = clip;
			
			//Play the clip.
			efxSource.Play ();
		}
}
