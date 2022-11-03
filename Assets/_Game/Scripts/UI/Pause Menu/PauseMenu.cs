using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour{
    [SerializeField] private OptionItems optionItems;

    private Canvas canvas;
    
    private static bool GamePaused;

    private void Awake(){
        canvas = GetComponent<Canvas>();
    }

    private void Update(){
        if (!Input.GetKeyDown(KeyCode.Escape)) return;
        TogglePause(!GamePaused);
    }

    public void SetCameraSensitivity(float value){
        PlayerPrefs.SetFloat("CameraSensitivity", value);
    }

    public void TogglePause(bool value){
        GamePaused = value;
        Time.timeScale = GamePaused ? 0 : 1;
        canvas.enabled = GamePaused;
        Cursor.lockState = GamePaused ? CursorLockMode.None : CursorLockMode.Locked;
        UpdateOptionItems();
        if (!GamePaused) return;
        Map.Instance.UpdateMap();
    }

    private void UpdateOptionItems(){
        optionItems.SensitivitySlider.value = PlayerPrefs.GetFloat("CameraSensitivity");
    }

    [Serializable]
    private class OptionItems{
        [field: SerializeField] public Slider SensitivitySlider{ get; private set; }
        [field: SerializeField] public Slider VolumeSlider{ get; private set; }
    }
}