using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindAWay.Rules
{
    public class Board
    {
        public int Steps { get; set; }
        public int FieldsBySteps { get; set; }
        public int RightWayBySteps { get; set; }
        public IList<Field> Fields { get; set; }
        

        public Board(int steps, int fieldsBySteps, int rightWayBySteps)
        {
            this.Steps = steps;
            this.FieldsBySteps = fieldsBySteps;
            this.RightWayBySteps = rightWayBySteps;
            this.Fields = new List<Field>();
            StartsBoard();
        }

        public void StartsBoard()
        {
            for (int step = 0; step < Steps; step++)
            {
                for (int position = 0; position < FieldsBySteps; position++)
                    this.Fields.Add(new Field(step, position, false, false));

                CreatePath(step);
            }   
        }

        public void CreatePath(int step)
        {
            int position;
            while (NumRightWayInserted(step) < RightWayBySteps)
            {
                Random random = new Random();
                position = random.Next(FieldsBySteps);

                Fields.Where(x => x.Position == position && x.Step == step).FirstOrDefault().RightWay = true;
            }
        }

        public int NumRightWayInserted(int step)
        {
            int count = 0;
            foreach (var item in Fields.Where(field => field.Step == step))
                if (item.RightWay)
                    count++;
            return count;
        }

    }
}
