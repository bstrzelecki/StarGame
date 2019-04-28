﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarGame
{
    class JumpAssistant
    {
        public static bool IsPlayerInVaidPosition(Vector2 destinationStarPosition)
        {
            Vector2 playerPos = MainScene.player.position;
            if (Vector2.Distance(playerPos, MainScene.sun.position) < GetLastPlanet(MainScene.sun)) return false;

            Vector2 starPos = StarMap.playerStar.Position;
            Vector2 deltaPos = destinationStarPosition - starPos;

            Vector2 playerDeltaPos = MainScene.player.position - MainScene.sun.position;
            deltaPos.Normalize();
            playerDeltaPos.Normalize();
            
            double angle2 = Math.Atan2(-deltaPos.Y, deltaPos.X);
            double pangle2 = Math.Atan2(-playerDeltaPos.Y, playerDeltaPos.X);
            angle2 = Input.GetDegree((float)angle2);
            pangle2 = Input.GetDegree((float)pangle2);
            if (Math.Abs(angle2 - pangle2) > 90)
            {
                return false;
            }
            return true;
        }
        public static bool DeductPower(int cap)
        {
            if(MainScene.barArray.GetResource("power") > cap * .9f)
            {
                MainScene.barArray.SubtractResource("power", cap * .9f);
                return true;
            }
            return false;
        }
        public static void Jump()
        {
            Vector2 newPlayerPos = MainScene.sun.position - MainScene.player.position;
            newPlayerPos.Normalize();
            RandomSpaceGenerator rsg = new RandomSpaceGenerator();
            MainScene.sun = rsg.Build();
            newPlayerPos *= GetLastPlanet(MainScene.sun) + 500;
            MainScene.player.position = newPlayerPos;
        }
        private static float GetLastPlanet(StarSystem system)
        {
            float max = int.MinValue;
            foreach(Planet pl in system.planets)
            {
                max = (pl.distance > max) ? pl.distance : max;
            }
            return max;
        }
    }
}
