﻿using System.Security.Cryptography.X509Certificates;

namespace ArduinoRGBLib
{
    namespace States
    {
        public abstract class RgbState
        {
            public abstract MessageOut GetMessageOut();
            public abstract RgbEndpoint Parent { get; }
        }

        // SOLID
        // One constant RGB colour.

        public class Solid : RgbState
        {
            public override RgbEndpoint Parent { get; }
            private byte _red;
            private byte _green;
            private byte _blue;

            public byte Red
            {
                get => _red;
                set {
                    _red = value;
                    Parent.SetLedState(GetMessageOut());
                }
            }
            public byte Green
            {
                get => _green;
                set {
                    _green = value;
                    Parent.SetLedState(GetMessageOut());
                }
            }
            public byte Blue
            {
                get => _blue;
                set {
                    _blue = value;
                    Parent.SetLedState(GetMessageOut());
                }
            }

            public Solid(byte red, byte green, byte blue, RgbEndpoint parent)
            {
                _red = red;
                _green = green;
                _blue = blue;
                Parent = parent;
            }

            public override MessageOut GetMessageOut()
            {
                return new UpdateLedStaticMessage(Red, Green, Blue);
            }
        }


        // GRADIENT
        // Two hues, one saturation and one luminance. Hues are smoothly faded between by the device.

        public class Gradient : RgbState
        {
            public override RgbEndpoint Parent { get; }
            private byte _hue1;
            private byte _hue2;
            private byte _sat;
            private byte _lum;

            public byte Hue1
            {
                get => _hue1;
                set
                {
                    _hue1 = value;
                    Parent.SetLedState(GetMessageOut());
                }
            }
            public byte Hue2
            {
                get => _hue2;
                set
                {
                    _hue2 = value;
                    Parent.SetLedState(GetMessageOut());
                }
            }
            public byte Saturation
            {
                get => _sat;
                set
                {
                    _sat = value;
                    Parent.SetLedState(GetMessageOut());
                }
            }

            public byte Luminance
            {
                get => _lum;
                set
                {
                    _lum = value;
                    Parent.SetLedState(GetMessageOut());
                }
            }

            public Gradient(byte hue1, byte hue2, byte sat, byte lum, RgbEndpoint parent)
            {
                _hue1 = hue1;
                _hue2 = hue2;
                _sat = sat;
                _lum = lum;
                Parent = parent;
            }

            public override MessageOut GetMessageOut()
            {
                return new UpdateLedGradientMessage(Hue1, Hue2, Saturation, Luminance);
            }
        }
    }
}