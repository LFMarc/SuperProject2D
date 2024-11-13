using UnityEngine;

public class Jetpack : MonoBehaviour
{
    public enum Direction
    {
        Left,
        Right
    }

    #region Properties
    public float Energy
    {
        get
        {
            return _energy;
        }
        set
        {
            // Aseguramos que la energía no exceda el máximo
            _energy = Mathf.Clamp(value, 0, _maxEnergy);
        }
    }

    public bool Flying { get; set; }
    #endregion

    #region Fields
    private Rigidbody2D _targetRB;
    [SerializeField] private float _energy;   // Energía actual
    [SerializeField] private float _maxEnergy = 100f;  // Energía máxima inicial de 100
    [SerializeField] private float _energyFlyingRatio;
    [SerializeField] private float _energyRegenerationRatio;
    [SerializeField] private float _horizontalForce;
    [SerializeField] private float _flyForce;
    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        _targetRB = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        Energy = _maxEnergy;  // Establecer la energía inicial al valor máximo de 100
    }

    void FixedUpdate()
    {
        if (Flying)
            DoFly();

        if (Mathf.Abs(_targetRB.velocity.y) < 0.1f)
            Regenerate();
    }
    #endregion

    #region Public Methods
    public void FlyUp()
    {
        Flying = true;
    }

    public void StopFlying()
    {
        Flying = false;
    }

    public void Regenerate()
    {
        Energy += _energyRegenerationRatio;
    }

    public void AddEnergy(float energy)
    {
        Energy += energy;
    }

    public void FlyHorizontal(Direction flyDirection)
    {
        if (!Flying)
            return;

        if (flyDirection == Direction.Left)
            _targetRB.AddForce(Vector2.left * _horizontalForce);
        else
            _targetRB.AddForce(Vector2.right * _horizontalForce);
    }

    // Método para cambiar el valor de _maxEnergy cuando se complete el juego
    public void SetMaxEnergyTo1000()
    {
        _maxEnergy = 1000f;  // Cambiar _maxEnergy a 1000
        Energy = 1000f;  // Recargar la energía a 1000
    }
    #endregion

    #region Private Methods
    private void DoFly()
    {
        if (Energy > 0)
        {
            _targetRB.AddForce(Vector2.up * _flyForce);
            Energy -= _energyFlyingRatio;
        }
        else
            Flying = false;
    }
    #endregion
}
