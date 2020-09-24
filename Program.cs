// Decompiled with JetBrains decompiler
// Type: GrowDBG.Program
// Assembly: CTools, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE9AC13E-5F2D-4B9C-9DCA-5D75DEE0C107
// Assembly location: C:\Users\ADMİN\Desktop\cerberusyrra7\cTools\CTools.exe

using System;
using System.Windows.Forms;

namespace GrowDBG
{
  internal static class Program
  {
    [STAThread]
    private static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run((Form) new Form1());
    }
  }
}
