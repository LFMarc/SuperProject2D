using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class UIController : MonoBehaviour
{
    #region Properties
    #endregion

    #region Fields
    [SerializeField] private Jetpack _jetpack; // Referencia al script Jetpack
    [SerializeField] private Slider _energySlider;
    [SerializeField] private TextMeshProUGUI _textSlider;

    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private AudioClip _newBackgroundMusic;

    // Referencia al ItemSpawner
    [SerializeField] private ItemSpawner _itemSpawner;

    // Referencia al sprite de Game Completed
    [SerializeField] private GameObject _gameCompletedSprite;

    // Bandera para asegurarnos de que la música y la energía solo cambien una vez
    private bool _hasCompletedGame = false;
    #endregion

    #region Unity Callbacks

    void Update()
    {
        // Actualizar el slider y el texto
        _energySlider.value = _jetpack.Energy;
        _textSlider.text = ((int)_jetpack.transform.position.y).ToString();

        // Verificar si el jugador completó el juego (valor de _textSlider >= 360)
        if (int.Parse(_textSlider.text) >= 360 && !_hasCompletedGame)
        {
            CompleteGame();
        }
    }

    #endregion

    #region Private Methods

    private void CompleteGame()
    {
        ChangeBackgroundMusic();

        _jetpack.SetMaxEnergyTo1000();

        _itemSpawner.enabled = false;

        _gameCompletedSprite.SetActive(true);

        StartCoroutine(AnimateScale(_gameCompletedSprite));

        _hasCompletedGame = true;
    }

    private void ChangeBackgroundMusic()
    {
        _audioSource.Stop();

        _audioSource.clip = _newBackgroundMusic;
        _audioSource.Play();
    }

    #endregion

    #region Animation Methods

    private IEnumerator AnimateScale(GameObject sprite)
    {
        RectTransform rt = sprite.GetComponent<RectTransform>();
        Vector3 originalScale = rt.localScale;
        Vector3 targetScale = originalScale * 1.2f; // Aumentar un 20% para el efecto

        // Ciclo de animación para aumentar y disminuir la escala
        while (true)
        {
            // Aumentar la escala
            float time = 0f;
            while (time < 1f)
            {
                rt.localScale = Vector3.Lerp(originalScale, targetScale, time);
                time += Time.deltaTime * 2f; // Velocidad de la animación
                yield return null;
            }

            // Disminuir la escala de vuelta
            time = 0f;
            while (time < 1f)
            {
                rt.localScale = Vector3.Lerp(targetScale, originalScale, time);
                time += Time.deltaTime * 2f; // Velocidad de la animación
                yield return null;
            }
        }
    }

    #endregion
}
