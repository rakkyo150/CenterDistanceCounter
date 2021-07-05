using System;
using System.Collections.Generic;
using System.Globalization;
using CountersPlus.Counters.Custom;
using CountersPlus.Counters.Interfaces; 
using TMPro;
using UnityEngine;

namespace CenterDistanceCounter
{
    public class CenterDistanceCounter : BasicCustomCounter, INoteEventHandler, IDisposable
    {

        private int _notesLeft = 0;
        private int _notesRight = 0;

        private double _absoluteAverageLeft = 0;
        private double _absoluteAverageRight = 0;

        private double _absoluteTotalLeft = 0;
        private double _absoluteTotalRight = 0;
        private double _absoluteAverageBoth;

        private double _relativeAverageLeft = 0;
        private double _relativeAverageRight = 0;

        private double _relativeTotalLeft = 0;
        private double _relativeTotalRight = 0;
        private double _relativeAverageBoth;

        private List<double> _listLeft = new List<double>();
        private List<double> _listRight = new List<double>();
        private List<double> _listBoth = new List<double>();

        private TMP_Text _counterLeft;
        private TMP_Text _counterRight;

        float x = Configuration.Instance.OffsetX;
        float y = Configuration.Instance.OffsetY;
        float z = Configuration.Instance.OffsetZ;

        double _relativeValue;
        double _absoluteValue;

        private BeatmapObjectManager _objectManager;
        private bool disposedValue;

        public CenterDistanceCounter(BeatmapObjectManager objectManager)
        {
            this._objectManager = objectManager;
            this._objectManager.noteWasCutEvent += this.OnNoteCutWasEvent;
        }

        public override void CounterInit()
        {
            string defaultValue = $"{Format(0, Configuration.Instance.DecimalPrecision)}\n{Format(0, Configuration.Instance.DecimalPrecision)}";

            if (Configuration.Instance.CounterType != Configuration.counterType.Both.ToString())
            {
                defaultValue = Format(0, Configuration.Instance.DecimalPrecision);
            }

            if (Configuration.Instance.EnableLabel)
            {
                var label = CanvasUtility.CreateTextFromSettings(Settings, new Vector3(x, y, z));
                label.text = "Center Distance";
                label.fontSize = Configuration.Instance.LabelFontSize;
            }

            Vector3 leftOffset = new Vector3(x, y - 0.2f, z);
            TextAlignmentOptions leftAlign = TextAlignmentOptions.Top;
            if (Configuration.Instance.SeparateSaber)
            {
                _counterRight = CanvasUtility.CreateTextFromSettings(Settings, new Vector3(x + 0.2f, y - 0.2f, z));
                _counterRight.lineSpacing = -26;
                _counterRight.fontSize = Configuration.Instance.FigureFontSize;
                _counterRight.text = defaultValue;
                _counterRight.alignment = TextAlignmentOptions.TopLeft;

                leftOffset = new Vector3(x - 0.2f, y - 0.2f, z);
                leftAlign = TextAlignmentOptions.TopRight;
            }

            _counterLeft = CanvasUtility.CreateTextFromSettings(Settings, leftOffset);
            _counterLeft.lineSpacing = -26;
            _counterLeft.fontSize = Configuration.Instance.FigureFontSize;
            _counterLeft.text = defaultValue;
            _counterLeft.alignment = leftAlign;
        }

        public void OnNoteCut(NoteData data, NoteCutInfo info) { }
        public void OnNoteMiss(NoteData data) { }
        public void OnNoteCutWasEvent(NoteController data, in NoteCutInfo info)
        {
            if (data.noteData.colorType == ColorType.None || !info.allIsOK) return;
            UpdateText(data.noteTransform,data.noteData.cutDirection,info.cutPoint,info.cutDistanceToCenter, info.saberType);
        }

