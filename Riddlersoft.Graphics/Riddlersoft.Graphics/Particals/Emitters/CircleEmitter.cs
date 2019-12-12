using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace Riddlersoft.Graphics.Particals.Emitters
{
    public class CircleEmitter : Emitter
    {
        public Rectangle Area { get; private set; }

        public float Radius { get; set; }
        public FloatRange ParticalSpeed = 1;
        public EmmiterFunction Function { get; set; }

        public CircleEmitter(float radius): base()
        {
            Radius = radius;
        }

        protected override void TriggerParticals(Vector2 pos, int amount)
        {
            for (int i=0; i < amount; i++)
            {
                Partical p = GenerateParticalProperties(
                    Vector2.Transform(new Vector2(_random.Next(0, (int)Radius), 0),
                    Matrix.CreateRotationZ((float)_random.NextDouble() * MathHelper.TwoPi)));
                switch (Function)
                {
                    case EmmiterFunction.Explosive:
                        p.Velocity = p.Position;
                        p.Velocity.Normalize();
                        p.Velocity *= ParticalSpeed.GetValue();
                        break;

                    case EmmiterFunction.Implosive:
                        p.Velocity = -p.Position;
                        p.Velocity.Normalize();
                        p.Velocity *= ParticalSpeed.GetValue();
                        break;
                }
                p.Position = p.Position + pos;
                Particals.Add(p);
            }
        }

       
    }
}
