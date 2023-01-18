﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FF8TAS
{
    static class Choices
    {
        static List<Choice> optionsList = new List<Choice>();
        public static Choice SelphieGarden1;
        public static Choice SelphieGarden2;

        public struct Choice
        {
            public string name;
            public int desiredID;
            public bool isCursorGoDown;
        }

        static Choices()
        {
            InitChoices();
        }

        static private void InitChoices()
        {
            SelphieGarden1 = new Choice();
            SelphieGarden1.desiredID = 1;
            SelphieGarden1.name = "Selphie Garden 1";
            SelphieGarden1.isCursorGoDown = true;

            SelphieGarden2 = new Choice();
            SelphieGarden2.desiredID = 1;
            SelphieGarden2.name = "Selphie Garden 2";
            SelphieGarden2.isCursorGoDown = true;
        }

        static public void AddQueue(Choice choice)
        {
            optionsList.Add(choice);
        }

        static public Choice GetNextChoice()
        {
            if (optionsList.Count > 0)
            {
                var choice = optionsList[0];
                optionsList.RemoveAt(0);
                return choice;
            }
            else
            {
                Choice choice = new Choice();
                choice.desiredID = 0;
                choice.isCursorGoDown = true;
                choice.name = "";
                return choice;
            }
        }
    }
}
