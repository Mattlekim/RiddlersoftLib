using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace Riddlersoft.Graphics.Particals.Emitters
{
    public class ConeEmitter : Emitter
    {
        public Rectangle Area { get; private set; }

        public float Radius { get; set; }
        public float ParticalSpeed = 1;
        
        public EmmiterFunction Function { get; set; }
        public float MinRadious = 0;
        public float MaxRadious = MathHelper.TwoPi;

        public ConeEmitter(float radius): base()
        {
            Radius = radius;
        }

        protected override void TriggerParticals(Vector2 pos, int amount)
        {
            for (int i=0; i < amount; i++)
            {
                Partical p = GenerateParticalProperties(
                    Vector2.Transform(new Vector2(_random.Next(0, (int)Radius), 0),
                    Matrix.CreateRotationZ((float)_random.NextDouble() * (MaxRadious - MinRadious) + MinRadious)));
                switch (Function)
                {
                    case EmmiterFunction.Explosive:
                        p.Velocity = p.Position;
                        p.Velocity.Normalize();
                        p.Velocity *= ParticalSpeed;
                        break;

                    case EmmiterFunction.Implosive:
                        p.Velocity = -p.Position;
                        p.Velocity.Normalize();
                        p.Velocity *= ParticalSpeed;
                        break;
                }
                p.Position = p.Position + pos;
                Particals.Add(p);
            }
        }

       
    }
}
