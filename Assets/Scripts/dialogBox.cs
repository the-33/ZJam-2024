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
        showDialog("");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void showDialog(string text)
    {
        m_text.text = text;

        for (float i = 0; i <= fadeInSeconds; i += Time.deltaTime)
        {
            m_image.color = new Color(m_image.color.r, m_image.color.g, m_image.color.b, i/fadeInSeconds);
        }

        m_image.color = new Color(m_image.color.r, m_image.color.g, m_image.color.b, 1);
    }

    void closeDialog()
    {
        m_image.color = new Color(m_image.color.r, m_image.color.g, m_image.color.b, 0);
        m_text.text = "";
    }
}
