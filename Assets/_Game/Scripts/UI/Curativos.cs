using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curativos : MonoBehaviour
{
    public void BandAid()
    {
        HUDManager.Instance.Curar(1);
    }
}
