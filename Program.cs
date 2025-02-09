﻿using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace CubeConundrum
{
    class Program
    {
        static void Main()
        {
            
            string path = "C:\\Users\\rdelrosario\\Desktop\\Day2.txt";
            string content = File.ReadAllText(path);
            string[] lines = content.Split('\n');

            int summaryGameIds = CalculateGameIds(lines);
            Console.WriteLine("This is Day 2 of Cube Conundrum (Part 1): The sum of the IDs of the possible games is: " + summaryGameIds);

            int summaryPower = CalculateSum(lines);
            Console.WriteLine("This is Day 2 of Cube Conundrum (Part 2): The sum of the power of the minimum set of cubes is: " + summaryPower);
        }

        static Dictionary<string, int> ProcessRound(string round)
        {
            var colorCounts = new Dictionary<string, int> { { "red", 0 }, { "green", 0 }, { "blue", 0 } };
            var cubes = round.Split(',');
            foreach (var cube in cubes)
            {
                var parts = cube.Trim().Split(' ');
                var count = int.Parse(parts[0]);
                var color = parts[1];
                colorCounts[color] = Math.Max(colorCounts[color], count);
            }
            return colorCounts;
        }

        static int CalculateGameIds(string[] lines)
        {
            int sum = 0;
            foreach (string line in lines)
            {
                var gameParts = line.Split(':');
                var gameId = int.Parse(gameParts[0].Replace("Game ", ""));
                var rounds = gameParts[1].Split(';');

                bool isPossible = true;
                foreach (var round in rounds)
                {
                    var colorCounts = ProcessRound(round);
                    if (colorCounts["red"] > 12 || colorCounts["green"] > 13 || colorCounts["blue"] > 14)
                    {
                        isPossible = false;
                        break;
                    }
                }

                if (isPossible)
                {
                    sum += gameId;
                }
            }
            return sum;
        }

        static int CalculateSum(string[] lines)
        {
            int summaryPower = 0;
            foreach (string line in lines)
            {
                var gameParts = line.Split(':');
                var rounds = gameParts[1].Split(';');

                int maxRed = 0, maxGreen = 0, maxBlue = 0;
                foreach (var round in rounds)
                {
                    var colorCounts = ProcessRound(round);
                    maxRed = Math.Max(maxRed, colorCounts["red"]);
                    maxGreen = Math.Max(maxGreen, colorCounts["green"]);
                    maxBlue = Math.Max(maxBlue, colorCounts["blue"]);
                }

                summaryPower += maxRed * maxGreen * maxBlue;
            }
            return summaryPower;
        }
    }
}