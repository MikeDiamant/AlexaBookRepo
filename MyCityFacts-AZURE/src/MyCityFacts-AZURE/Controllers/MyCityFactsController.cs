﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyCityFacts_AZURE.Controllers
{
    public class MyCityFactsController : ApiController
    {
        [HttpPost, Route("api/alexa/MyCityFacts")]
        public dynamic GetMyCityFacts (dynamic request)
        {
            var speech = BuildOutputSpeech(request);

            return new
            {
                version = "1.0",
                sessionAttributes = new { },
                response = new
                {
                    outputSpeech = new
                    {
                        type = "PlainText",
                        text = speech
                    },
                    card = new
                    {
                        type = "Simple",
                        title = "MyCity Fact",
                        content = speech
                    },
                    shouldEndSession = true
                }
            };
        }

        private string BuildOutputSpeech(dynamic request)
        {
            switch ((string)request.intent.name)
            {
                case "GetMyCityFactIntent":
                    return "Here is your fact: " + GetRandomFact();

                case "AMAZON.CancelIntent":
                case "AMAZON.StopIntent":
                    return "OK";

                case "AMAZON.HelpIntent":
                    return "Here is your help message";

                default:
                    return "Here is your help message";
            }
        }

        private string GetRandomFact()
        {
            var RNG = new Random();

            var Facts = new string[] {
                "MyCity has a population of 123,456 people, 11 bears, 3 alligators, and 7,134 cats.",
                "MyCity has several popular attractions, including 3 lakes, 2 mountains, and a 400 acre wildflower sanctuary. On a nice day, you can see the bears playing among the wildflowers, but dont get too close.",
                "MyCity was founded in 1873 by settlers who got lost on their way from Virginia to California and decided to stay put and make the best of it.",
                "The mayor of MyCity is YourName Goes Here. This is the 14th consecutive term for Mayor Here.",
                "MyCity holds a record in the Guinness Book of World Records for the largest man-made neon star on top of a mountain.",
                "City Hall has had three locations since first being constructed in 1902. The first move was due to extensive damage from the great flood of 1937, and the second move was due to the needs of the administration outgrowing the previous facilities. This move occurred in 1992, and City Hall now resides at the intersection of Hill Street and Mountain View Blvd.",
                "The summer carnival is MyCitys most popular event and raises enough money to fund the volunteer fire department every year.",
                "MyCity is home to 237 pizza restaurants, 9 fast food establishments, and 1 frozen banana stand, which is open from the beginning of July to the end of September.",
                "One of the prettiest places on Earth, MyCity has four perfect seasons of exactly 3 months each. The leaves change every fall, and winter offers exactly 22.4 inches of snowfall every year. Average summer temp is 76.8, every year.",
                "The Mascots are the official football team of MyCity. This is also the name of the local baseball team, basketball team, and soccer team."
            };

            return Facts[RNG.Next(0, Facts.Length - 1)];
        }
    }
}