using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class dialogBox : MonoBehaviour
{
    private Image m_image;
    private TextMeshProUGUI m_text;

    private float fadeInSeconds = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        m_image = GetComponent<Image>();
        m_text = gameObject.GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showDialog(string text)
    {
        m_text.text += text;
        StartCoroutine(fadeDialogIn());
    }

    IEnumerator fadeDialogIn()
    {
        Color originalColor = m_image.color;
        float elapsedTime = 0f;

        while (elapsedTime < 0.3f)
        {
            float alpha = Mathf.Lerp(0, 1, elapsedTime / 0.3f);
            m_image.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        m_image.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1);
    }

    public void closeDialog()
    {
        m_image.color = new Color(m_image.color.r, m_image.color.g, m_image.color.b, 0);
        m_text.text = "";
    }
}
