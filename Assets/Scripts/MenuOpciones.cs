using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MenuOpciones : MonoBehaviour
{
    [SerializeField] private AudioMixer m_audioMixer;
   public void ajustarVolumen(float volumen)
    {
        m_audioMixer.SetFloat("Volumen", volumen);
    }
}
