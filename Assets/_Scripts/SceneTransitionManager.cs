using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransitionManager : MonoBehaviour
{
    [SerializeField] private Image m_blackScreen;
    [SerializeField] private float m_crossfadeDuration = 1f;

    public void ChangeScene(string sceneName)
    {
        Debug.Log("called scene change");
        StartCoroutine(Crossfade(sceneName));
    }

    private IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(1f);
    }

    private void FadeOut()
    {

    }

    private IEnumerator Crossfade(string sceneName)
    {
        m_blackScreen.enabled = true;

        Color solidBlackScreen = Color.black;
        Color alphaBlackScreen = solidBlackScreen;
        alphaBlackScreen.a = 0f;

        float elapsed = 0f;

        while (m_blackScreen.color.a < 1f)
        {
            Color newColor = Color.Lerp(alphaBlackScreen, solidBlackScreen, elapsed / (m_crossfadeDuration/2));
            m_blackScreen.color = newColor;

            Debug.Log(m_blackScreen.color.a);
            elapsed += Time.deltaTime;
            yield return null;
        }

        SceneManager.LoadScene(sceneName);

        elapsed = 0f;

        while (m_blackScreen.color.a > 0f)
        {
            Color newColor = Color.Lerp(solidBlackScreen, alphaBlackScreen, elapsed / (m_crossfadeDuration/2));
            m_blackScreen.color = newColor;

            elapsed += Time.deltaTime;
            yield return null;
        }

        m_blackScreen.enabled = false;
    }
}
