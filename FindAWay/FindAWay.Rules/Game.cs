using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindAWay.Rules
{
    public class Game
    {
        public Board GameBoard { get; set; }
        public EDifficulty Difficulty { get; set; }
        public int StepGamer { get; set; }

        public bool GameOver()
        {
            foreach (var item in GameBoard.Fields)
            {
                if (item.Visible && !item.RightWay)
                    return true;
            }

            return false;
        }

        public bool GamerWin()
        {
            if (!GameOver())
            {
                foreach (var item in GameBoard.Fields.Where(x=>x.Step == (GameBoard.Steps -1)))
                {
                    if (item.Visible && item.RightWay)
                        return true;
                }
            }

            return false;
        }

        public Game(EDifficulty difficulty = EDifficulty.Normal)
        {
            Difficulty = difficulty;
            if (Difficulty == EDifficulty.Easy)
                this.GameBoard = new Board(3, 3, 2);

            if (Difficulty == EDifficulty.Normal)
                this.GameBoard = new Board(5, 4, 1);

            if (Difficulty == EDifficulty.Hard)
                this.GameBoard = new Board(7, 5, 1);

            StepGamer = 0;
        }

        public static EDifficulty StrDifficultyToEnum(string difficulty)
        {
            if (difficulty.Equals("Easy"))
                return EDifficulty.Easy;

            if (difficulty.Equals("Hard"))
                return EDifficulty.Hard;

            return EDifficulty.Normal;

        }

        public bool StepLiberado(int step)
        {
            if (step == StepGamer)
                return true;

            return false;
        }

        public void SelectField(int step, int position)
        {
            this.GameBoard.Fields.Where(x => x.Step == step && x.Position == position).FirstOrDefault().Visible = true;
            this.StepGamer++;
        }
    }

    public enum EDifficulty
    {
        Easy = 0,
        Normal = 1,
        Hard = 2
    }
}
