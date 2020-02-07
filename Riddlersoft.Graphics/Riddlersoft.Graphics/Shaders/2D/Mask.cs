using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Riddlersoft.Graphics.Shaders._2D
{
    public class Mask : Effect
    {
        private Texture2D _maskTexture;

        public Texture2D MaskTexture
        {
            get { return _maskTexture; }
            set
            {
                _maskTexture = value;
                Parameters["Mask"].SetValue(value);
            }
        }

        EffectParameter MatrixTransform;

        private const string _path = "Shaders\\2D\\Mask";


        public bool _enableMask = true;

        public bool EnabledMask
        {
            get { return _enableMask; }
            set
            {
                _enableMask = value;
             //   Parameters["Enabled"].SetValue(value);
            }
        }

        public Mask(Effect source) :
            base(source)
        {
            MatrixTransform = Parameters["MatrixTransform"];
        }

        public static Mask Load(ContentManager content)
        {
            return new _2D.Mask(content.Load<Effect>(_path));
        }

    }
}
