// Decompiled with JetBrains decompiler
// Type: Class1
// Assembly: CrackMe, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E929FEB7-F3DD-449F-99CD-83285EA760B5
// Assembly location: C:\Development\de4dot\de4dot-v3-1\CrackMe-cleaned.exe

using CrackMe;
using System;
using System.Windows.Forms;

internal static class Class1
{
  [STAThread]
  private static void Main()
  {
    Application.EnableVisualStyles();
    Application.SetCompatibleTextRenderingDefault(false);
    Application.Run((Form) new Form1());
  }
}
