using System.Collections;
using UnityEngine;

public class ItemGhost : Item
{
    const float EFFECT_DURATION = 3f;  // Duración del efecto en segundos
    const float DESTROY_DELAY = 20f;   // Tiempo para destruir el objeto después de activarse

    #region Unity Callbacks
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Cambiar la etiqueta del jugador a "Untagged"
            GameObject player = collision.gameObject;
            string originalTag = player.tag;
            player.tag = "Untagged";

            // Activar el trigger "Ghost" en el Animator
            Animator playerAnimator = player.GetComponent<Animator>();
            if (playerAnimator != null)
            {
                playerAnimator.SetTrigger("Ghost");
            }

            // Hacer invisible el objeto
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = false; // Hace el objeto invisible
            }

            // Desactivar las colisiones
            Collider2D collider = GetComponent<Collider2D>();
            if (collider != null)
            {
                collider.enabled = false; // Desactiva las colisiones
            }

            // Obtener el componente Jetpack y poner la energía a 0
            Jetpack jetpack = player.GetComponent<Jetpack>();
            if (jetpack != null)
            {
                jetpack.Energy = 0f; // Poner la energía a 0
            }

            RecolectedNoDestroy(); // Genera las partículas sin destruir el objeto

            // Iniciar las corrutinas para el efecto y para la destrucción
            StartCoroutine(ApplyGhostEffect(player, originalTag, jetpack));
            StartCoroutine(DestroyAfterDelay()); // Inicia el temporizador para destruir después de 20 segundos
        }
    }
    #endregion

    #region Private Methods
    private IEnumerator ApplyGhostEffect(GameObject player, string originalTag, Jetpack jetpack)
    {
        // Esperar la duración del efecto (3 segundos)
        yield return new WaitForSeconds(EFFECT_DURATION);

        // Restaurar la etiqueta del jugador
        player.tag = originalTag;

        // Restaurar la energía del jugador a 100
        if (jetpack != null)
        {
            jetpack.Energy = 100f; // Restaurar la energía al máximo (100)
        }
    }

    private IEnumerator DestroyAfterDelay()
    {
        // Esperar 20 segundos antes de destruir el objeto
        yield return new WaitForSeconds(DESTROY_DELAY);
        Destroy(gameObject); // Destruir el objeto después de los 20 segundos
    }
    #endregion

    // Método personalizado para crear las partículas pero no destruir el objeto
    private void RecolectedNoDestroy()
    {
        CreateParticles(); // Crea las partículas
    }
}
