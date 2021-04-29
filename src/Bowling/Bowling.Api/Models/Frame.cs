using System;

namespace Bowling.Api
{
    public class Frame
    {
        private int _Order;
        public int Order //Order of Frame, between 1 and 10.
        {
            get { return _Order; }
            set
            {
                if (value < FrameOrder.min || value > FrameOrder.Max)
                {
                    throw new ArgumentOutOfRangeException();
                }

                _Order = value;
            }
        }

        private int _firstRoll;
        public int FirstRoll
        {
            get { return _firstRoll; }
            set
            {
                if (value < PinsDowned.Min || value > PinsDowned.Max)
                {
                    throw new ArgumentOutOfRangeException();
                }

                _firstRoll = value;
            }
        }

        private int _secondRoll;
        public int SecondRoll
        {
            get { return _secondRoll; }
            set
            {
                if (Order == FrameOrder.Max)
                {
                    if (value < PinsDowned.Min || value > PinsDowned.Max)
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                }
                else
                {
                    if (value < PinsDowned.Min || value > (PinsDowned.Max - FirstRoll))
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                }

                _secondRoll = value;
            }
        }

        private int _thirdRoll;
        public int ThirdRoll
        {
            get { return _thirdRoll; }
            set
            {
                if (Order != FrameOrder.Max || value < PinsDowned.Min || value > PinsDowned.Max)
                {
                    throw new ArgumentOutOfRangeException();
                }

                if (FirstRoll == PinsDowned.Max || (FirstRoll + SecondRoll) == PinsDowned.Max)
                {
                    _thirdRoll = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        //Score will be set as -1 if can not be determined
        public int Score { get; set; }

        public bool IsCompleted { get; set; }

        public bool IsStrike
        {
            get { return FirstRoll == PinsDowned.Max ? true : false; }
        }

        public bool IsSpare
        {
            get { return (FirstRoll != PinsDowned.Max && (FirstRoll + SecondRoll) == PinsDowned.Max) ? true : false; }
        }

        //Number of rolls happend in this frame
        public int NumberOfRolls { get; private set; }

        public Frame()
        {

        }

        public Frame(int frameOrder)
        {
            Order = frameOrder;
        }

        public bool AddRoll(int pinDowned, int rollNumber)
        {
            var result = false;

            switch (rollNumber)
            {
                case 1:
                    FirstRoll = pinDowned;

                    if (Order != FrameOrder.Max && this.IsStrike)
                    {
                        IsCompleted = true;
                    }
                    else
                    {
                        IsCompleted = false;
                    }

                    NumberOfRolls = rollNumber;
                    result = true;
                    break;

                case 2:
                    SecondRoll = pinDowned;

                    if (Order == FrameOrder.Max && (IsStrike || IsSpare))
                    {
                        //For frame 10, if 
                        IsCompleted = false;
                    }
                    else
                    {
                        IsCompleted = true;
                    }

                    NumberOfRolls = rollNumber;
                    result = true;
                    break;

                case 3:
                    ThirdRoll = pinDowned;
                    IsCompleted = true;
                    NumberOfRolls = rollNumber;
                    result = true;
                    break;

                default:
                    result = false;
                    break;
            }

            return result;
        }

    }
}
