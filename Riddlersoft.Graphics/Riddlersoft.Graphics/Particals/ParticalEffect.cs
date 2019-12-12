using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Riddlersoft.Graphics.Particals.Emitters;
namespace Riddlersoft.Graphics.Particals
{
    public class ParticalEffect
    {
        public List<Emitter> Emitters { get; private set; }
        private SpriteBatch _spriteBatch;
        public void Clear()
        {
            foreach (Emitter emitter in Emitters)
                emitter.Clear();
        }

        public void SetEmitterTexture(Texture2D texture, int emitter)
        {
            Emitters[emitter].SetTexture(texture);
        }

        

        public ParticalEffect()
        {
            Emitters = new List<Emitter>();
        }

        public void Trigger(Point position, int amount)
        {
            if (amount == 0)
                return;
            foreach (Emitter em in Emitters)
                em.Trigger(position, amount);
        }

        public void Trigger(Vector2 position, int amount, int velocity)
        {
            if (amount == 0)
                return;
            foreach (Emitter em in Emitters)
            {
                ConeEmitter ce = em as ConeEmitter;
                if (ce != null)
                    ce.ParticalSpeed = velocity;
                em.Trigger(position, amount);
            }
        }

        public void Trigger(Vector2 position, int amount)
        {
            if (amount == 0)
                return;
            foreach (Emitter em in Emitters)
                em.Trigger(position, amount);
        }

        public void AddEmitter(Emitter emitter)
        {
            Emitters.Add(emitter);
        }

        public void KillParticles(Modifyers.KillModifyer kill, float dt)
        {
            foreach (Emitter em in Emitters)
                foreach (Partical p in em.Particals)
                {
                    kill.Processes(p, dt);
                }
        }

        public void Update(float dt)
        {
            foreach (Emitter em in Emitters)
                em.Update(dt);
        }

        public void Render(Vector2 offset)
        {
            foreach (Emitter em in Emitters)
                em.Render(_spriteBatch, offset);
        }

        public void Render(SpriteBatch sb)
        {
            foreach (Emitter em in Emitters)
                em.Render(sb);
        }

        public void Render(SpriteBatch sb, Vector2 offset)
        {
            foreach (Emitter em in Emitters)
                em.Render(sb, offset);
        }


    }
}
