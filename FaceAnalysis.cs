using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ReceiveAttachmentBot
{
    public class FaceAnalysis
    {

        private static string happinessIndex;
        public static string faceEmotion;

        public string getEmotion(string contentJson)
        {
            EmotionDetect(contentJson);
            double happinessIndexf = Convert.ToDouble(happinessIndex);
            if (happinessIndexf <= 0.2)
            {
                faceEmotion = "In poor mood";
            }
            else if (happinessIndexf <= 0.5)
            {
                faceEmotion = "in good mood";
            }
            else if (happinessIndexf <= 0.8 )
            {
                faceEmotion = "in happy mood";
            }
            else if (happinessIndexf <= 1)
            {
                faceEmotion = "in very happy mood";
            }

            return faceEmotion;
        }


        public void EmotionDetect(string contentJson)
        {
            
            JArray a = JArray.Parse(contentJson);

            foreach (JObject o in a.Children<JObject>())
            {
                foreach (JProperty p in o.Properties())
                {
                    string name = p.Name;
                    if (name == "faceAttributes")
                    {
                        Console.WriteLine(name);
                        JObject faceAttributes = (JObject)p.Value;
                        foreach (JProperty p1 in faceAttributes.Properties())
                        {
                            string name1 = p1.Name;
                            if (name1 == "emotion")
                            {
                                Console.WriteLine(name1);
                                JObject emotion = (JObject)p1.Value;
                                foreach (JProperty p2 in emotion.Properties())
                                {
                                    string name2 = p2.Name;
                                    if (name2 == "happiness")
                                    {
                                        Console.WriteLine(name2);
                                        string happiness = (String)p2.Value;
                                        Console.WriteLine(happiness);
                                        happinessIndex = happiness;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}