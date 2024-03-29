﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Graphics;
using SFML.System;

namespace Noob_Agario
{
    public static class Extensions
    {
        public static Vector2f Normalize(this Vector2f vector)
        {
            float length = (float)Math.Sqrt((vector.X * vector.X) + (vector.Y * vector.Y));
            if (length != 0)
                return new Vector2f(vector.X / length, vector.Y / length);
            return vector;
        }

        public static float CalculateDistance(this float length, Vector2f player1Position, Vector2f player2Position)
        {
            float x = player1Position.X - player2Position.X;
            float y = player1Position.Y - player2Position.Y;
            float distance = (float)Math.Sqrt(x * x + y * y);
            return distance;
        }

        public static bool Intersects(this Shape model1, Shape model2)
            => model1.GetGlobalBounds().Intersects(model2.GetGlobalBounds());

        public static bool BiggerThan(this Player player1, Player player2)
            => player1.radius > player2.radius;
    }
}