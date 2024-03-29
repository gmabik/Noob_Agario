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
    public class Controller
    {
        public Player currentOwner;

        public Controller(Player owner)
        {
            currentOwner = owner;
        }

        private Vector2f positionToGo = new Vector2f(0, 0);
        private Vector2f directionToGo = new Vector2f(0, 0);
        public Vector2f GetMovementDirection()
        {
            directionToGo = new Vector2f(0, 0);
            if (!currentOwner.isBot)
            {
                if (Keyboard.IsKeyPressed(Keyboard.Key.W)) directionToGo += new Vector2f(0, -1);
                if (Keyboard.IsKeyPressed(Keyboard.Key.S)) directionToGo += new Vector2f(0, 1);
                if (Keyboard.IsKeyPressed(Keyboard.Key.A)) directionToGo += new Vector2f(-1, 0);
                if (Keyboard.IsKeyPressed(Keyboard.Key.D)) directionToGo += new Vector2f(1, 0);
                /*if (Mouse.IsButtonPressed(Mouse.Button.Left))
                {
                    PositionToGo = (Vector2f)Mouse.GetPosition();
                    PositionToGo.X -= Radius;
                    PositionToGo.Y -= Radius;
                }
                Vector2f path = PositionToGo - Position;
                Vector2f normalizedPath = normalize(path);
                CheckIfCanMove(normalizedPath); */
            }
            else
            {
                Vector2f position = currentOwner.position;
                if (positionToGo == new Vector2f(0, 0) || 
                    (positionToGo.X - position.X <= 5 && positionToGo.Y - position.Y <= 5))
                {
                    positionToGo = RandomGenerator.GeneratePosition(currentOwner.radius);
                }
                Vector2f direction = positionToGo - position;
                directionToGo = direction.Normalize();
            }
            return directionToGo;
        }

        public bool WantsToChangeControllers()
            => !currentOwner.isBot && Keyboard.IsKeyPressed(Keyboard.Key.R);

        public bool WantsToShoot()
            => !currentOwner.isBot && Mouse.IsButtonPressed(Mouse.Button.Left);
    }
}