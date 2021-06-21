using System;
using System.Collections.Generic;
using System.Linq;
using BeatSaberMarkupLanguage.Attributes;

namespace CenterDistanceCounter
{
    public class SettingsHandler
    {
        [UIValue("SeparateSaber")]
        public bool SeparateSaber
        {
            get => Configuration.Instance.SeparateSaber;
            set
            {
                Configuration.Instance.SeparateSaber = value;
            }
        }

        [UIValue("DecimalPrecision")]
        public int DecimalPrecision
        {
            get => Configuration.Instance.DecimalPrecision;
            set
            {
                Configuration.Instance.DecimalPrecision = value;
            }
        }

        [UIValue("CounterType")]
        public string CounterType
        {
            get => Configuration.Instance.CounterType;
            set
            {
                Configuration.Instance.CounterType = value;
            }
        }

        [UIValue("type")]
        public List<object> type = Enum.GetNames(typeof(Configuration.counterType)).ToList<object>();


        [UIValue("EnableLabel")]
        public bool EnableLabel
        {
            get => Configuration.Instance.EnableLabel;
            set
            {
                Configuration.Instance.EnableLabel = value;
            }
        }

        [UIValue("LabelFontSize")]
        public float LabelFontSize
        {
            get => Configuration.Instance.LabelFontSize;
            set
            {
                Configuration.Instance.LabelFontSize = value;
            }
        }

        [UIValue("FigureFontSize")]
        public float FigureFontSize
        {
            get => Configuration.Instance.FigureFontSize;
            set
            {
                Configuration.Instance.FigureFontSize = value;
            }
        }

        [UIValue("OffsetX")]
        public float OffsetX
        {
            get => Configuration.Instance.OffsetX;
            set
            {
                Configuration.Instance.OffsetX = value;
            }
        }

        [UIValue("OffsetY")]
        public float OffsetY
        {
            get => Configuration.Instance.OffsetY;
            set
            {
                Configuration.Instance.OffsetY = value;
            }
        }

        [UIValue("OffsetZ")]
        public float OffsetZ
        {
            get => Configuration.Instance.OffsetZ;
            set
            {
                Configuration.Instance.OffsetZ = value;
            }
        }
    }
}

