using UnityEngine;

public class CameraController : MonoBehaviour
{
    private InputActions _inputActions;

    private Vector3 _newPosition;
    private Quaternion _newRotation;
    
    private Vector3 _currentZoom;
    private Vector3 _newZoom;

    [SerializeField] private Transform cameraTransform = null;
    [SerializeField] private Transform cameraTarget = null;

    [SerializeField] private float smoothFactor = 1f;

    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private float zoomSpeed = 1f;


    private void Awake()
    {
        _inputActions = new InputActions();

        _newPosition = transform.position;
        _newRotation = transform.rotation;

        _currentZoom = _newZoom = cameraTransform.localPosition;
    }

    private void Update()
    {
        // Get inputs
        Vector2 movementInput = _inputActions.CameraControls.Movement.ReadValue<Vector2>();
        float rotationInput = _inputActions.CameraControls.Rotation.ReadValue<float>();
        float zoomInput = _inputActions.CameraControls.Zoom.ReadValue<float>();
        
        // Calculate new positions
        _newPosition += (transform.right * movementInput.x + transform.forward * movementInput.y) * (movementSpeed * Time.deltaTime);
        _newRotation *= Quaternion.Euler(Vector3.up * (rotationInput * rotationSpeed * Time.deltaTime));
        _newZoom += new Vector3(0, -1, 1) * (zoomInput * zoomSpeed * Time.deltaTime);

        if (_newZoom.y >= cameraTarget.position.y + 3f)
            _currentZoom = _newZoom;
        else
            _newZoom = _currentZoom;

        // Apply position with interpolation
        transform.position = Vector3.Lerp(transform.position, _newPosition, Time.deltaTime * smoothFactor);
        transform.rotation = Quaternion.Lerp(transform.rotation, _newRotation, Time.deltaTime * smoothFactor);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, _currentZoom, Time.deltaTime * smoothFactor);

        cameraTransform.LookAt(cameraTarget);
    }

    private void OnEnable()
    {
        _inputActions.Enable();
    }

    private void OnDisable()
    {
        _inputActions.Disable();
    }
}
