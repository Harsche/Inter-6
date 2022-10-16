using System;
using System.Collections;
using UnityEngine;

namespace Harsche.Utils{
    public static class UtilityMethods{
        public static Vector2 AngleToVector2(float angle){
            float radiansAngle = angle * Mathf.Deg2Rad;
            return new Vector2(Mathf.Cos(radiansAngle), Mathf.Sin(radiansAngle));
        }

        public static float RemapClamped(Vector2 inMinMax, Vector2 outMinMax, float value){
            if (value <= inMinMax.x) return outMinMax.x;
            if (value >= inMinMax.y) return outMinMax.y;
                float percentage = (value - inMinMax.x) / (inMinMax.y - inMinMax.x);
            return outMinMax.x + (outMinMax.y - outMinMax.x) * percentage;
        }
        
        public static IEnumerator TimedCallback(float time, Action callback){
            yield return new WaitForSeconds(time);
            callback?.Invoke();
        }
    }
}