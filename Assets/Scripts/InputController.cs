using UnityEngine;
using System;

public class InputController : MonoBehaviour
{
    #region Properties
    #endregion

    #region Fields
    [SerializeField] private Jetpack _jetpack;
    [SerializeField] private Transform _characterTransform;
    #endregion

    #region Unity Callbacks
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Horizontal Fly
        if (Input.GetAxis("Horizontal") < 0)
        {
            _jetpack.FlyHorizontal(Jetpack.Direction.Left);
            FlipSprite(true);
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            _jetpack.FlyHorizontal(Jetpack.Direction.Right);
            FlipSprite(false);
        }

        //Vertical Fly
        if (Input.GetAxis("Vertical") > 0)
            _jetpack.FlyUp();
        else
            _jetpack.StopFlying();
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    private void FlipSprite(bool flip)
    {
        Vector3 scale = _characterTransform.localScale;
        scale.x = flip ? -Mathf.Abs(scale.x) : Mathf.Abs(scale.x);
        _characterTransform.localScale = scale;
    }
    #endregion
}
