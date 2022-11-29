using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;

public class ChangeVolume : MonoBehaviour{

    public void ToggleLight(bool onOrOff){
        if (!onOrOff){
            ActivateVolume(Player.Instance.VolumeDark);
            DeactivateVolume(Player.Instance.VolumeNormal);
            return;
        }

        ActivateVolume(Player.Instance.VolumeNormal);
        DeactivateVolume(Player.Instance.VolumeDark);
    }

    private void ActivateVolume(Volume volume){
        DOTween.To(() => volume.weight, x => volume.weight = x, 1f, 3f);
    }
    
    private void DeactivateVolume(Volume volume){
        DOTween.To(() => volume.weight, x => volume.weight = x, 0f, 3f);
    }
}