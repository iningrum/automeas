using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace automeas_ui.MWM.Model.McuCmdGenerator
{
    internal class Step
    {
        public Step(double v, double a, double sps)
        {
            Velocity = v;
            Acceleration = a;
            StepsPerSecond = sps;
        }
        public readonly double Velocity;
        public readonly double Acceleration;
        public readonly double StepsPerSecond;
    }
    internal static class StepperMotorModel
    {
        public struct Max
        {
            public const double Velocity = 2.5;
            public const double Acceleration = 1.2;
            public const int StepsPerSecond = 3;
            public const double StepAngle = 1.8;
        }
        public const double Radius = 3;
        public static List<Step> Steps  = new List<Step>{
            new Step(2.5,1.2,3), // full
            new Step(2,1,3.1),   // 1/2
            new Step(1,0.5,3.5)  // 1/4
            };
    }

}
