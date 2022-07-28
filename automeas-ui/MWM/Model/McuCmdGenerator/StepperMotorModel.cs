using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace automeas_ui.MWM.Model.McuCmdGenerator
{
    internal static class StepperMotorModel
    {
        public struct Max
        {
            public const double Velocity = 2.5;
            public const double Acceleration = 1.2;
            public const int StepsPerSecond = 3;
        }
        public struct Full
        {
            public struct Max
            {
                public const double Velocity = 2.5;
                public const double Acceleration = 1.2;
                public const int StepsPerSecond = 3;
            }
        }
        public struct Half
        {
            public struct Max
            {
                public const double Velocity = 2;
                public const double Acceleration = 0.8;
                public const int StepsPerSecond = 3;
            }
        }
        public struct Quarter
        {
            public struct Max
            {
                public const double Velocity = 1.4;
                public const double Acceleration = 0.2;
                public const int StepsPerSecond = 3;
            }
        }
        public struct Eighth
        {
            public struct Max
            {
                public const double Velocity = 2.5;
                public const double Acceleration = 1.2;
                public const int StepsPerSecond = 3;
            }
        }
        public struct Sixteenth
        {
            public struct Max
            {
                public const double Velocity = 2.5;
                public const double Acceleration = 1.2;
                public const int StepsPerSecond = 3;
            }
        }
        public struct ThirtySecond
        {
            public struct Max
            {
                public const double Velocity = 2.5;
                public const double Acceleration = 1.2;
                public const int StepsPerSecond = 3;
            }
        }
    }

}
