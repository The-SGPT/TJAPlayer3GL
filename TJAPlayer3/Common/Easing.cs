﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TJAPlayer3;
using FDK;

namespace TJAPlayer3
{
    class Easing
    {
        public int EaseIn(CCounter counter, int startPoint, int endPoint, CalcType type)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
            Sa = EndPoint - StartPoint;
            TimeMs = counter.n終了値;
            Type = type;
            CounterValue = counter.n現在の値;

            switch (Type)
            {
                case CalcType.Quadratic: //Quadratic
                    CounterValue /= TimeMs;
                    Value = Sa * CounterValue * CounterValue + StartPoint;
                    break;
                case CalcType.Cubic: //Cubic
                    CounterValue /= TimeMs;
                    Value = Sa * CounterValue * CounterValue * CounterValue + StartPoint;
                    break;
                case CalcType.Quartic: //Quartic
                    CounterValue /= TimeMs;
                    Value = Sa * CounterValue * CounterValue * CounterValue * CounterValue + StartPoint;
                    break;
                case CalcType.Quintic: //Quintic
                    CounterValue /= TimeMs;
                    Value = Sa * CounterValue * CounterValue * CounterValue * CounterValue * CounterValue + StartPoint;
                    break;
                case CalcType.Sinusoidal: //Sinusoidal
                    Value = -Sa * Math.Cos(CounterValue / TimeMs * (Math.PI / 2)) + Sa + StartPoint;
                    break;
                case CalcType.Exponential: //Exponential
                    Value = Sa * Math.Pow(2, 10 * (CounterValue / TimeMs - 1)) + StartPoint;
                    break;
                case CalcType.Circular: //Circular
                    CounterValue /= TimeMs;
                    Value = -Sa * (Math.Sqrt(1 - CounterValue * CounterValue) - 1) + StartPoint;
                    break;
            }

            return (int)Value;
        }
        public int EaseOut(CCounter counter, int startPoint, int endPoint, CalcType type)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
            Sa = EndPoint - StartPoint;
            TimeMs = counter.n終了値;
            Type = type;
            CounterValue = counter.n現在の値;

            switch (Type)
            {
                case CalcType.Quadratic: //Quadratic
                    CounterValue /= TimeMs;
                    Value = -Sa * CounterValue * (CounterValue - 2) + StartPoint;
                    break;
                case CalcType.Cubic: //Cubic
                    CounterValue /= TimeMs;
                    CounterValue--;
                    Value = Sa * (CounterValue * CounterValue * CounterValue + 1) + StartPoint;
                    break;
                case CalcType.Quartic: //Quartic
                    CounterValue /= TimeMs;
                    CounterValue--;
                    Value = -Sa * (CounterValue * CounterValue * CounterValue * CounterValue - 1) + StartPoint;
                    break;
                case CalcType.Quintic: //Quintic
                    CounterValue /= TimeMs;
                    CounterValue--;
                    Value = Sa * (CounterValue * CounterValue * CounterValue * CounterValue * CounterValue + 1) + StartPoint;
                    break;
                case CalcType.Sinusoidal: //Sinusoidal
                    Value = Sa * Math.Sin(CounterValue / TimeMs * (Math.PI / 2)) + StartPoint;
                    break;
                case CalcType.Exponential: //Exponential
                    Value = Sa * (-Math.Pow(2, -10 * CounterValue / TimeMs) + 1) + StartPoint;
                    break;
                case CalcType.Circular: //Circular
                    CounterValue /= TimeMs;
                    CounterValue--;
                    Value = Sa * Math.Sqrt(1 - CounterValue * CounterValue) + StartPoint;
                    break;
            }

            return (int)Value;
        }

        private int StartPoint;
        private int EndPoint;
        private int Sa;
        private int TimeMs;
        private CalcType Type;
        private double CounterValue;
        private double Value;
        public enum CalcType
        {
            Quadratic,
            Cubic,
            Quartic,
            Quintic,
            Sinusoidal,
            Exponential,
            Circular
        }
    }
}