// Decompiled with JetBrains decompiler
// Type: CrackMe.Properties.Settings
// Assembly: CrackMe, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E929FEB7-F3DD-449F-99CD-83285EA760B5
// Assembly location: C:\Development\de4dot\de4dot-v3-1\CrackMe-cleaned.exe

using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace CrackMe.Properties
{
    [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "11.0.0.0")]
    [CompilerGenerated]
    internal sealed class Settings : ApplicationSettingsBase
    {
        private static Settings settings_0;
        [NonSerialized] private string string_0;

        public static Settings Default
        {
            get { return Settings.settings_0; }
        }

        static Settings()
        {
            // ISSUE: reference to a compiler-generated field
            // ISSUE: object of a compiler-generated type is created
            Settings.settings_0 = (Settings) SettingsBase.Synchronized((SettingsBase) new Settings());
        }
    }
}
