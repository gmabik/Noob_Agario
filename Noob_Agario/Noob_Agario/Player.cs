﻿using System;
using SFML.Window;
using SFML.Graphics;
using SFML.System;
namespace Noob_Agario
{
    public class Player
    {
        private RenderWindow _window;

        public CircleShape playerModel;

        public Vector2f Position()
            => playerModel.Position;

        public float Radius()
            => playerModel.Radius;

        public Text name;
        public bool isAlive;
        public bool isBot;

        private int starterRadius = 30;
        private float speed = 4f;

        public Controller controller;

        public Player(RenderWindow window, string playerName, bool isABot)
        {
            _window = window;

            playerModel = ObjectCreator.CreateCircle(starterRadius);
            isAlive = true;
            isBot = isABot;

            controller = ObjectCreator.GenerateController(this);

            name = ObjectCreator.CreateText(playerName, (uint)(playerModel.Radius * 0.5f));
            name.Position = new Vector2f(playerModel.Position.X + playerModel.Radius * 0.7f, playerModel.Position.Y + playerModel.Radius * 0.7f);
        }

        public void InputLogic(Player[] players)
        {
            Vector2f movePosition = controller.GetInput();
            CheckIfCanMove(movePosition);

            if (controller.WantsToChangeControllers()) ChangeControllersWithBot(players);
        }

        private void CheckIfCanMove(Vector2f path)
        {
            if ((playerModel.Position.X - speed < 0) && path.X < 0) return;
            if ((playerModel.Position.X + playerModel.Radius * 2 + speed > _window.Size.X) && path.X > 0) return;

            if ((playerModel.Position.Y - speed < 0) && path.Y < 0) return;
            if ((playerModel.Position.Y + playerModel.Radius * 2 + speed > _window.Size.Y) && path.Y > 0) return;
            Move(path);
        }

        private void Move(Vector2f path)
        {
            Vector2f newPosition = playerModel.Position + path * speed;
            playerModel.Position = newPosition;
            name.Position = playerModel.Position + new Vector2f(playerModel.Radius * 0.7f, playerModel.Radius * 0.7f);
        }

        public bool CheckIfCanEatPlayer(Player[] players)
        {
            foreach (Player player in players)
            {
                if(player != this && playerModel.Intersects(player.playerModel) && Radius() > player.Radius())
                {
                    playerModel.Radius += player.playerModel.Radius / 2;
                    player.Die();
                    return true;
                }
            }
            return false;
        }

        private void Die()
        {
            playerModel.Radius = 0;
            name.DisplayedString = "";
            isAlive = false;
        }

        public void TryEatFood(Food[] foods)
        {
            foreach (Food food in foods)
            {
                if (playerModel.Intersects(food.foodModel))
                {
                    playerModel.Radius += 2;
                    food.Relocate();
                }
            }
        }

        private void ChangeControllersWithBot(Player[] players)
        {
            Player closestBot = null;
            float closestLength = float.MaxValue;
            foreach (Player player in players)
            {
                float length = 0;
                length.CalculateDistance(player.playerModel.Position, playerModel.Position);
                if (length < closestLength && player != this && player.isAlive)
                {
                    closestBot = player;
                    closestLength = length;
                }
            }
            (closestBot.controller, controller) = (controller, closestBot.controller);
        }
    }
}