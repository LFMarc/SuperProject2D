using UnityEngine;

public class ItemNose : Item
{
    const float NOSE_DAMAGE = -20;

    #region Unity Callbacks
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Destruir si colisiona con el suelo
        if (collision.gameObject.tag == "Ground")
        {
            Recolected();
        }

        // Reducir energía del jugador si colisiona con el jugador
        if (collision.gameObject.tag == "Player")
        {
            Jetpack jetpack = collision.gameObject.GetComponent<Jetpack>();
            if (jetpack != null)
            {
                jetpack.AddEnergy(NOSE_DAMAGE);
            }
            Recolected();
        }

        // Destruir cualquier otro ítem
        Item item = collision.gameObject.GetComponent<Item>();
        if (item != null && item != this)  // Verificar que no sea sí mismo
        {
            Destroy(item.gameObject);
        }
    }

    #endregion
}
