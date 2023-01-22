using System;
using System.Collections.Generic;
using System.Text;

namespace FF8TAS
{
    class Language
    {
        public static ulong BaseAddress = 0x0;
        // English addresses
        private static ulong BaseAddressEN = 0x18fe9b8;

        private static ulong EN_fieldX = 0x1677238;
        private static ulong EN_fieldY = 0x167723C;
        private static ulong EN_BGDraw = 0x18E4906;
        private static ulong EN_TextStatus = 0x192B354;
        private static ulong EN_SqualAnim = 0x156ED16;
        private static ulong EN_OptionChoice = 0x192B35B;
        private static ulong EN_IsGFMenu = 0x18E4A68;
        private static ulong EN_TextBox = 0x192B585;
        private static ulong EN_IsMenu = 0x18E490B;
        private static ulong EN_FieldID = 0x18D2FC0;
        private static ulong EN_CanMove = 0x199D018;
        private static ulong EN_TextID = 0x148C8C8;
        private static ulong EN_IsField = 0x1C3ED2C;

        private static ulong EN_MenuCursorReady = 0x1976C50;
        private static ulong EN_JunctionScroll = 0x1976CF9;
        private static ulong EN_ItemMenuFade = 0x1976CF5;
        private static ulong EN_JunctionFade = 0x1976CF3;

        private static ulong EN_AbilityPosition = 0x1976D0B;
        private static ulong EN_CharacterScrollJ = 0x1976CF6;
        private static ulong EN_MainCursorPos = 0x1976C80; // 1976CE0
        private static ulong EN_ConfigCursorPos = 0x1976CE0;
        private static ulong EN_MainOuterFade = 0x1976C6D;
        private static ulong EN_ConfigFade = 0x1976CDD;
        private static ulong EN_GF_Fade = 0x1976CE3;
        private static ulong EN_GF_CursorPosition = 0x1976CE3;
        private static ulong EN_GF_AbilityScroll = 0x1976CE0;
        private static ulong EN_GF_SelectedScroll = 0x1976CE7;
        private static ulong EN_GF_LearnScroll = 0x1976CE5;
        private static ulong EN_GF_AbilityPos1 = 0x1976CF1;
        private static ulong EN_GF_AbilityPos2 = 0x1976CF2;

        private static ulong EN_WMCameraTilt = 0x1C3ED08;

        // French addresses
        private static ulong BaseAddressFR = 0x1677238;

        private static ulong FR_fieldX = 0x1677238;
        private static ulong FR_fieldY = 0x167723C;
        private static ulong FR_BGDraw = 0x18E4906;
        private static ulong FR_TextStatus = 0x192B354;
        private static ulong FR_SqualAnim = 0x156ED16;
        private static ulong FR_OptionChoice = 0x192B35B;
        private static ulong FR_IsGFMenu = 0x18E4A68;
        private static ulong FR_TextBox = 0x192B585;
        private static ulong FR_IsMenu = 0x18E490B;

        public Language(string lang)
        {
            if (lang == "FR")
            {
                BaseAddress = BaseAddressFR;
                Memory.FieldX_Address = BaseAddressFR + EN_fieldX - BaseAddressEN;
            }
            if (lang == "EN")
            {
                BaseAddress = BaseAddressEN;
                Memory.FieldX_Address = EN_fieldX;
                Memory.FieldY_Address = EN_fieldY;
                Memory.BGDraw_Address = EN_BGDraw;
                Memory.TextStatus_Address = EN_TextStatus;
                Memory.SquallAnim_Address = EN_SqualAnim;
                Memory.OptionChoice_Address = EN_OptionChoice;
                Memory.IsGFMenu_Address = EN_IsGFMenu;
                Memory.TextBox_Address = EN_TextBox;
                Memory.IsMenu_Address = EN_IsMenu;
                Memory.FieldID_Address = EN_FieldID;
                Memory.CanMove_Address = EN_CanMove;
                Memory.TextID_Address = EN_TextID;
                Memory.IsBattleOrField_Address = EN_IsField;
                Memory.MenuCursorStatus_Address = EN_MenuCursorReady;
                Memory.JunctionScroll_Address = EN_JunctionScroll;
                Memory.ItemFade_Address = EN_ItemMenuFade;
                Memory.JunctionFade_Address = EN_JunctionFade;
                Memory.AbilityPosition_Address = EN_AbilityPosition;
                Memory.CharacterScrollJ_Address = EN_CharacterScrollJ;
                Memory.MainCursorPos_Address = EN_MainCursorPos;
                Memory.ConfigCursorPos_Address = EN_ConfigCursorPos;
                Memory.MainOuterFade_Address = EN_MainOuterFade;
                Memory.ConfigFade_Address = EN_ConfigFade;

                Memory.GF_Fade_Address = EN_GF_Fade;
                Memory.GF_CursorPosition_Address = EN_GF_CursorPosition;
                Memory.GF_AbilityScroll_Address = EN_GF_AbilityScroll;
                Memory.GF_SelectedScroll_Address = EN_GF_SelectedScroll;
                Memory.GF_LearnScroll_Address = EN_GF_LearnScroll;
                Memory.GF_AbilityPos1_Address = EN_GF_AbilityPos1;
                Memory.GF_AbilityPos2_Address = EN_GF_AbilityPos2;
            }
        }
    }
}