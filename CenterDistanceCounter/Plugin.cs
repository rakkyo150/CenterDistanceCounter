using HarmonyLib;
using IPA;
using IPA.Config.Stores;
using SiraUtil.Zenject;
using System.Reflection;
using System;
using IPALogger = IPA.Logging.Logger;

namespace CenterDistanceCounter
{
    [Plugin(RuntimeOptions.DynamicInit)]
    public class Plugin
    {
        internal static Plugin instance { get; private set; }
        internal static string Name => "CenterDistanceCounter";

        public const string HarmonyId = "com.github.rakkyo150.CenterDistanceCounter";
        internal static readonly HarmonyLib.Harmony harmony = new HarmonyLib.Harmony(HarmonyId);

        [Init]
        public void Init(IPALogger logger, IPA.Config.Config config, Zenjector zenjector)
        {
            Configuration.Instance = config.Generated<Configuration>();
            instance = this;
            Logger.log = logger;
            Logger.log.Debug("Logger initialized.");
        }

        [OnEnable]
        public void OnEnable() 
        {
            ApplyHarmonyPatches();
        }

        [OnDisable]
        public void OnDisable() 
        {
            RemoveHarmonyPatches();
        }

        #region Harmony
        /// <summary>
        /// Attempts to apply all the Harmony patches in this assembly.
        /// </summary>
        internal static void ApplyHarmonyPatches()
        {
            try
            {
                harmony.PatchAll(Assembly.GetExecutingAssembly());
            }
            catch (Exception ex)
            {
                Logger.log?.Error("Error applying Harmony patches: " + ex.Message);
                Logger.log?.Debug(ex);
            }
        }

        /// <summary>
        /// Attempts to remove all the Harmony patches that used our HarmonyId.
        /// </summary>
        internal static void RemoveHarmonyPatches()
        {
            try
            {
                harmony.UnpatchSelf();
            }
            catch (Exception ex)
            {
                Logger.log?.Error("Error removing Harmony patches: " + ex.Message);
                Logger.log?.Debug(ex);
            }
        }
        #endregion
    }
}
