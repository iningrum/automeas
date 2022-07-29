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
        public HexChar Mode2;
        public HexChar Clk1;
        public HexChar Clk0;
        public override string ToString()
        {
            if(Mode2 == null || Clk1==null || Clk0 == null)
                throw new NotSupportedException();
            return $"{Mode2.Value}{Clk1.Value}{Clk0.Value}";
        }
    }
    internal class Line
    {
        public Instruction GetInstruction(DataPoint current)
        {
            int step_number = -1;
            double clk_freq = -1;
            Instruction result = new Instruction();
            { // 1. find best ||step_number|| for achieving desired velocity
                List<Step> steps = StepperMotorModel.Steps;
                steps.Reverse();
                for (int i=0; i<steps.Count; i++)
                {
                    if (steps[i].Velocity >= current.Velocity)
                    {
                        step_number = i;
                        break;
                    }
                }
            }
            { // 2. find requested clk freq
                clk_freq = (180 * current.Velocity) / (Math.PI*StepperMotorModel.Max.StepAngle);
            }
            { // map step_number to instruction
                // assuming device only moves right for now
                step_number = (step_number)*2; // corresponds to bit shift left
                step_number += 1; // 1 = right, 0 = left
                result.Mode2 = new HexChar(step_number);
            }
            { // map frequency
              // very sloppy solution - just accept range 0-255
              if (clk_freq<0 || clk_freq > 255)
              {
                    throw new NotSupportedException();
              }
              else
              {
                    int f = (int)clk_freq;
                    { // confert the result to two hexes
                        string binary = Convert.ToString(f, 2);
                        string first, second;
                        first = binary.Substring(0, 4);
                        second = binary.Substring(4, 4);
                        int a = Convert.ToInt32(first, 2);
                        int b = Convert.ToInt32(second, 2);
                        result.Clk1 = new HexChar(a);
                        result.Clk0 = new HexChar(b);
                    }
              }
            }
            return result;
        }
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