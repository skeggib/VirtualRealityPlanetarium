using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

[RequireComponent(typeof(Camera))]
public class FreeFlyCamera : MonoBehaviour
{
    [Space]

    [SerializeField]
    private bool _active = true;

    [Space]

    [SerializeField]
    private bool _enableRotation = true;

    [SerializeField]
    private float _mouseSense = 0.8f;

    [Space]

    [SerializeField]
    private bool _enableTranslation = true;

    [SerializeField]
    private float _translationSpeed = 55f;

    [Space]

    [SerializeField]
    private bool _enableMovement = true;

    [SerializeField]
    private float _movementSpeed = 10f;

    [SerializeField]
    private float _boostedSpeed = 50f;

    [Space]

    [SerializeField]
    private bool _enableSpeedAcceleration = true;

    [SerializeField]
    private float _speedAccelerationFactor = 1.5f;

    [Space]

    [SerializeField]
    private KeyCode _initPositonButton = KeyCode.R;
    
    private CursorLockMode _wantedMode;
    private float _currentIncrease;

    private Vector3 _initPosition;
    private Vector3 _initRotation;

    public GameObject Globals;
    private Globals _globalsScript;

    private void Start()
    {
        _initPosition = transform.position;
        _initRotation = transform.eulerAngles;
        _globalsScript = Globals.GetComponent<Globals>();
    }

    private void OnEnable()
    {
        if (_active)
            _wantedMode = CursorLockMode.Locked;
    }

    // Apply requested cursor state
    private void SetCursorState()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = _wantedMode = CursorLockMode.None;
        }

        if (Input.GetMouseButtonDown(0))
        {
            _wantedMode = CursorLockMode.Locked;
        }

        // Apply cursor state
        Cursor.lockState = _wantedMode;
        // Hide cursor when locking
        Cursor.visible = (CursorLockMode.Locked != _wantedMode);
    }

    private void Update()
    {
        if (!_active)
            return;

        SetCursorState();

        if (Cursor.visible)
            return;

        // Rotation
        if (_enableRotation)
        {
            Vector3 eulerAngles = transform.eulerAngles;
            eulerAngles.x -= Input.GetAxis("Mouse Y") * _mouseSense;
            eulerAngles.y += Input.GetAxis("Mouse X") * _mouseSense;
            transform.eulerAngles = eulerAngles;
        }

        // Translation
        if (_enableTranslation)
        {
            transform.Translate(Vector3.forward * Input.mouseScrollDelta.y * Time.deltaTime * _translationSpeed);
        }

        // Movement
        if (_enableMovement)
        {
            Vector3 deltaPosition = Vector3.zero;
            float currentSpeed = _movementSpeed;

            var closestDistance = _globalsScript.Planets.Min(planet => Vector3.Distance(planet.transform.position, transform.position));
            double factor = 1 / (1 + Math.Exp(-7 * (closestDistance - 0.5)));
            currentSpeed *= (float)factor;

            if (Input.GetKey(KeyCode.Z))
                deltaPosition += new Vector3(transform.forward.x, 0, transform.forward.z).normalized;

            if (Input.GetKey(KeyCode.S))
                deltaPosition -= new Vector3(transform.forward.x, 0, transform.forward.z).normalized;

            if (Input.GetKey(KeyCode.Q))
                deltaPosition -= transform.right;

            if (Input.GetKey(KeyCode.D))
                deltaPosition += transform.right;

            if (Input.GetKey(KeyCode.Space))
                deltaPosition += new Vector3(0, 1, 0);

            if (Input.GetKey(KeyCode.LeftShift))
                deltaPosition -= new Vector3(0, 1, 0);

            // Calc acceleration
            if (_enableSpeedAcceleration)
            {
                if (deltaPosition != Vector3.zero)
                    _currentIncrease += _currentIncrease * _speedAccelerationFactor * Time.deltaTime;
                else
                    _currentIncrease = _speedAccelerationFactor;
            }

            transform.position += deltaPosition * (currentSpeed + _currentIncrease) * Time.deltaTime;
        }

        // Return to init position
        if (Input.GetKeyDown(_initPositonButton))
        {
            transform.position = _initPosition;
            transform.eulerAngles = _initRotation;
        }
    }
}
