﻿using System;
using System.Collections.Generic;
using System.Xml;
using Timeline.Data.Events;
using Timeline.Data.Model;
using Timeline.Simulation.Events;

namespace Timeline.Simulation.Services
{
    public static class ConfigurationService
    {
        public static WorldConfiguration LoadFromFile(string filePath)
        {
            XmlDocument fileContent = new XmlDocument();
            fileContent.Load(filePath);

            int seed = DetermineSeed(fileContent);
            var configuration = new WorldConfiguration(seed);

            LoadRaces(fileContent, configuration);

            configuration.Map = LoadMap(fileContent, configuration);
            configuration.Events.AddRange(LoadEvents(fileContent, configuration));

            return configuration;
        }

        private static int DetermineSeed(XmlDocument dataFile)
        {
            int seed;
            if (dataFile.DocumentElement.HasAttribute("seed"))
                seed = int.Parse(dataFile.DocumentElement.Attributes["seed"].Value);
            else
                seed = new Random().Next(int.MinValue, int.MaxValue);
            return seed;
        }

        private static void LoadRaces(XmlDocument dataFile, WorldConfiguration configuration)
        {
            var raceNodes = dataFile.SelectNodes("/World/Races/Race");

            foreach (XmlNode raceNode in raceNodes)
            {
                var race = LoadRace(raceNode);
                configuration.RacesByName.Add(race.Name, race);
            }

            LoadFertility(raceNodes, configuration);
        }

        private static Race LoadRace(XmlNode raceNode)
        {
            var name = raceNode.Attributes["name"].Value;
            var race = new Race(name);

            race.Lifespan = ReadDistribution(raceNode["Lifespan"]);
            race.MinChildBearingAge = ReadDistribution(raceNode["ChildbearingAgeMin"]);
            race.MaxChildBearingAge = ReadDistribution(raceNode["ChildbearingAgeMax"]);
            
            return race;
        }

        private static Distribution ReadDistribution(XmlElement node)
        {
            double mean = double.Parse(node.Attributes["mean"].Value);
            double deviation = double.Parse(node.Attributes["deviation"].Value);
            return new Distribution(mean, deviation);
        }

        private static void LoadFertility(XmlNodeList raceNodes, WorldConfiguration configuration)
        {
            foreach (XmlNode raceNode in raceNodes)
            {
                var race = configuration.RacesByName[raceNode.Attributes["name"].Value];
                var fertilityNodes = raceNode.SelectNodes("Fertility/With");
                foreach (XmlNode fertilityNode in fertilityNodes)
                    LoadFertilityInstance(race, fertilityNode, configuration);
            }
        }

        private static void LoadFertilityInstance(Race race, XmlNode fertilityNode, WorldConfiguration configuration)
        {
            var fatherRace = configuration.RacesByName[fertilityNode.Attributes["father"].Value];
            var childRace = configuration.RacesByName[fertilityNode.Attributes["child"].Value];
            var value = int.Parse(fertilityNode.Attributes["value"].Value);

            race.FertilityChances[fatherRace] = new Tuple<int, Race>(value, childRace);
        }

        private static Map LoadMap(XmlDocument dataFile, WorldConfiguration configuration)
        {
            var mapNode = dataFile.SelectSingleNode("/World/Map");
            int width = int.Parse(mapNode.Attributes["width"].Value);
            int height = int.Parse(mapNode.Attributes["height"].Value);

            var map = new Map(width, height);

            foreach (XmlNode node in mapNode.ChildNodes)
            {
                var x = int.Parse(node.Attributes["x"].Value);
                var y = int.Parse(node.Attributes["y"].Value);

                var habitability = int.Parse(node.Attributes["habitability"].Value);
                map.Areas[x, y].Habitability = habitability;
            }

            return map;
        }

        private static IEnumerable<Event> LoadEvents(XmlDocument dataFile, WorldConfiguration configuration)
        {
            var eventNodes = dataFile.SelectNodes("/World/Events/*");

            foreach (XmlNode eventNode in eventNodes)
            {
                switch (eventNode.Name)
                {
                    case "Spawn":
                        yield return ReadSpawnEvent(eventNode, configuration);
                        break;

                    default:
                        continue;
                }
            }
        }

        private static SpawnEvent ReadSpawnEvent(XmlNode eventNode, WorldConfiguration configuration)
        {
            var race = configuration.RacesByName[eventNode.Attributes["race"].Value];
            var number = int.Parse(eventNode.Attributes["number"].Value);

            var ticks = long.Parse(eventNode.Attributes["year"].Value);
            var time = new GameTime(ticks);

            var locationX = int.Parse(eventNode.Attributes["locationX"].Value);
            var locationY = int.Parse(eventNode.Attributes["locationY"].Value);
            var location = configuration.Map.Areas[locationX, locationY];

            return new SpawnEvent(race, number, time, location);
        }
    }
}
