using UnityEngine;

public class CameraRaycast : MonoBehaviour{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera outlineCamera;
    [SerializeField] private float raycastDistance = 10f;
    [SerializeField] private LayerMask layerMask;

    private IInteractable highlightedObject;

    private void Update(){
        if (GameManager.IsGamePaused){ return; }

        bool leftClick = Input.GetMouseButtonDown(0);
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hitInfo, raycastDistance, layerMask);
        IInteractable interactable = hitInfo.collider
            ? hitInfo.collider.GetComponent<IInteractable>()
            : null;
        outlineCamera.enabled = interactable != null;
        UpdateHighlighted(interactable);
        if (leftClick && interactable != null){ interactable.Interact(); }
    }

    private void UpdateHighlighted(IInteractable interactable){
        highlightedObject?.ToggleHighlight(false);
        interactable?.ToggleHighlight(true);
        highlightedObject = interactable;
    }
}