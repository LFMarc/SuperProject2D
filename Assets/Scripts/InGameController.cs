using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.Collections;

public class InGameController : MonoBehaviour
{
    private AudioSource _audioSource;
    private float targetVolume = 0.3f;  // Volumen final después del fade in
    private float fadeDuration = 2f;    // Duración del efecto de fade in en segundos

    #region Unity Callbacks
    private void Start()
    {
        // Obtén el componente AudioSource en el objeto y configura el volumen inicial a 0
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource != null)
        {
            _audioSource.volume = 0f; // Inicia el volumen en 0
            Invoke(nameof(StartAudioWithFadeIn), 4f); // Espera 4 segundos antes de iniciar el audio con fade in
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
            SceneManager.LoadScene("MainMenu");
    }
    #endregion

    // Método para iniciar el audio con efecto de fade in
    private void StartAudioWithFadeIn()
    {
        _audioSource.Play();
        StartCoroutine(FadeInAudio());
    }

    private IEnumerator FadeInAudio()
    {
        float startVolume = _audioSource.volume;
        float elapsedTime = 0f;

        // Gradualmente incrementa el volumen durante el tiempo especificado
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            _audioSource.volume = Mathf.Lerp(startVolume, targetVolume, elapsedTime / fadeDuration);
            yield return null;
        }

        // Asegúrate de que el volumen final sea exactamente el targetVolume
        _audioSource.volume = targetVolume;
    }
}
