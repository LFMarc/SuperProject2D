using UnityEngine;
using System;

public class Item : MonoBehaviour, IRecolectable
{
    #region Enums
    public enum ItemTypes
    {
        None,
        NoSe,
        ErrorCode,
        PositiveWords,
        GhostMode
    }
    #endregion

    #region Properties
    [field: SerializeField] public ItemTypes Type { get; set; }
    #endregion

    #region Fields
    [SerializeField] private GameObject _particles;
    #endregion

    #region Public Methods
    public virtual void Recolected()
    {
        Destroy(gameObject);
        CreateParticles();
    }
    #endregion

    #region Private Methods
    protected void CreateParticles()  // Cambiado de private a protected
    {
        Instantiate(_particles, transform.position, Quaternion.identity);
    }
    #endregion
}
