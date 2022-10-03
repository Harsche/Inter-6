using UnityEngine;

public class CameraRaycast : MonoBehaviour{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float raycastDistance = 10f;
    [SerializeField] private string layerName;

    private IInteractable highlightedObject;
    
    private void Update(){
        bool leftClick = Input.GetMouseButtonDown(0);
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hitInfo, raycastDistance, LayerMask.NameToLayer(layerName));
        IInteractable interactable = hitInfo.collider
            ? hitInfo.collider.GetComponent<IInteractable>()
            : null;
        UpdateHighlighted(interactable);
        if (leftClick && interactable != null) interactable.Interact();
    }

    private void UpdateHighlighted(IInteractable interactable){
        highlightedObject?.ToggleHighlight(false);
        interactable?.ToggleHighlight(true);
        highlightedObject = interactable;
    }
}