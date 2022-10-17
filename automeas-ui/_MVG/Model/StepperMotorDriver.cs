﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace automeas_ui._MVG.Model
{
    internal class StepperMotorDriver
    {
        // singleton implementation - thread safe
        private static readonly Lazy<StepperMotorDriver> lazy = new(() => new());
        public static StepperMotorDriver Instance { get { return lazy.Value; } }
        // ctor
        public StepperMotorDriver()
        {
            Step = new()
            {
                9,
                7,
                5,
                3,
                1
            };
            SmallestDelay = 0.5;
        }
        public readonly List<double> Step;
        public readonly double SmallestDelay;
        public double QuantitizeVelocity(double v)
        {
            double last_v = Step[0];
            foreach (var velocity in Step)
            {
                if (velocity < v) { break; }
                last_v = velocity;
            }
            return last_v;
        }
    }
}