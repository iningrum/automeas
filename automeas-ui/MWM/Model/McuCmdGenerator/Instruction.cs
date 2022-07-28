using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace automeas_ui.MWM.Model.McuCmdGenerator
{
    /*
     *  Contains definitions of all possible instructions
     */
    internal class HexChar
    {
        public HexChar(int val) => Value = ToChar(Value);
        public HexChar(char val)
        {
            Value = ToChar(ToInt(val));
        }
        // attr
        public readonly char Value;
        // statics
        public static char ToChar(int value)
        {
            if (value < 0)
            {
                return '-';
            }
            else if (value > 15)
            {
                return 'F';
            }
            return Convert.ToString(value, 16)[0];
        }
        public static int ToInt(char value)
        {
            switch (value)
            {
                case '0': return 0;
                case '1': return 1;
                case '2': return 2;
                case '3': return 3;
                case '4': return 4;
                case '5': return 5;
                case '6': return 6;
                case '7': return 7;
                case '8': return 8;
                case '9': return 9;
                case 'A': return 10;
                case 'B': return 11;
                case 'C': return 12;
                case 'D': return 13;
                case 'E': return 14;
                case 'F': return 15;
                case 'a': return 10;
                case 'b': return 11;
                case 'c': return 12;
                case 'd': return 13;
                case 'e': return 14;
                case 'f': return 15;
                case '-': return -1;
                default: throw new NotSupportedException();

            }
        }
    }
    internal class Instruction
    {
        public Instruction(DataPoint prevoius, DataPoint current)
        {

        }
    }
    internal class Line
    {
        // ctor
        public Line(DataPoint previous, DataPoint current)
        {
            double requested_acceleration = (current.Velocity-previous.Velocity)/(current.Time-previous.Time);
            double requested_velocty = current.Velocity;
            /*
             *  Mode mode = GetClosestMode(requested_acceleration, requested_velocity);
             *  double frequency = 360requested_acceleration/2pir
             *  convert this into two commands
             */
        }
        // attr
        public readonly DataPoint A;
        public readonly DataPoint B;
    }
    internal class DataPoint
    {
        // ctor
        public DataPoint(double velocity, double time)
        {
            { // handle velocity
                if (velocity > StepperMotorModel.Max.Velocity)
                    velocity = StepperMotorModel.Max.Velocity;
                else if (velocity < 0)
                    velocity = 0;
            }
            { // handle time
                if (time < 0)
                    time = 0;
            }
            Velocity = velocity;
            Time = time;

        }
        // attr
        public readonly double Velocity;
        public readonly double Time;
    }

}