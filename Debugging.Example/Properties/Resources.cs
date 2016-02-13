// Decompiled with JetBrains decompiler
// Type: CrackMe.Properties.Resources
// Assembly: CrackMe, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E929FEB7-F3DD-449F-99CD-83285EA760B5
// Assembly location: C:\Development\de4dot\de4dot-v3-1\CrackMe-cleaned.exe

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace CrackMe.Properties
{
    [DebuggerNonUserCode]
    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [CompilerGenerated]
    internal class Resources
    {
        private static ResourceManager resourceManager_0;
        private static CultureInfo cultureInfo_0;
        [NonSerialized] private string string_0;

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static ResourceManager ResourceManager
        {
            get
            {
                int num = 2;
                while (true)
                {
                    switch (num)
                    {
                        case 0:
                            goto label_8;
                        case 1:
                            Resources.resourceManager_0 = new ResourceManager("CrackMe.Properties.Resources",
                                typeof (Resources).Assembly);
                            num = 0;
                            continue;
                        case 2:
                            goto default;
                        default:
                            switch (1)
                            {
                                default:
                                    if (Resources.resourceManager_0 == null)
                                    {
                                        num = 1;
                                        continue;
                                    }
                                    goto label_8;
                            }
                    }
                }
                label_8:
                return Resources.resourceManager_0;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture
        {
            get { return Resources.cultureInfo_0; }
            set { Resources.cultureInfo_0 = value; }
        }

        internal Resources()
        {
        }
    }
}
