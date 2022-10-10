using BeatSaberMarkupLanguage;
using CountersPlus.ConfigModels;
using HarmonyLib;
using HMUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using VRUIControls;

/// <summary>
/// See https://github.com/pardeike/Harmony/wiki for a full reference on Harmony.
/// </summary>
namespace CenterDistanceCounter.HarmonyPatches
{
    /// <summary>
    /// This patches ClassToPatch.MethodToPatch(Parameter1Type arg1, Parameter2Type arg2)
    /// </summary>
    [HarmonyPatch(typeof(ResultsViewController), "SetDataToUI")]
    public class ResultsViewPatch
    {
        internal static string leftCount;

        internal static string canvasName;
        internal static Vector3 canvasPosition;
        internal static bool canvasMatchBaseGameHUDDepth;
        internal static Vector3 canvasRotation;
        internal static float canvasSize;
        internal static float canvasCurveRadius;

        internal static Vector2 leftAnchoredPosition;


        public static Canvas CreateCanvasWithConfig()
        {
            GameObject obj = new GameObject("Counters+ | " + canvasName + " Canvas")
            {
                layer = LayerMask.NameToLayer("UI")
            };
            Vector3 position = canvasPosition;
            if (canvasMatchBaseGameHUDDepth)
            {
                position.Set(position.x, position.y, 7f);
            }

            Vector3 rotation = canvasRotation;
            float size = canvasSize;
            Canvas canvas = obj.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.WorldSpace;
            obj.transform.localScale = Vector3.one / size;
            obj.transform.position = position;
            obj.transform.rotation = Quaternion.Euler(rotation);
            obj.AddComponent<CurvedCanvasSettings>().SetRadius(canvasCurveRadius);

            return canvas;
        }

        public static TMP_Text CreateText(Canvas canvas, Vector3 anchoredPosition)
        {
            RectTransform obj = canvas.transform as RectTransform;
            obj.sizeDelta = new Vector2(100f, 50f);

            TMP_Text tMP_Text = BeatSaberUI.CreateText(obj, leftCount, anchoredPosition);
            tMP_Text.gameObject.layer = LayerMask.NameToLayer("UI");
            tMP_Text.alignment = TextAlignmentOptions.Center;
            tMP_Text.fontSize = 4f;
            tMP_Text.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 2f);
            tMP_Text.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 2f);
            tMP_Text.enableWordWrapping = false;
            tMP_Text.overflowMode = TextOverflowModes.Overflow;

            return tMP_Text;
        }

        /// <summary>
        /// This code is run after the original code in MethodToPatch is run.
        /// </summary>
        /// <param name="__instance">The instance of ClassToPatch</param>
        /// <param name="arg1">The Parameter1Type arg1 that was passed to MethodToPatch</param>
        /// <param name="____privateFieldInClassToPatch">Reference to the private field in ClassToPatch named '_privateFieldInClassToPatch', 
        ///     added three _ to the beginning to reference it in the patch. Adding ref means we can change it.</param>
        static void Postfix(ResultsViewController __instance)
        {
            Logger.log.Info("Postfix");
            Logger.log.Info(leftCount);
            Logger.log.Info(canvasName);
            Logger.log.Info(canvasPosition.ToString());
            Logger.log.Info(canvasMatchBaseGameHUDDepth.ToString());
            Logger.log.Info(canvasRotation.ToString());
            Logger.log.Info(canvasSize.ToString());
            Logger.log.Info(canvasCurveRadius.ToString());

            Logger.log.Info(leftAnchoredPosition.ToString());

            var canvas=CreateCanvasWithConfig();

            var tmp_text=CreateText(canvas, leftAnchoredPosition);
            Logger.log.Info(tmp_text.rectTransform.ToString());
        }
    }
}