        public void UpdateText(Transform center,NoteCutDirection direction,Vector3 cutPoint,double cutDistance, SaberType saberType)
        {
            cutDistance *= 100;
            
            //For next update
            _listBoth.Add(cutDistance);

            _absoluteValue = cutDistance;
            _relativeValue = cutDistance*Relativize(center, direction, cutPoint);


            if (saberType == SaberType.SaberA)
            {
                //For next update
                _listLeft.Add(cutDistance);

                _absoluteTotalLeft = (_absoluteAverageLeft * _notesLeft) + _absoluteValue;
                _relativeTotalLeft = (_relativeAverageLeft * _notesLeft) + _relativeValue;
                _notesLeft++;
                _absoluteAverageLeft = _absoluteTotalLeft / _notesLeft;
                _relativeAverageLeft = _relativeTotalLeft / _notesLeft;
            }
            else
            {
                //For next update
                _listRight.Add(cutDistance);

                _absoluteTotalRight = (_absoluteAverageRight * _notesRight) + _absoluteValue;
                _relativeTotalRight = (_relativeAverageRight * _notesRight) + _relativeValue;
                _notesRight++;
                _absoluteAverageRight = _absoluteTotalRight / _notesRight;
                _relativeAverageRight = _relativeTotalRight / _notesRight;
            }

            _absoluteAverageBoth = (_absoluteTotalLeft + _absoluteTotalRight) / (_notesLeft + _notesRight);
            _relativeAverageBoth = (_relativeTotalLeft + _relativeTotalRight) / (_notesLeft + _notesRight);

            UpdateText();
        }

        
        private int Relativize(Transform center, NoteCutDirection direction, Vector3 cutPoint)
        {
            if (direction != NoteCutDirection.Left && direction != NoteCutDirection.Right)
            {
                if (center.position.x <= cutPoint.x) return 1;
                else return -1;
            }
            else if(direction==NoteCutDirection.Left)
            {
                if (center.position.y >= cutPoint.y) return 1;
                else return -1;
            }
            else
            {
                if (center.position.y <= cutPoint.y) return 1;
                else return -1;
            }
        }
        private void UpdateText()
        {
            if (Configuration.Instance.CounterType == Configuration.counterType.Both.ToString())
            {
                if (Configuration.Instance.SeparateSaber)
                {
                    _counterLeft.text = $"{Format(_absoluteAverageLeft, Configuration.Instance.DecimalPrecision)}\n{Format(_relativeAverageLeft, Configuration.Instance.DecimalPrecision)}";
                    _counterRight.text = $"{Format(_absoluteAverageRight, Configuration.Instance.DecimalPrecision)}\n{Format(_relativeAverageRight, Configuration.Instance.DecimalPrecision)}";
                }
                else
                {
                    _counterLeft.text = $"{Format(_absoluteAverageBoth, Configuration.Instance.DecimalPrecision)}\n{Format(_relativeAverageBoth, Configuration.Instance.DecimalPrecision)}";

                }
            }
            else if (Configuration.Instance.CounterType == Configuration.counterType.Absolute.ToString())
            {

                if (Configuration.Instance.SeparateSaber)
                {
                    _counterLeft.text = Format(_absoluteAverageLeft, Configuration.Instance.DecimalPrecision);
                    _counterRight.text = Format(_absoluteAverageRight, Configuration.Instance.DecimalPrecision);
                }
                else
                {
                    _counterLeft.text = Format(_absoluteAverageBoth, Configuration.Instance.DecimalPrecision);
                }
            }
            else if (Configuration.Instance.CounterType == Configuration.counterType.Relative.ToString())
            {
                if (Configuration.Instance.SeparateSaber)
                {
                    _counterLeft.text = Format(_relativeAverageLeft, Configuration.Instance.DecimalPrecision);
                    _counterRight.text = Format(_relativeAverageRight, Configuration.Instance.DecimalPrecision);
                }
                else
                {
                    _counterLeft.text = Format(_relativeAverageBoth, Configuration.Instance.DecimalPrecision);
                }
            }
        }

        private string Format(double CenterDistance, int decimals)
        {
            return CenterDistance.ToString($"F{decimals}", CultureInfo.InvariantCulture);
        }

        public override void CounterDestroy() { }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue) {
                if (disposing) {
                    this._objectManager.noteWasCutEvent -= this.OnNoteCutWasEvent;
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
