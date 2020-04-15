﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Riddlersoft.Graphics.Text.Modifyers;

namespace Riddlersoft.Graphics.Text
{
    public class StringEffect
    {
        private string _text;
        internal List<TextChar> _chars = new List<TextChar>();
        private SpriteFont _font;

        public Vector2 Position;

        public int TextWidth { get; private set; }
        public int TextHeight { get; private set; }

        public int MaxWidth = 200;

        private List<TextModifyer> _modifyers = new List<TextModifyer>();

        public bool _effectBuilt = false;

        public int LetterCount { get { return _chars.Count; } }

        public bool Shadown = true;

        public float Scale = 1;

        public float Speed = 1;

        public string debug;

        private List<LifeTimeTrigger> _lifetimeTriggers = new List<LifeTimeTrigger>();

        public Action<string> OnModifyerDeactivated;

        private List<Decoders.CharData> _colData = null;

        public void AddEventToTime(Action action, float time)
        {
            _lifetimeTriggers.Add(new LifeTimeTrigger()
            {
                Trigger = action,
                Time = time,
            });
        }

        public StringEffect(SpriteFont font, string text, Vector2 pos)
        {
            //text = text.Insert(3, "#c0-15");
            Decoders.ColorDecoder cd = new Decoders.ColorDecoder();
            _colData = cd.Decode(text, out _text);
            _font = font;

//            _text = text;
            Position = pos;
        }

        public void DisableModifyer(string tag)
        {
            foreach (TextModifyer mod in _modifyers)
                if (mod.Tag == tag)
                    mod.Disable();
        }

       
        private void OnModifyerDisable(string s)
        {
            if (OnModifyerDeactivated != null)
                OnModifyerDeactivated(s);
        }

        public void AddModifyer(TextModifyer mod)
        {
            if (_effectBuilt)
                throw new Exception("modifyers can not be added after the effect has been built");
            mod.OnDisabled += OnModifyerDisable;
            _modifyers.Add(mod);
            
        }

        private int charToBreakAd(int startIndex, float offset)
        {
            float width = -offset;
            for (int i = 0; i < _text.Length; i++)
            {
                if (width >= MaxWidth) //now we are need to break at previous space
                {
                    for (int c = i; c >= 0; c--)
                    {
                        if (_text[c] == ' ')
                            return c + 1;
                    }
                    return _text.Length;
                }
                width += _font.MeasureString(_text[i].ToString()).X * Scale;
            }
            return _text.Length;
        }

        public void BuildEffect()
        {
           

            _chars = new List<Text.TextChar>();

            float textOffsetX = 0;
            float xNeg = 0;
            float textOffsetY = 0;
            for (int i =0; i<_text.Length;i++)
            {
                int w = charToBreakAd(i, xNeg);
                if (i == w)
                {
                    xNeg += textOffsetX;
                    textOffsetX = 0;
                    textOffsetY += 30;
                }

                _chars.Add(new Text.TextChar(_text[i], new Vector2(textOffsetX, textOffsetY)));
                textOffsetX += _font.MeasureString(_text[i].ToString()).X * Scale;
                _chars[_chars.Count - 1].Scale = Scale;
                _chars[_chars.Count - 1].SetUp(_font);
            }

            if (_colData != null)
            {
                int start;
                int lenght;
                foreach (Decoders.CharData cd in _colData)
                {
                    start = cd.StartIndex;
                    lenght = cd.Count;
                    if (start + lenght >= _chars.Count)
                        lenght = _chars.Count - start;

                    for (int i = start; i < start + lenght; i++)
                        _chars[i].Colour = cd.Colour;
                }
            }
            for (int i =0; i < _chars.Count; i++)
                foreach (TextModifyer mod in _modifyers)
                    mod.Apply(_chars[i], this, i);

            _effectBuilt = true;

            float width = 0;
            float height = 0;
            foreach (TextChar t in _chars)
            {
                if (t.Position.X > width)
                    width = t.Position.X;

                if (t.Position.Y > height)
                    height = t.Position.Y;
            }

            width += 20;
            width -= Position.X;
            height += 20;
            height -= Position.Y;

            TextWidth = Convert.ToInt32(width);
            TextHeight = Convert.ToInt32(height);
        }

        public void Update(float dt)
        {
            dt *= Speed;
            for (int i = _chars.Count - 1; i >= 0; i--)
            {
                _chars[i].Update(dt);
                foreach (TextModifyer mod in _modifyers)
                    mod.Update(_chars[i],  i);

            }

            foreach (TextModifyer mod in _modifyers)
                mod.UpdateModifyer(this);

            for (int i = 0; i < _lifetimeTriggers.Count; i++)
                if (!_lifetimeTriggers[i].Used)
                    if (_lifetimeTriggers[i].Time <= _chars[0].LifeTime)
                    {
                        _lifetimeTriggers[i].Used = true;
                        _lifetimeTriggers[i].Trigger();
                    }
        }

        public void Draw(SpriteBatch sb)
        {
            //  sb.DrawString(_font, _text, Vector2.Zero, Color.White);

            foreach (TextChar c in _chars)
            {
                if (c.Sprite == null)
                {
                    sb.DrawString(_font, c.character, c.Position + Position + new Vector2(2), Color.Black * c.Opacity,
                       c.Rotation, c.Center, c.Scale, SpriteEffects.None, 0f);

                    sb.DrawString(_font, c.character, c.Position + Position, c.Colour * c.Opacity,
                        c.Rotation, c.Center, c.Scale, SpriteEffects.None, 0f);
                }
                else
                {
                    sb.Draw(c.Sprite, c.Position, null, c.Colour, c.Rotation, c.Center, c.Scale, SpriteEffects.None, 0f);
                }

            }
        }
    }
}
