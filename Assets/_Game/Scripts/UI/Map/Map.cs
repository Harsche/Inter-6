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
    [SerializeField] private GameObject legend1;
    [SerializeField] private GameObject legend2;
    [SerializeField] private float changeFloorHeight;

    private Bounds gameSpaceBounds;
    private Vector2 halfMapSpaceSize;
    private Vector2 mapSpaceSize;
    private int playerCurrentFloor;

    public static Map Instance{ get; private set; }

    private void Awake(){
        if (Instance != null){
            Destroy(gameObject);
            return;
        }

        Instance = this;

        if (!gameSpaceBoxCollider){ return; }

        gameSpaceBounds = gameSpaceBoxCollider.bounds;
        mapSpaceSize = mapSpaceTransform.sizeDelta;
        halfMapSpaceSize = mapSpaceSize / 2;
    }

    private void ChangeFloorDisplay(int floor){
        mapImage.sprite = floor == 2 ? floor2 : floor1;
        playerIcon.gameObject.SetActive(floor == playerCurrentFloor);
    }

    public void UpdateMap(){
        if (!gameSpaceBoxCollider){ return; }

        // Calculate Player Position
        Vector3 playerPosition = Player.Instance.transform.position;

        var inMinMaxX = new Vector2(gameSpaceBounds.min.x, gameSpaceBounds.max.x);
        var inMinMaxZ = new Vector2(gameSpaceBounds.min.z, gameSpaceBounds.max.z);

        var outMinMaxX = new Vector2(-halfMapSpaceSize.x, halfMapSpaceSize.x);
        var outMinMaxY = new Vector2(-halfMapSpaceSize.y, halfMapSpaceSize.y);

        var playerMapPosition = new Vector2(
            UtilityMethods.RemapClamped(inMinMaxX, outMinMaxX, playerPosition.x),
            UtilityMethods.RemapClamped(inMinMaxZ, outMinMaxY, playerPosition.z)
        );

        playerCurrentFloor = playerPosition.y >= changeFloorHeight ? 2 : 1;

        if (playerCurrentFloor == 1){
            legend2.SetActive(false);
            legend1.SetActive(true);
            DisplayFloor1(true);
        }
        else{
            legend1.SetActive(false);
            legend2.SetActive(true);
            DisplayFloor2(true);
        }

        playerIcon.localPosition = playerMapPosition;
        playerIcon.localEulerAngles = Vector3.back * (Player.Instance.transform.eulerAngles.y - 90f);
    }

    public void DisplayFloor1(bool value){
        if (value){ ChangeFloorDisplay(1); }
    }

    public void DisplayFloor2(bool value){
        if (value){ ChangeFloorDisplay(2); }
    }
}