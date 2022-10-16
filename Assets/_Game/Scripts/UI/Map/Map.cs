using Harsche.Utils;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour{
    [SerializeField] private RectTransform mapSpaceTransform;
    [SerializeField] private BoxCollider gameSpaceBoxCollider;
    [SerializeField] private Transform playerIcon;
    [SerializeField] private Image mapImage;
    [SerializeField] private Sprite floor1;
    [SerializeField] private Sprite floor2;
    [SerializeField] private float changeFloorHeight;

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

        if (!gameSpaceBoxCollider) return;
        gameSpaceBounds = gameSpaceBoxCollider.bounds;
        mapSpaceSize = mapSpaceTransform.sizeDelta;
        halfMapSpaceSize = mapSpaceSize / 2;
    }

    public void UpdateMap(){
        if (!gameSpaceBoxCollider) return;
        // Calculate Player Position
        Vector3 playerPosition = Player.Instance.transform.position;

        var inMinMaxX = new Vector2(gameSpaceBounds.min.x, gameSpaceBounds.max.x);
        // var inMinMaxY = new Vector2(gameSpaceBounds.min.y, gameSpaceBounds.max.y);
        var inMinMaxZ = new Vector2(gameSpaceBounds.min.z, gameSpaceBounds.max.z);

        var outMinMaxX = new Vector2(-halfMapSpaceSize.x, halfMapSpaceSize.x);
        var outMinMaxY = new Vector2(-halfMapSpaceSize.y, halfMapSpaceSize.y);

        var playerMapPosition = new Vector2(
            UtilityMethods.RemapClamped(inMinMaxX, outMinMaxX, playerPosition.x),
            UtilityMethods.RemapClamped(inMinMaxZ, outMinMaxY, playerPosition.z)
        );

        mapImage.sprite = playerPosition.y >= changeFloorHeight ? floor2 : floor1;

        playerIcon.localPosition = playerMapPosition;
        playerIcon.localEulerAngles = Vector3.back * Player.Instance.transform.localEulerAngles.y;
    }
}