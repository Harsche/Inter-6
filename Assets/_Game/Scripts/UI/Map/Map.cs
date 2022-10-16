using Harsche.Utils;
using UnityEngine;

public class Map : MonoBehaviour{
    [SerializeField] private RectTransform mapSpaceTransform;
    [SerializeField] private BoxCollider gameSpaceBoxCollider;
    [SerializeField] private Transform playerIcon;

    private Bounds gameSpaceBounds;
    private Vector2 mapSpaceSize;
    private Vector2 halfMapSpaceSize;

    public static Map Instance{ get; private set; }

    private void Awake(){
        if (Instance != null){
            Destroy(gameObject);
            return;
        }

        Instance = this;

        gameSpaceBounds = gameSpaceBoxCollider.bounds;
        mapSpaceSize = mapSpaceTransform.sizeDelta;
        halfMapSpaceSize = mapSpaceSize / 2;
    }

    public void UpdateMap(){
        // Calculate Player Position
        Vector3 playerPosition = Player.Instance.transform.position;

        var inMinMaxX = new Vector2(gameSpaceBounds.min.x, gameSpaceBounds.max.x);
        var inMinMaxY = new Vector2(gameSpaceBounds.min.x, gameSpaceBounds.max.x);
        var inMinMaxZ = new Vector2(gameSpaceBounds.min.x, gameSpaceBounds.max.x);

        var outMinMaxX = new Vector2(-halfMapSpaceSize.x, halfMapSpaceSize.x);
        var outMinMaxY = new Vector2(-halfMapSpaceSize.y, halfMapSpaceSize.y);

        var playerMapPosition = new Vector2(
            UtilityMethods.RemapClamped(inMinMaxX, outMinMaxX, playerPosition.x),
            UtilityMethods.RemapClamped(inMinMaxY, outMinMaxY, playerPosition.y)
        );

        Vector2 finalMapPosition = -halfMapSpaceSize + playerMapPosition;
        playerIcon.position = finalMapPosition;
    }
    
    
}
    