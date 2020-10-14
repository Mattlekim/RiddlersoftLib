using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace Riddlersoft.Core.Extentions
{
    public static class IntExtension
    {
        /// <summary>
        /// Shrinks the rectangle by the given amount and returns a new rectangle
        /// </summary>
        /// <param name="source"></param>
        /// <param name="amountInPx">the amount of pixels to shrink on each side</param>
        /// <returns></returns>
        public static Rectangle Shrink(this Rectangle source, int amountInPx)
        {
            return new Rectangle(source.X + amountInPx, source.Y + amountInPx, source.Width - amountInPx * 2, source.Height - amountInPx * 2);
        }

        /// <summary>
        /// Grows the rectangle by the given amount and returns a new rectangle
        /// </summary>
        /// <param name="source"></param>
        /// <param name="amountInPx">the amount of pixels to grow the rectangle by on each side</param>
        /// <returns></returns>
        public static Rectangle Grow(this Rectangle source, int amountInPx)
        {
            return new Rectangle(source.X - amountInPx, source.Y - amountInPx, source.Width + amountInPx * 2, source.Height + amountInPx * 2);
        }
    }
}


