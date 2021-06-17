﻿using CountersPlus.Counters.Custom;
using CountersPlus.Counters.Interfaces;
using System;
using System.Globalization;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CenterDistanceCounter
{
    public class CenterDistanceCounter : BasicCustomCounter,INoteEventHandler
    {
        
        private int _notesLeft=0;
        private int _notesRight=0;

        private double _averageLeft=0;
        private double _averageRight=0;
        
        private double _totalLeft = 0;
        private double _totalRight = 0;
        private double _averageBoth;

        private List<double> _listLeft = new List<double>();
        private List<double> _listRight = new List<double>();
        private List<double> _listBoth = new List<double>();

        private double _standardDeviationLeft=0;
        private double _standardDeviationRight=0;
        private double _standardDeviationBoth;

        private TMP_Text _counterLeft;
        private TMP_Text _counterRight;

        float x = Configuration.Instance.OffsetX;
        float y = Configuration.Instance.OffsetY;
        float z = Configuration.Instance.OffsetZ;
        

        public override void CounterInit()
        {          
            string defaultValue = $"{Format(0, Configuration.Instance.DecimalPrecision)}\n{Format(0, Configuration.Instance.DecimalPrecision)}";

            if (Configuration.Instance.CounterType!=Configuration.counterType.Both.ToString())
            {
                defaultValue =  Format(0, Configuration.Instance.DecimalPrecision);
            }

            if (Configuration.Instance.EnableLabel)
            {
                var label = CanvasUtility.CreateTextFromSettings(Settings,new Vector3 (x,y,z));
                label.text = "Center Distance";
                label.fontSize = Configuration.Instance.LabelFontSize;
            }
            
            Vector3 leftOffset = new Vector3(x,y-0.2f,z);
            TextAlignmentOptions leftAlign = TextAlignmentOptions.Top;
            if (Configuration.Instance.SeparateSaber)
            {
                _counterRight = CanvasUtility.CreateTextFromSettings(Settings, new Vector3(x+0.2f, y-0.2f, z));
                _counterRight.lineSpacing = -26;
                _counterRight.fontSize = Configuration.Instance.FigureFontSize;
                _counterRight.text = defaultValue;
                _counterRight.alignment = TextAlignmentOptions.TopLeft;

                leftOffset = new Vector3(x-0.2f, y-0.2f, z);
                leftAlign = TextAlignmentOptions.TopRight;
            }

            _counterLeft = CanvasUtility.CreateTextFromSettings(Settings, leftOffset);
            _counterLeft.lineSpacing = -26;
            _counterLeft.fontSize = Configuration.Instance.FigureFontSize;
            _counterLeft.text = defaultValue;
            _counterLeft.alignment = leftAlign;
        }

        public void OnNoteMiss(NoteData data) { }

        public void OnNoteCut(NoteData data,NoteCutInfo info)
        {
            if (data.colorType == ColorType.None || !info.allIsOK) return;
            UpdateText(info.cutDistanceToCenter, info.saberType);
        }

        public void UpdateText(double cutDistance, SaberType saberType)
        {
            cutDistance *= 100;
            _listBoth.Add(cutDistance);
            
            if (saberType == SaberType.SaberA)
            {
                _listLeft.Add(cutDistance);

                _totalLeft = (_averageLeft * _notesLeft) + cutDistance;
                _notesLeft++;
                _averageLeft = _totalLeft / _notesLeft;

                _standardDeviationLeft=StandardDeviation(_listLeft, _averageLeft, _notesLeft);
            }
            else
            {
                _listRight.Add(cutDistance);

                _totalRight = (_averageRight * _notesRight) + cutDistance;
                _notesRight++;
                _averageRight = _totalRight / _notesRight;

                _standardDeviationRight=StandardDeviation(_listRight,_averageRight, _notesRight);
            }

            _averageBoth = (_totalLeft + _totalRight) / (_notesLeft + _notesRight);
            _standardDeviationBoth = StandardDeviation(_listBoth, _averageBoth, _notesLeft + _notesRight);

            UpdateText();
        }

        private void UpdateText()
        {
            if (Configuration.Instance.CounterType == Configuration.counterType.Both.ToString())
            {
                if (Configuration.Instance.SeparateSaber)
                {
                    _counterLeft.text = $"{Format(_averageLeft, Configuration.Instance.DecimalPrecision)}\n{Format(_standardDeviationLeft, Configuration.Instance.DecimalPrecision)}";
                    _counterRight.text = $"{Format(_averageRight, Configuration.Instance.DecimalPrecision)}\n{Format(_standardDeviationRight, Configuration.Instance.DecimalPrecision)}";
                }
                else
                {
                    _counterLeft.text = $"{Format(_averageBoth, Configuration.Instance.DecimalPrecision)}\n{Format(_standardDeviationBoth, Configuration.Instance.DecimalPrecision)}";

                }
            }
            else if (Configuration.Instance.CounterType == Configuration.counterType.Distance.ToString())
            {

                if (Configuration.Instance.SeparateSaber)
                {
                    _counterLeft.text = Format(_averageLeft, Configuration.Instance.DecimalPrecision);
                    _counterRight.text = Format(_averageRight, Configuration.Instance.DecimalPrecision);
                }
                else
                {
                    _counterLeft.text = Format(_averageBoth, Configuration.Instance.DecimalPrecision);
                }
            }
            else if (Configuration.Instance.CounterType==Configuration.counterType.StandardDeviation.ToString())
            {
                if (Configuration.Instance.SeparateSaber)
                {
                    _counterLeft.text = Format(_standardDeviationLeft, Configuration.Instance.DecimalPrecision);
                    _counterRight.text = Format(_standardDeviationRight, Configuration.Instance.DecimalPrecision);
                }
                else
                {
                    _counterLeft.text = Format(_standardDeviationBoth, Configuration.Instance.DecimalPrecision);
                }
            }
        }

        private string Format(double CenterDistance,int decimals)
        {
            return CenterDistance.ToString($"F{decimals}", CultureInfo.InvariantCulture);
        }
        private double StandardDeviation(List<double>　distanceList,double average,int notes)
        {
            double beforeDividedTotal=0;
            
            foreach(double distance in distanceList)
            {
                beforeDividedTotal += Math.Pow(distance - average, 2);
            }

            return Math.Sqrt(beforeDividedTotal / notes);
        }

        public override void CounterDestroy() { }

        
    }
}