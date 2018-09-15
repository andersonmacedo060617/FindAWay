using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindAWay.Rules
{
    public class Field
    {
        public int Step { get; set; }
        public int Position { get; set; }
        public bool Visible { get; set; }
        public bool RightWay { get; set; }

        public Field(int step, int position, bool visible, bool rightWay)
        {
            this.Step = step;
            this.Position = position;
            this.Visible = visible;
            this.RightWay = rightWay;
        }
    }
}
