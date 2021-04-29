using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bowling.Api
{
    public class Game
    {
        public const int UnDeterminedScore = -1;

        public List<Frame> Frames { get; set; } = new List<Frame>();
        public bool IsGameCompleted
        {
            get { return Frames.Count == FrameOrder.Max && Frames[FrameOrder.Max - 1].IsCompleted ? true : false; }
        }

        public Game(List<int> pinsDowned)
        {
            int frameOrder = 1; //The order of a frame within a game, value range 1 to 10
            int RollNumber = 1; //The number of roll within a frame, value range 1 to 3
            var frame = new Frame(frameOrder);

            for (int i = 0; i < pinsDowned.Count; i++)
            {
                if (frame.AddRoll(pinsDowned[i], RollNumber))
                {
                    if (frame.IsCompleted)
                    {
                        //frame completed:
                        //1. Add frame to Game
                        //2. Reset roll number
                        //3. Create a new frame
                        Frames.Add(frame);
                        RollNumber = 1;
                        frameOrder++;
                        if (frameOrder <= FrameOrder.Max)
                        {
                            frame = new Frame(frameOrder);
                        }
                    }
                    else
                    {
                        //frame not completeted, add more roll
                        RollNumber++;
                    }
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }

            }

            CalculateScroe();

        }

        private void CalculateScroe()
        {
            var currentScore = 0;

            for (int i = 0; i < Frames.Count; i++)
            {
                if (!Frames[i].IsCompleted)
                {
                    Frames[i].Score = UnDeterminedScore;
                }
                else
                {
                    if (Frames[i].Order == FrameOrder.Max) //Calculate score for frame 10
                    {
                        currentScore = currentScore + Frames[i].FirstRoll + Frames[i].SecondRoll + Frames[i].ThirdRoll;
                        Frames[i].Score = currentScore;
                    }
                    else //Calculate score for frame 1-9
                    {
                        if (Frames[i].IsStrike) //Caculate strike frame
                        {
                            if (i + 1 < Frames.Count)
                            {
                                if (Frames[i + 1].NumberOfRolls >= 2) //Frames[i + 1] has 2 or more rolls
                                {
                                    currentScore = currentScore + Frames[i].FirstRoll + Frames[i].SecondRoll + Frames[i + 1].FirstRoll + Frames[i + 1].SecondRoll;
                                    Frames[i].Score = currentScore;
                                }
                                else if (i + 2 < Frames.Count) //Frames[i + 1] is strike and Frames[i + 2] has 1 roll at least
                                {
                                    currentScore = currentScore + Frames[i].FirstRoll + Frames[i].SecondRoll + Frames[i + 1].FirstRoll + Frames[i + 2].FirstRoll;
                                    Frames[i].Score = currentScore;
                                }
                                else
                                {
                                    Frames[i].Score = UnDeterminedScore;
                                }
                            }
                            else //No more frames, can't determine current frame score
                            {
                                Frames[i].Score = UnDeterminedScore;
                            }

                        }
                        else if (Frames[i].IsSpare) //Caculate Spare frame
                        {
                            if (i + 1 < Frames.Count)
                            {
                                currentScore = currentScore + Frames[i].FirstRoll + Frames[i].SecondRoll + Frames[i + 1].FirstRoll;
                                Frames[i].Score = currentScore;
                            }
                            else
                            {
                                Frames[i].Score = UnDeterminedScore;
                            }
                        }
                        else //Caculate score for frames with total pinsdowned less than 10
                        {
                            currentScore = currentScore + Frames[i].FirstRoll + Frames[i].SecondRoll;
                            Frames[i].Score = currentScore;
                        }
                    }
                }
            }
        }
    }
}
