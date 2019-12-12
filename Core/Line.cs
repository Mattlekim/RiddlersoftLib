using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace Riddlersoft.Core
{
    public struct Line
    {
        public Vector2 Start, End;
        public Line(Vector2 start, Vector2 end)
        {
            Start = start;
            End = end;
        }
    }
}
