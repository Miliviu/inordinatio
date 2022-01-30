using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

using UnityEngine;

public class endscene : MonoBehaviour
{
    public GameObject image;
    public GameObject text1;
    public GameObject text2;
    public GameObject text3;
    public GameObject text4;
    public GameObject text5;


    // Start is called before the first frame update
    void Start()
    {
        image.SetActive(true);
        StartCoroutine(sad_text());
    }

    IEnumerator sad_text()
    {
        text1.SetActive(true);
        yield return new WaitForSeconds(10);
        text1.SetActive(false);
        text2.SetActive(true);
        yield return new WaitForSeconds(10);
        text2.SetActive(false);
        text3.SetActive(true);
        yield return new WaitForSeconds(10);
        text3.SetActive(false);
        text4.SetActive(true);
        yield return new WaitForSeconds(10);
        text4.SetActive(false);
        text5.SetActive(true);
        yield return new WaitForSeconds(10);
        Application.Quit();
    }
}
