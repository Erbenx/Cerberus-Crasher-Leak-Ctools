// Decompiled with JetBrains decompiler
// Type: GrowDBG.Form1
// Assembly: CTools, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE9AC13E-5F2D-4B9C-9DCA-5D75DEE0C107
// Assembly location: C:\Users\ADMİN\Desktop\cerberusyrra7\cTools\CTools.exe

using ENet.Managed;
using Guna.UI2.WinForms;
using Guna.UI2.WinForms.Suite;
using Memory;
using MetroFramework;
using MetroFramework.Controls;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrowDBG
{
  public class Form1 : MetroForm
  {
    public static string game_version = "3.39";
    public static string country = "ch";
    public static int token = 0;
    public static int userID = 0;
    public static int lmode = 0;
    public static string macc = "02:15:11:20:30:05";
    public static string doorid = "";
    public static Form1.PacketSending packetSender = new Form1.PacketSending();
    private static char[] hexmap = new char[16]
    {
      '0',
      '1',
      '2',
      '3',
      '4',
      '5',
      '6',
      '7',
      '8',
      '9',
      'a',
      'b',
      'c',
      'd',
      'e',
      'f'
    };
    private static string hash = Convert.ToString((uint) Form1.RandomNumbers.NextNumber());
    private static string hash2 = Convert.ToString((uint) Form1.RandomNumbers.NextNumber());
    private Mem mem = new Mem();
    private string wrold = "";
    private string lvls = "";
    public static string tankIDName;
    public static string tankIDPass;
    public static int Growtopia_Port;
    public static string Growtopia_IP;
    public static string Growtopia_Master_IP;
    public static int Growtopia_Master_Port;
    private static ENetHost g_Client;
    private static ENetPeer g_Peer;
    private string all;
    private int zdz;
    private IContainer components;
    private MetroTabControl metroTabControl1;
    private System.Windows.Forms.Timer timer1;
    private Guna2Button guna2Button1;
    private Guna2HtmlLabel guna2HtmlLabel2;
    private Guna2TextBox guna2TextBox2;
    private Guna2HtmlLabel guna2HtmlLabel1;
    private Guna2Elipse guna2Elipse1;
    private System.Windows.Forms.Timer timer2;
    private Guna2HtmlLabel guna2HtmlLabel3;
    private Guna2TextBox guna2TextBox1;
    public MetroTabPage metroTabPage2;
    private Guna2TextBox guna2TextBox3;
    private Guna2HtmlLabel guna2HtmlLabel4;
    private Guna2TextBox guna2TextBox4;
    private MetroTabPage metroTabPage1;
    private Guna2Button guna2Button2;
    private Guna2Button guna2Button3;
    public Guna2HtmlLabel guna2HtmlLabel5;
    public Guna2HtmlLabel guna2HtmlLabel6;
    private Guna2Button guna2Button4;
    private MetroTabPage metroTabPage3;
    private Guna2TextBox guna2TextBox5;
    private Guna2HtmlLabel guna2HtmlLabel7;
    private Guna2Button guna2Button5;
    private Guna2TextBox guna2TextBox6;
    private Guna2HtmlLabel guna2HtmlLabel8;

    public Form1()
    {
      this.InitializeComponent();
    }

    private static string hexStr(byte data)
    {
      return Form1.StringFunctions.ChangeCharacter(Form1.StringFunctions.ChangeCharacter(new string(' ', 2), 0, Form1.hexmap[((int) data & 240) >> 4]), 1, Form1.hexmap[(int) data & 15]);
    }

    private static string generateMeta()
    {
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < 9; ++index)
        stringBuilder.Append(Form1.hexStr((byte) Form1.RandomNumbers.NextNumber()));
      stringBuilder.Append(".com");
      return stringBuilder.ToString();
    }

    private static string generateMac()
    {
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < 6; ++index)
      {
        stringBuilder.Append(Form1.hexStr((byte) Form1.RandomNumbers.NextNumber()));
        if (index != 5)
          stringBuilder.Append(":");
      }
      return stringBuilder.ToString();
    }

    private static string generateRid()
    {
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < 16; ++index)
        stringBuilder.Append(Form1.hexStr((byte) Form1.RandomNumbers.NextNumber()).ToUpper());
      return stringBuilder.ToString();
    }

    public static string CreateLogonPacket(string customGrowID = "", string customPass = "")
    {
      string str1 = string.Empty;
      Random random = new Random();
      bool flag = false;
      if (Form1.token > 0 || Form1.token < 0)
        flag = true;
      if (customGrowID == "")
      {
        if (Form1.tankIDName != "")
          str1 = str1 + "tankIDName|" + Form1.tankIDName + "\n" + "tankIDPass|" + Form1.tankIDPass + "\n";
      }
      else
        str1 = str1 + "tankIDName|" + customGrowID + "\n" + "tankIDPass|" + customPass + "\n";
      string str2 = str1 + "requestedName|Bill\n" + "f|1\n" + "protocol|100\n" + "game_version|" + Form1.game_version + "\n" + "fz|6069928\n" + "lmode|0\n" + "cbits|0\n" + "player_age|57\n" + "GDPR|1\n" + "hash2|" + random.Next(-777777777, 777777777).ToString() + "\n" + "meta|" + Form1.generateMeta() + "\n" + "fhash|-716928004\n" + "rid|" + Form1.generateRid() + "\n" + "platformID|0\n" + "deviceVersion|0\n" + "country|ch\n" + "hash|" + random.Next(-777777777, 777777777).ToString() + "\n" + "user|" + Form1.userID.ToString() + "\n" + "token|" + Form1.token.ToString() + "\n" + "mac|" + Form1.generateMac() + "\n" + "wk|" + Form1.generateRid() + "\n" + "zf|-595512788";
      if (flag)
        str2 = str2 + "user|" + Form1.userID.ToString() + "\n";
      if (flag)
        str2 = str2 + "token|" + Form1.token.ToString() + "\n";
      if (Form1.doorid != "")
        str2 = str2 + "doorID|" + Form1.doorid.ToString() + "\n";
      return str2;
    }

    public static bool ProgramRunningAsAdmin()
    {
      return new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
    }

    private void disablecontrols()
    {
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      if (!Form1.ProgramRunningAsAdmin())
        this.disablecontrols();
      Console.WriteLine("[+] Initialized", (object) (Console.ForegroundColor = ConsoleColor.Yellow));
      this.metroTabControl1.SelectedIndex = 0;
    }

    private async void metroButton1_Click(object sender, EventArgs e)
    {
      try
      {
        this.mem.OpenProcess(Process.GetProcessesByName("Growtopia")[0].Id);
        await Task.Delay(220);
      }
      catch
      {
      }
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
      int length = Process.GetProcessesByName("Growtopia").Length;
    }

    public static void ConnectCurrent()
    {
      if (Form1.g_Client == null || !Form1.g_Client.ServiceThreadStarted)
        return;
      if (Form1.g_Peer == null)
      {
        Form1.g_Peer = Form1.g_Client.Connect(new IPEndPoint(IPAddress.Parse(Form1.Growtopia_IP), Form1.Growtopia_Port), (byte) 2, 0U);
      }
      else
      {
        if (Form1.g_Peer.State != ENetPeerState.Connected)
          return;
        Form1.g_Peer.Reset();
        Form1.g_Peer = Form1.g_Client.Connect(new IPEndPoint(IPAddress.Parse(Form1.Growtopia_IP), Form1.Growtopia_Port), (byte) 2, 0U);
      }
    }

    private void Peer_OnReceive_Client(object sender, ENetPacket e)
    {
      try
      {
        byte[] payloadFinal = e.GetPayloadFinal();
        Console.WriteLine("RECEIVE TYPE: " + payloadFinal[0].ToString());
        switch (payloadFinal[0])
        {
          case 1:
            string text1 = this.guna2TextBox1.Text;
            string text2 = this.guna2TextBox2.Text;
            Console.WriteLine("[ACCOUNT-CHECKER] Logging on " + text1 + "...");
            Console.WriteLine("[CONSOLE] Spoofing GT Version...");
            Form1.packetSender.SendPacket(2, Form1.CreateLogonPacket(text1, text2), Form1.g_Peer, ENetPacketFlags.Reliable);
            Form1.packetSender.SendPacket(3, "action|join_request\nname|xfacts", Form1.g_Peer, ENetPacketFlags.Reliable);
            Form1.packetSender.SendPacket(3, "action|join_request\nname|start", Form1.g_Peer, ENetPacketFlags.Reliable);
            break;
          case 2:
          case 3:
            Console.WriteLine("[ACCOUNT-CHECKER] TEXT PACKET CONTENT:\n" + Encoding.ASCII.GetString(((IEnumerable<byte>) payloadFinal).Skip<byte>(4).ToArray<byte>()));
            if (Encoding.ASCII.GetString(((IEnumerable<byte>) payloadFinal).Skip<byte>(4).ToArray<byte>()).IndexOf("action|play_sfx") != -1)
              Form1.packetSender.SendPacket(3, "action|join_request|\nname|xfacts", Form1.g_Peer, ENetPacketFlags.Reliable);
            if (Encoding.ASCII.GetString(payloadFinal).Contains("action|logon_fail"))
              Form1.ConnectCurrent();
            if (Encoding.ASCII.GetString(((IEnumerable<byte>) payloadFinal).Skip<byte>(4).ToArray<byte>()).IndexOf("password is wrong") != -1)
              this.BeginInvoke((Delegate) (() =>
              {
                Form1.g_Peer.Disconnect(0U);
                this.guna2Button1.Enabled = true;
                this.guna2TextBox1.Enabled = true;
                this.guna2TextBox2.Enabled = true;
              }));
            if (Encoding.ASCII.GetString(((IEnumerable<byte>) payloadFinal).Skip<byte>(4).ToArray<byte>()).IndexOf("LOGON ATTEMPTS") != -1)
              this.BeginInvoke((Delegate) (() =>
              {
                Form1.g_Peer.Disconnect(0U);
                this.guna2Button1.Enabled = true;
                this.guna2TextBox1.Enabled = true;
                this.guna2TextBox2.Enabled = true;
              }));
            if (Encoding.ASCII.GetString(((IEnumerable<byte>) payloadFinal).Skip<byte>(4).ToArray<byte>()).IndexOf("Protect") != -1)
              this.BeginInvoke((Delegate) (() =>
              {
                Form1.g_Peer.Disconnect(0U);
                this.guna2Button1.Enabled = true;
                this.guna2TextBox1.Enabled = true;
                this.guna2TextBox2.Enabled = true;
              }));
            if (Encoding.ASCII.GetString(((IEnumerable<byte>) payloadFinal).Skip<byte>(4).ToArray<byte>()).IndexOf("suspend") != -1)
              this.BeginInvoke((Delegate) (() =>
              {
                Form1.g_Peer.Disconnect(0U);
                this.guna2Button1.Enabled = true;
                this.guna2TextBox1.Enabled = true;
                this.guna2TextBox2.Enabled = true;
              }));
            if (Encoding.ASCII.GetString(((IEnumerable<byte>) payloadFinal).Skip<byte>(4).ToArray<byte>()).IndexOf("ban") != -1)
              this.BeginInvoke((Delegate) (() =>
              {
                Form1.g_Peer.Disconnect(0U);
                this.guna2Button1.Enabled = true;
                this.guna2TextBox1.Enabled = true;
                this.guna2TextBox2.Enabled = true;
              }));
            if (Encoding.ASCII.GetString(((IEnumerable<byte>) payloadFinal).Skip<byte>(4).ToArray<byte>()).IndexOf("maintenance") != -1)
              this.BeginInvoke((Delegate) (() =>
              {
                Form1.g_Peer.Disconnect(0U);
                this.guna2Button1.Enabled = true;
                this.guna2TextBox1.Enabled = true;
                this.guna2TextBox2.Enabled = true;
              }));
            if (Encoding.ASCII.GetString(((IEnumerable<byte>) payloadFinal).Skip<byte>(4).ToArray<byte>()).IndexOf("Authenticated|0") != -1)
              this.BeginInvoke((Delegate) (() =>
              {
                Form1.g_Peer.Disconnect(0U);
                this.guna2Button1.Enabled = true;
                this.guna2TextBox1.Enabled = true;
                this.guna2TextBox2.Enabled = true;
              }));
            if (Encoding.ASCII.GetString(((IEnumerable<byte>) payloadFinal).Skip<byte>(4).ToArray<byte>()).IndexOf("300_WORLD_VISIT") != -1)
              this.BeginInvoke((Delegate) (() =>
              {
                Form1.g_Peer.Disconnect(0U);
                this.guna2Button1.Enabled = true;
                this.guna2TextBox1.Enabled = true;
                this.guna2TextBox2.Enabled = true;
              }));
            if (Encoding.ASCII.GetString(((IEnumerable<byte>) payloadFinal).Skip<byte>(4).ToArray<byte>()).IndexOf("logon") == -1)
              break;
            this.BeginInvoke((Delegate) (() => {}));
            byte[] structData1 = Form1.VariantList.get_struct_data(payloadFinal);
            if (structData1[0] != (byte) 1)
              break;
            Form1.VariantList.VarList call1 = Form1.VariantList.GetCall(Form1.VariantList.get_extended_data(structData1));
            call1.netID = BitConverter.ToInt32(structData1, 4);
            call1.delay = BitConverter.ToUInt32(structData1, 20);
            if (!(call1.FunctionName == "OnSendToServer"))
              break;
            string str1 = (string) call1.functionArgs[4];
            if (str1.Contains("|"))
              str1 = str1.Substring(0, str1.IndexOf("|"));
            int functionArg1 = (int) call1.functionArgs[1];
            Form1.userID = (int) call1.functionArgs[3];
            Form1.token = (int) call1.functionArgs[2];
            Form1.lmode = (int) call1.functionArgs[5];
            Form1.Growtopia_IP = str1;
            Form1.Growtopia_Port = functionArg1;
            Form1.ConnectCurrent();
            break;
          case 4:
            string str2 = Encoding.ASCII.GetString(((IEnumerable<byte>) payloadFinal).ToArray<byte>());
            Console.WriteLine(str2);
            byte[] structData2 = Form1.VariantList.get_struct_data(payloadFinal);
            int num = (int) structData2[0];
            Form1.Player player = new Form1.Player();
            if (num == 9)
            {
              player.SerializePlayerInventory(Form1.VariantList.get_extended_data(structData2));
              foreach (Form1.InventoryItem inventoryItem in player.inventory.items)
                Console.WriteLine("ITEM NAME: " + Form1.ItemDatabase.GetItemDef((int) inventoryItem.itemID).itemName + " AMOUNT: " + inventoryItem.amount.ToString());
            }
            if (structData2[0] != (byte) 1)
              break;
            Form1.VariantList.VarList call2 = Form1.VariantList.GetCall(Form1.VariantList.get_extended_data(structData2));
            call2.netID = BitConverter.ToInt32(structData2, 4);
            call2.delay = BitConverter.ToUInt32(structData2, 20);
            Console.WriteLine("VLISTTYPES!!!" + call2.FunctionName);
            if (call2.FunctionName == "OnSpawn")
            {
              for (int index = 0; index < call2.functionArgs.Length; ++index)
                Console.WriteLine("onspawn argument{" + index.ToString() + "} packet: " + Encoding.ASCII.GetString(((IEnumerable<byte>) payloadFinal).Skip<byte>(4).ToArray<byte>()));
              Console.WriteLine("OnSpawn: " + str2);
            }
            if (call2.FunctionName == "OnRequestWorldSelectMenu")
              Form1.packetSender.SendPacket(3, "action|join_request\nname|xfacts", Form1.g_Peer, ENetPacketFlags.Reliable);
            if (call2.FunctionName == "OnPlayPositioned")
              Console.WriteLine("OnPlayPositioned packet: " + Encoding.ASCII.GetString(((IEnumerable<byte>) payloadFinal).Skip<byte>(4).ToArray<byte>()));
            if (call2.FunctionName == "OnTalkBubble")
              Console.WriteLine("OnTalkBubble packet: " + Encoding.ASCII.GetString(((IEnumerable<byte>) payloadFinal).Skip<byte>(4).ToArray<byte>()));
            if (call2.FunctionName == "onShowCaptcha")
              Console.WriteLine("Captcha: " + Encoding.ASCII.GetString(((IEnumerable<byte>) payloadFinal).Skip<byte>(4).ToArray<byte>()));
            if (call2.FunctionName == "OnSendToServer")
            {
              string str3 = (string) call2.functionArgs[4];
              if (str3.Contains("|"))
                str3 = str3.Substring(0, str3.IndexOf("|"));
              int functionArg2 = (int) call2.functionArgs[1];
              Form1.userID = (int) call2.functionArgs[3];
              Form1.token = (int) call2.functionArgs[2];
              Form1.lmode = (int) call2.functionArgs[5];
              Form1.Growtopia_IP = str3;
              Form1.Growtopia_Port = functionArg2;
              Form1.ConnectCurrent();
              this.BeginInvoke((Delegate) (() => {}));
            }
            if (!(call2.FunctionName == "OnConsoleMessage"))
              break;
            string input = Encoding.ASCII.GetString(((IEnumerable<byte>) payloadFinal).Skip<byte>(4).ToArray<byte>());
            Console.WriteLine(Regex.Replace(input, "\\x60[a-zA-Z0-9!@#$%^&*()_+\\-=\\[\\]\\{};':\"\\\\|,.<>\\/?]", "").Replace("\x0001", "").Replace("?", "").Replace("\x0010", "").Replace("\x0002", "").Replace(" ", "").Replace("                                                   ", ""));
            if (input.IndexOf("clock") != -1)
              Form1.packetSender.SendPacket(2, "action|input\n|text|Turkey Clock:" + DateTime.Now.ToString("h:mm:ss tt"), Form1.g_Peer, ENetPacketFlags.Reliable);
            if (input.IndexOf("quit") != -1)
            {
              Form1.packetSender.SendPacket(2, "action|input\n|text|respawned", Form1.g_Peer, ENetPacketFlags.Reliable);
              Form1.packetSender.SendPacket(2, "action|respawn", Form1.g_Peer, ENetPacketFlags.Reliable);
            }
            if (input.IndexOf("respawn") != -1)
              Form1.packetSender.SendPacket(3, "action|respawn", Form1.g_Peer, ENetPacketFlags.Reliable);
            if (input.IndexOf("dance") != -1)
              Form1.packetSender.SendPacket(2, "action|input\n|text|/dance", Form1.g_Peer, ENetPacketFlags.Reliable);
            Regex regex = new Regex("[0-9]");
            if (input.IndexOf("drop") != -1)
            {
              string str3 = string.Join<char>("", ((IEnumerable<char>) input.ToCharArray()).Where<char>(new Func<char, bool>(char.IsDigit))).Replace("006", "").Replace("046", "");
              string str4 = str3[str3.Length - 1].ToString();
              Form1.packetSender.SendPacket(2, "action|input\n|text|If have item of " + str4.ToString(), Form1.g_Peer, ENetPacketFlags.Reliable);
              Form1.packetSender.SendPacket(2, "action|drop\nitemID|" + str4.ToString() + "|\n", Form1.g_Peer, ENetPacketFlags.Reliable);
              string str5 = "action|dialog_return\ndialog_name|drop_item\nitemID|" + str4.ToString() + "|\ncount|1\n";
              Form1.packetSender.SendPacket(2, str5, Form1.g_Peer, ENetPacketFlags.Reliable);
            }
            if (input.IndexOf("inven") == -1)
              break;
            Form1.Inventory inventory = player.inventory;
            if (inventory.items == null)
              Console.WriteLine("inventory.items was null!");
            foreach (Form1.InventoryItem inventoryItem in inventory.items)
              Console.WriteLine(inventoryItem.ToString() + " 1111111111111111111111111111111111");
            break;
          case 6:
            Console.WriteLine("TRACKOC\n" + Encoding.ASCII.GetString(((IEnumerable<byte>) payloadFinal).Skip<byte>(4).ToArray<byte>()));
            if (Encoding.ASCII.GetString(((IEnumerable<byte>) payloadFinal).Skip<byte>(4).ToArray<byte>()).IndexOf("Level") != -1)
            {
              ++this.zdz;
              this.lvls = this.lvls + Encoding.ASCII.GetString(((IEnumerable<byte>) payloadFinal).Skip<byte>(4).ToArray<byte>()) + "\n";
            }
            if (Encoding.ASCII.GetString(((IEnumerable<byte>) payloadFinal).Skip<byte>(4).ToArray<byte>()).IndexOf("Worldlock") != -1)
            {
              ++this.zdz;
              this.wrold = this.wrold + Encoding.ASCII.GetString(((IEnumerable<byte>) payloadFinal).Skip<byte>(4).ToArray<byte>()) + "\n";
            }
            Form1.Growtopia_Port = Form1.Growtopia_Master_Port;
            Form1.Growtopia_IP = Form1.Growtopia_Master_IP;
            Form1.packetSender.SendPacket(2, "action|enter_game", Form1.g_Peer, ENetPacketFlags.Reliable);
            Console.WriteLine("Spoofing Gt Version...");
            this.BeginInvoke((Delegate) (() => {}));
            break;
          case 9:
            Console.WriteLine("inventory systems");
            break;
          default:
            Console.WriteLine("default [type" + payloadFinal[0].ToString() + " ]:" + Encoding.ASCII.GetString(((IEnumerable<byte>) payloadFinal).Skip<byte>(4).ToArray<byte>()));
            break;
        }
      }
      catch
      {
      }
    }

    private void Peer_OnDisconnect_Client(object sender, uint e)
    {
      this.BeginInvoke((Delegate) (() =>
      {
        this.guna2Button1.Enabled = true;
        this.guna2TextBox1.Enabled = true;
        this.guna2TextBox2.Enabled = true;
      }));
      Console.WriteLine("[ACCOUNT-CHECKER] Disconnected from GT Server(s)!");
      this.all = this.all + this.lvls + "\n" + this.wrold;
    }

    private void Client_OnConnect(object sender, ENetConnectEventArgs e)
    {
      e.Peer.OnReceive += new EventHandler<ENetPacket>(this.Peer_OnReceive_Client);
      e.Peer.OnDisconnect += new EventHandler<uint>(this.Peer_OnDisconnect_Client);
      e.Peer.PingInterval(1000U);
      e.Peer.Timeout(1000U, 9000U, 13000U);
      Console.WriteLine("[ACCOUNT-CHECKER] Successfully connected to GT Server(s)!");
    }

    private void guna2Button1_Click(object sender, EventArgs e)
    {
      Form1.Growtopia_IP = this.guna2TextBox3.Text;
      Form1.Growtopia_Port = int.Parse(this.guna2TextBox4.Text);
      Form1.tankIDName = this.guna2TextBox1.Text;
      Form1.tankIDPass = this.guna2TextBox2.Text;
      if (this.guna2TextBox1.Text.Length < 3 || this.guna2TextBox2.Text.Length < 3)
      {
        int num1 = (int) MessageBox.Show("Cannot be less account name and password than 3!");
        if (this.guna2TextBox3.Text.Length >= 3 && this.guna2TextBox4.Text.Length >= 3)
          return;
        int num2 = (int) MessageBox.Show("Cannot be less ip and port!");
      }
      else
      {
        this.guna2Button1.Enabled = false;
        this.guna2TextBox1.Enabled = false;
        this.guna2TextBox2.Enabled = false;
        ManagedENet.Startup((ENetAllocator) null);
        Form1.g_Client = new ENetHost(1, (byte) 2);
        Form1.g_Client.OnConnect += new EventHandler<ENetConnectEventArgs>(this.Client_OnConnect);
        Form1.g_Client.ChecksumWithCRC32();
        Form1.g_Client.CompressWithRangeCoder();
        Form1.g_Client.StartServiceThread();
        Form1.g_Peer = Form1.g_Client.Connect(new IPEndPoint(IPAddress.Parse(Form1.Growtopia_IP), Form1.Growtopia_Port), (byte) 2, 0U);
        this.BeginInvoke((Delegate) (() => {}));
      }
    }

    private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      int num = (int) MessageBox.Show("I think you don't allow your gmail account for less secure apps.", "Solution.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      Process.Start("https://myaccount.google.com/lesssecureapps");
    }

    private void guna2Button2_Click(object sender, EventArgs e)
    {
    }

    private void label2_Click(object sender, EventArgs e)
    {
      int num = (int) MessageBox.Show("Gmail method is unsafe, i recommend to use discord webhook receiver.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }

    private void timer2_Tick(object sender, EventArgs e)
    {
    }

    private void metroTabPage2_Click(object sender, EventArgs e)
    {
    }

    private void guna2Separator1_Click(object sender, EventArgs e)
    {
    }

    private void guna2Button3_Click(object sender, EventArgs e)
    {
      new Thread((ThreadStart) (() =>
      {
        while (true)
          Form1.packetSender.SendPacket(2, "action|input\n|text|ben oluyom", Form1.g_Peer, ENetPacketFlags.Reliable);
      })).Start();
    }

    private void guna2Button2_Click_1(object sender, EventArgs e)
    {
      new Thread((ThreadStart) (() =>
      {
        while (true)
        {
          Thread.Sleep(80);
          Form1.packetSender.SendPacket(2, "action|input\n|text|THIS SERVER RAIDED BY CERBERUS!", Form1.g_Peer, ENetPacketFlags.Reliable);
        }
      })).Start();
    }

    private void guna2Button4_Click(object sender, EventArgs e)
    {
      new Thread((ThreadStart) (() =>
      {
        while (true)
          Form1.packetSender.SendPacket(2, "action|input\n|text||", Form1.g_Peer, ENetPacketFlags.Reliable);
      })).Start();
    }

    private void richTextBox2_TextChanged(object sender, EventArgs e)
    {
    }

    private void guna2TextBox1_TextChanged(object sender, EventArgs e)
    {
    }

    private void metroTabControl1_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    private void guna2HtmlLabel3_Click(object sender, EventArgs e)
    {
    }

    private void guna2HtmlLabel2_Click(object sender, EventArgs e)
    {
    }

    private void guna2TextBox2_TextChanged(object sender, EventArgs e)
    {
    }

    private void guna2HtmlLabel1_Click(object sender, EventArgs e)
    {
    }

    private void metroTabPage1_Click(object sender, EventArgs e)
    {
    }

    private void guna2TextBox3_TextChanged(object sender, EventArgs e)
    {
    }

    private void guna2TextBox4_TextChanged(object sender, EventArgs e)
    {
    }

    private void guna2HtmlLabel5_Click(object sender, EventArgs e)
    {
    }

    private void metroTabPage3_Click(object sender, EventArgs e)
    {
    }

    private void guna2TextBox1_TextChanged_1(object sender, EventArgs e)
    {
    }

    private void guna2HtmlLabel6_Click(object sender, EventArgs e)
    {
    }

    private void guna2TextBox5_TextChanged(object sender, EventArgs e)
    {
    }

    private void guna2Button5_Click(object sender, EventArgs e)
    {
      this.guna2TextBox6.Text = new StreamReader(WebRequest.Create(this.guna2TextBox5.Text).GetResponse().GetResponseStream()).ReadToEnd();
    }

    private void richTextBox1_TextChanged(object sender, EventArgs e)
    {
    }

    private void guna2TextBox6_TextChanged(object sender, EventArgs e)
    {
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      this.metroTabControl1 = new MetroTabControl();
      this.metroTabPage2 = new MetroTabPage();
      this.guna2Button1 = new Guna2Button();
      this.guna2HtmlLabel4 = new Guna2HtmlLabel();
      this.guna2TextBox4 = new Guna2TextBox();
      this.guna2TextBox3 = new Guna2TextBox();
      this.guna2TextBox1 = new Guna2TextBox();
      this.guna2HtmlLabel3 = new Guna2HtmlLabel();
      this.guna2HtmlLabel2 = new Guna2HtmlLabel();
      this.guna2TextBox2 = new Guna2TextBox();
      this.guna2HtmlLabel1 = new Guna2HtmlLabel();
      this.metroTabPage3 = new MetroTabPage();
      this.guna2HtmlLabel8 = new Guna2HtmlLabel();
      this.guna2TextBox6 = new Guna2TextBox();
      this.guna2Button5 = new Guna2Button();
      this.guna2HtmlLabel7 = new Guna2HtmlLabel();
      this.guna2TextBox5 = new Guna2TextBox();
      this.metroTabPage1 = new MetroTabPage();
      this.guna2Button4 = new Guna2Button();
      this.guna2Button2 = new Guna2Button();
      this.guna2Button3 = new Guna2Button();
      this.timer1 = new System.Windows.Forms.Timer(this.components);
      this.guna2Elipse1 = new Guna2Elipse(this.components);
      this.timer2 = new System.Windows.Forms.Timer(this.components);
      this.guna2HtmlLabel5 = new Guna2HtmlLabel();
      this.guna2HtmlLabel6 = new Guna2HtmlLabel();
      this.metroTabControl1.SuspendLayout();
      this.metroTabPage2.SuspendLayout();
      this.metroTabPage3.SuspendLayout();
      this.metroTabPage1.SuspendLayout();
      this.SuspendLayout();
      this.metroTabControl1.Appearance = TabAppearance.Buttons;
      this.metroTabControl1.Controls.Add((Control) this.metroTabPage2);
      this.metroTabControl1.Controls.Add((Control) this.metroTabPage3);
      this.metroTabControl1.Controls.Add((Control) this.metroTabPage1);
      this.metroTabControl1.FontWeight = MetroTabControlWeight.Regular;
      this.metroTabControl1.Location = new Point(23, 86);
      this.metroTabControl1.Margin = new Padding(2, 3, 2, 3);
      this.metroTabControl1.Name = "metroTabControl1";
      this.metroTabControl1.SelectedIndex = 2;
      this.metroTabControl1.Size = new Size(515, 301);
      this.metroTabControl1.TabIndex = 0;
      this.metroTabControl1.SelectedIndexChanged += new EventHandler(this.metroTabControl1_SelectedIndexChanged);
      this.metroTabPage2.BorderStyle = BorderStyle.FixedSingle;
      this.metroTabPage2.Controls.Add((Control) this.guna2Button1);
      this.metroTabPage2.Controls.Add((Control) this.guna2HtmlLabel4);
      this.metroTabPage2.Controls.Add((Control) this.guna2TextBox4);
      this.metroTabPage2.Controls.Add((Control) this.guna2TextBox3);
      this.metroTabPage2.Controls.Add((Control) this.guna2TextBox1);
      this.metroTabPage2.Controls.Add((Control) this.guna2HtmlLabel3);
      this.metroTabPage2.Controls.Add((Control) this.guna2HtmlLabel2);
      this.metroTabPage2.Controls.Add((Control) this.guna2TextBox2);
      this.metroTabPage2.Controls.Add((Control) this.guna2HtmlLabel1);
      this.metroTabPage2.HorizontalScrollbarBarColor = true;
      this.metroTabPage2.Location = new Point(4, 38);
      this.metroTabPage2.Name = "metroTabPage2";
      this.metroTabPage2.Size = new Size(507, 259);
      this.metroTabPage2.TabIndex = 1;
      this.metroTabPage2.Text = "Account Checker";
      this.metroTabPage2.VerticalScrollbarBarColor = true;
      this.metroTabPage2.Click += new EventHandler(this.metroTabPage2_Click);
      this.guna2Button1.CheckedState.Parent = (CustomButtonBase) this.guna2Button1;
      this.guna2Button1.CustomImages.Parent = (CustomButtonBase) this.guna2Button1;
      this.guna2Button1.FillColor = Color.FromArgb(0, 174, 219);
      this.guna2Button1.Font = new Font("Segoe UI", 9f);
      this.guna2Button1.ForeColor = Color.White;
      this.guna2Button1.HoverState.Parent = (CustomButtonBase) this.guna2Button1;
      this.guna2Button1.Location = new Point(196, 197);
      this.guna2Button1.Name = "guna2Button1";
      this.guna2Button1.ShadowDecoration.Parent = (Control) this.guna2Button1;
      this.guna2Button1.Size = new Size(143, 41);
      this.guna2Button1.TabIndex = 6;
      this.guna2Button1.Text = "Check";
      this.guna2Button1.Click += new EventHandler(this.guna2Button1_Click);
      this.guna2HtmlLabel4.BackColor = Color.Transparent;
      this.guna2HtmlLabel4.Font = new Font("Bahnschrift SemiCondensed", 15.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.guna2HtmlLabel4.Location = new Point(24, 144);
      this.guna2HtmlLabel4.Name = "guna2HtmlLabel4";
      this.guna2HtmlLabel4.Size = new Size(77, 27);
      this.guna2HtmlLabel4.TabIndex = 16;
      this.guna2HtmlLabel4.Text = "SWPORT:";
      this.guna2TextBox4.Cursor = Cursors.IBeam;
      this.guna2TextBox4.DefaultText = "";
      this.guna2TextBox4.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
      this.guna2TextBox4.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
      this.guna2TextBox4.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
      this.guna2TextBox4.DisabledState.Parent = (Guna.UI2.WinForms.Suite.TextBox) this.guna2TextBox4;
      this.guna2TextBox4.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
      this.guna2TextBox4.FocusedState.BorderColor = Color.FromArgb(94, 148, (int) byte.MaxValue);
      this.guna2TextBox4.FocusedState.Parent = (Guna.UI2.WinForms.Suite.TextBox) this.guna2TextBox4;
      this.guna2TextBox4.Font = new Font("Segoe UI", 9f);
      this.guna2TextBox4.HoverState.BorderColor = Color.FromArgb(94, 148, (int) byte.MaxValue);
      this.guna2TextBox4.HoverState.Parent = (Guna.UI2.WinForms.Suite.TextBox) this.guna2TextBox4;
      this.guna2TextBox4.Location = new Point(117, 144);
      this.guna2TextBox4.Name = "guna2TextBox4";
      this.guna2TextBox4.PasswordChar = char.MinValue;
      this.guna2TextBox4.PlaceholderText = "";
      this.guna2TextBox4.SelectedText = "";
      this.guna2TextBox4.ShadowDecoration.Parent = (Control) this.guna2TextBox4;
      this.guna2TextBox4.Size = new Size(339, 36);
      this.guna2TextBox4.TabIndex = 15;
      this.guna2TextBox4.TextChanged += new EventHandler(this.guna2TextBox4_TextChanged);
      this.guna2TextBox3.Cursor = Cursors.IBeam;
      this.guna2TextBox3.DefaultText = "";
      this.guna2TextBox3.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
      this.guna2TextBox3.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
      this.guna2TextBox3.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
      this.guna2TextBox3.DisabledState.Parent = (Guna.UI2.WinForms.Suite.TextBox) this.guna2TextBox3;
      this.guna2TextBox3.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
      this.guna2TextBox3.FocusedState.BorderColor = Color.FromArgb(94, 148, (int) byte.MaxValue);
      this.guna2TextBox3.FocusedState.Parent = (Guna.UI2.WinForms.Suite.TextBox) this.guna2TextBox3;
      this.guna2TextBox3.Font = new Font("Segoe UI", 9f);
      this.guna2TextBox3.HoverState.BorderColor = Color.FromArgb(94, 148, (int) byte.MaxValue);
      this.guna2TextBox3.HoverState.Parent = (Guna.UI2.WinForms.Suite.TextBox) this.guna2TextBox3;
      this.guna2TextBox3.Location = new Point(117, 102);
      this.guna2TextBox3.Name = "guna2TextBox3";
      this.guna2TextBox3.PasswordChar = char.MinValue;
      this.guna2TextBox3.PlaceholderText = "";
      this.guna2TextBox3.SelectedText = "";
      this.guna2TextBox3.ShadowDecoration.Parent = (Control) this.guna2TextBox3;
      this.guna2TextBox3.Size = new Size(339, 36);
      this.guna2TextBox3.TabIndex = 14;
      this.guna2TextBox3.TextChanged += new EventHandler(this.guna2TextBox3_TextChanged);
      this.guna2TextBox1.Cursor = Cursors.IBeam;
      this.guna2TextBox1.DefaultText = "";
      this.guna2TextBox1.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
      this.guna2TextBox1.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
      this.guna2TextBox1.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
      this.guna2TextBox1.DisabledState.Parent = (Guna.UI2.WinForms.Suite.TextBox) this.guna2TextBox1;
      this.guna2TextBox1.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
      this.guna2TextBox1.FocusedState.BorderColor = Color.FromArgb(94, 148, (int) byte.MaxValue);
      this.guna2TextBox1.FocusedState.Parent = (Guna.UI2.WinForms.Suite.TextBox) this.guna2TextBox1;
      this.guna2TextBox1.Font = new Font("Segoe UI", 9f);
      this.guna2TextBox1.HoverState.BorderColor = Color.FromArgb(94, 148, (int) byte.MaxValue);
      this.guna2TextBox1.HoverState.Parent = (Guna.UI2.WinForms.Suite.TextBox) this.guna2TextBox1;
      this.guna2TextBox1.Location = new Point(117, 15);
      this.guna2TextBox1.Name = "guna2TextBox1";
      this.guna2TextBox1.PasswordChar = char.MinValue;
      this.guna2TextBox1.PlaceholderText = "";
      this.guna2TextBox1.SelectedText = "";
      this.guna2TextBox1.ShadowDecoration.Parent = (Control) this.guna2TextBox1;
      this.guna2TextBox1.Size = new Size(339, 36);
      this.guna2TextBox1.TabIndex = 13;
      this.guna2TextBox1.TextChanged += new EventHandler(this.guna2TextBox1_TextChanged_1);
      this.guna2HtmlLabel3.BackColor = Color.Transparent;
      this.guna2HtmlLabel3.Font = new Font("Bahnschrift SemiCondensed", 15.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.guna2HtmlLabel3.Location = new Point(12, 102);
      this.guna2HtmlLabel3.Name = "guna2HtmlLabel3";
      this.guna2HtmlLabel3.Size = new Size(89, 27);
      this.guna2HtmlLabel3.TabIndex = 12;
      this.guna2HtmlLabel3.Text = "IPADRESS:";
      this.guna2HtmlLabel3.Click += new EventHandler(this.guna2HtmlLabel3_Click);
      this.guna2HtmlLabel2.BackColor = Color.Transparent;
      this.guna2HtmlLabel2.Font = new Font("Bahnschrift SemiCondensed", 15.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.guna2HtmlLabel2.Location = new Point(13, 60);
      this.guna2HtmlLabel2.Name = "guna2HtmlLabel2";
      this.guna2HtmlLabel2.Size = new Size(88, 27);
      this.guna2HtmlLabel2.TabIndex = 5;
      this.guna2HtmlLabel2.Text = "Password:";
      this.guna2HtmlLabel2.Click += new EventHandler(this.guna2HtmlLabel2_Click);
      this.guna2TextBox2.Cursor = Cursors.IBeam;
      this.guna2TextBox2.DefaultText = "";
      this.guna2TextBox2.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
      this.guna2TextBox2.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
      this.guna2TextBox2.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
      this.guna2TextBox2.DisabledState.Parent = (Guna.UI2.WinForms.Suite.TextBox) this.guna2TextBox2;
      this.guna2TextBox2.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
      this.guna2TextBox2.FocusedState.BorderColor = Color.FromArgb(94, 148, (int) byte.MaxValue);
      this.guna2TextBox2.FocusedState.Parent = (Guna.UI2.WinForms.Suite.TextBox) this.guna2TextBox2;
      this.guna2TextBox2.Font = new Font("Segoe UI Semilight", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.guna2TextBox2.HoverState.BorderColor = Color.FromArgb(94, 148, (int) byte.MaxValue);
      this.guna2TextBox2.HoverState.Parent = (Guna.UI2.WinForms.Suite.TextBox) this.guna2TextBox2;
      this.guna2TextBox2.Location = new Point(117, 57);
      this.guna2TextBox2.Margin = new Padding(2, 3, 2, 3);
      this.guna2TextBox2.Name = "guna2TextBox2";
      this.guna2TextBox2.PasswordChar = char.MinValue;
      this.guna2TextBox2.PlaceholderText = "";
      this.guna2TextBox2.SelectedText = "";
      this.guna2TextBox2.ShadowDecoration.Parent = (Control) this.guna2TextBox2;
      this.guna2TextBox2.Size = new Size(339, 36);
      this.guna2TextBox2.TabIndex = 4;
      this.guna2TextBox2.UseSystemPasswordChar = true;
      this.guna2TextBox2.TextChanged += new EventHandler(this.guna2TextBox2_TextChanged);
      this.guna2HtmlLabel1.BackColor = Color.Transparent;
      this.guna2HtmlLabel1.Font = new Font("Bahnschrift SemiCondensed", 15.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.guna2HtmlLabel1.Location = new Point(12, 19);
      this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
      this.guna2HtmlLabel1.Size = new Size(91, 27);
      this.guna2HtmlLabel1.TabIndex = 3;
      this.guna2HtmlLabel1.Text = "Username:";
      this.guna2HtmlLabel1.Click += new EventHandler(this.guna2HtmlLabel1_Click);
      this.metroTabPage3.AccessibleRole = AccessibleRole.Cursor;
      this.metroTabPage3.Controls.Add((Control) this.guna2HtmlLabel8);
      this.metroTabPage3.Controls.Add((Control) this.guna2Button5);
      this.metroTabPage3.Controls.Add((Control) this.guna2HtmlLabel7);
      this.metroTabPage3.Controls.Add((Control) this.guna2TextBox5);
      this.metroTabPage3.Controls.Add((Control) this.guna2TextBox6);
      this.metroTabPage3.HorizontalScrollbarBarColor = true;
      this.metroTabPage3.Location = new Point(4, 38);
      this.metroTabPage3.Name = "metroTabPage3";
      this.metroTabPage3.Size = new Size(507, 259);
      this.metroTabPage3.TabIndex = 3;
      this.metroTabPage3.Text = "Port Check";
      this.metroTabPage3.VerticalScrollbarBarColor = true;
      this.metroTabPage3.Click += new EventHandler(this.metroTabPage3_Click);
      this.guna2HtmlLabel8.BackColor = Color.Transparent;
      this.guna2HtmlLabel8.Font = new Font("Bahnschrift SemiCondensed", 15.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.guna2HtmlLabel8.Location = new Point(94, 71);
      this.guna2HtmlLabel8.Name = "guna2HtmlLabel8";
      this.guna2HtmlLabel8.Size = new Size(61, 27);
      this.guna2HtmlLabel8.TabIndex = 16;
      this.guna2HtmlLabel8.Text = "Output:";
      this.guna2TextBox6.Cursor = Cursors.IBeam;
      this.guna2TextBox6.DefaultText = "";
      this.guna2TextBox6.DisabledState.Parent = (Guna.UI2.WinForms.Suite.TextBox) this.guna2TextBox6;
      this.guna2TextBox6.FocusedState.Parent = (Guna.UI2.WinForms.Suite.TextBox) this.guna2TextBox6;
      this.guna2TextBox6.Font = new Font("Segoe UI", 9f);
      this.guna2TextBox6.HoverState.Parent = (Guna.UI2.WinForms.Suite.TextBox) this.guna2TextBox6;
      this.guna2TextBox6.Location = new Point(174, 72);
      this.guna2TextBox6.Name = "guna2TextBox6";
      this.guna2TextBox6.PasswordChar = char.MinValue;
      this.guna2TextBox6.PlaceholderText = "";
      this.guna2TextBox6.SelectedText = "";
      this.guna2TextBox6.ShadowDecoration.Parent = (Control) this.guna2TextBox6;
      this.guna2TextBox6.Size = new Size(200, 36);
      this.guna2TextBox6.TabIndex = 15;
      this.guna2TextBox6.TextChanged += new EventHandler(this.guna2TextBox6_TextChanged);
      this.guna2Button5.CheckedState.Parent = (CustomButtonBase) this.guna2Button5;
      this.guna2Button5.CustomImages.Parent = (CustomButtonBase) this.guna2Button5;
      this.guna2Button5.FillColor = Color.FromArgb(0, 174, 219);
      this.guna2Button5.Font = new Font("Segoe UI", 9f);
      this.guna2Button5.ForeColor = Color.White;
      this.guna2Button5.HoverState.Parent = (CustomButtonBase) this.guna2Button5;
      this.guna2Button5.Location = new Point(204, 114);
      this.guna2Button5.Name = "guna2Button5";
      this.guna2Button5.ShadowDecoration.Parent = (Control) this.guna2Button5;
      this.guna2Button5.Size = new Size(143, 41);
      this.guna2Button5.TabIndex = 12;
      this.guna2Button5.Text = "Check";
      this.guna2Button5.Click += new EventHandler(this.guna2Button5_Click);
      this.guna2HtmlLabel7.BackColor = Color.Transparent;
      this.guna2HtmlLabel7.Font = new Font("Bahnschrift SemiCondensed", 15.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.guna2HtmlLabel7.Location = new Point(66, 38);
      this.guna2HtmlLabel7.Name = "guna2HtmlLabel7";
      this.guna2HtmlLabel7.Size = new Size(89, 27);
      this.guna2HtmlLabel7.TabIndex = 4;
      this.guna2HtmlLabel7.Text = "IPADRESS:";
      this.guna2TextBox5.Cursor = Cursors.IBeam;
      this.guna2TextBox5.DefaultText = "";
      this.guna2TextBox5.DisabledState.Parent = (Guna.UI2.WinForms.Suite.TextBox) this.guna2TextBox5;
      this.guna2TextBox5.FocusedState.Parent = (Guna.UI2.WinForms.Suite.TextBox) this.guna2TextBox5;
      this.guna2TextBox5.Font = new Font("Segoe UI", 9f);
      this.guna2TextBox5.HoverState.Parent = (Guna.UI2.WinForms.Suite.TextBox) this.guna2TextBox5;
      this.guna2TextBox5.Location = new Point(174, 30);
      this.guna2TextBox5.Name = "guna2TextBox5";
      this.guna2TextBox5.PasswordChar = char.MinValue;
      this.guna2TextBox5.PlaceholderText = "";
      this.guna2TextBox5.SelectedText = "";
      this.guna2TextBox5.ShadowDecoration.Parent = (Control) this.guna2TextBox5;
      this.guna2TextBox5.Size = new Size(200, 36);
      this.guna2TextBox5.TabIndex = 14;
      this.guna2TextBox5.TextChanged += new EventHandler(this.guna2TextBox5_TextChanged);
      this.metroTabPage1.Controls.Add((Control) this.guna2Button4);
      this.metroTabPage1.Controls.Add((Control) this.guna2Button2);
      this.metroTabPage1.Controls.Add((Control) this.guna2Button3);
      this.metroTabPage1.HorizontalScrollbarBarColor = true;
      this.metroTabPage1.Location = new Point(4, 38);
      this.metroTabPage1.Name = "metroTabPage1";
      this.metroTabPage1.Size = new Size(507, 259);
      this.metroTabPage1.TabIndex = 2;
      this.metroTabPage1.Text = "Crash Page";
      this.metroTabPage1.VerticalScrollbarBarColor = true;
      this.metroTabPage1.Click += new EventHandler(this.metroTabPage1_Click);
      this.guna2Button4.CheckedState.Parent = (CustomButtonBase) this.guna2Button4;
      this.guna2Button4.CustomImages.Parent = (CustomButtonBase) this.guna2Button4;
      this.guna2Button4.FillColor = Color.FromArgb(0, 174, 219);
      this.guna2Button4.Font = new Font("Segoe UI", 9f);
      this.guna2Button4.ForeColor = Color.White;
      this.guna2Button4.HoverState.Parent = (CustomButtonBase) this.guna2Button4;
      this.guna2Button4.Location = new Point(157, 96);
      this.guna2Button4.Name = "guna2Button4";
      this.guna2Button4.ShadowDecoration.Parent = (Control) this.guna2Button4;
      this.guna2Button4.Size = new Size(143, 41);
      this.guna2Button4.TabIndex = 15;
      this.guna2Button4.Text = "Special";
      this.guna2Button4.Click += new EventHandler(this.guna2Button4_Click);
      this.guna2Button2.CheckedState.Parent = (CustomButtonBase) this.guna2Button2;
      this.guna2Button2.CustomImages.Parent = (CustomButtonBase) this.guna2Button2;
      this.guna2Button2.FillColor = Color.FromArgb(0, 174, 219);
      this.guna2Button2.Font = new Font("Segoe UI", 9f);
      this.guna2Button2.ForeColor = Color.White;
      this.guna2Button2.HoverState.Parent = (CustomButtonBase) this.guna2Button2;
      this.guna2Button2.Location = new Point(313, 19);
      this.guna2Button2.Name = "guna2Button2";
      this.guna2Button2.ShadowDecoration.Parent = (Control) this.guna2Button2;
      this.guna2Button2.Size = new Size(143, 41);
      this.guna2Button2.TabIndex = 14;
      this.guna2Button2.Text = "Message Spam";
      this.guna2Button2.Click += new EventHandler(this.guna2Button2_Click_1);
      this.guna2Button3.CheckedState.Parent = (CustomButtonBase) this.guna2Button3;
      this.guna2Button3.CustomImages.Parent = (CustomButtonBase) this.guna2Button3;
      this.guna2Button3.FillColor = Color.FromArgb(0, 174, 219);
      this.guna2Button3.Font = new Font("Segoe UI", 9f);
      this.guna2Button3.ForeColor = Color.White;
      this.guna2Button3.HoverState.Parent = (CustomButtonBase) this.guna2Button3;
      this.guna2Button3.Location = new Point(4, 19);
      this.guna2Button3.Name = "guna2Button3";
      this.guna2Button3.ShadowDecoration.Parent = (Control) this.guna2Button3;
      this.guna2Button3.Size = new Size(143, 41);
      this.guna2Button3.TabIndex = 13;
      this.guna2Button3.Text = "PacketSpam";
      this.guna2Button3.Click += new EventHandler(this.guna2Button3_Click);
      this.timer1.Enabled = true;
      this.timer1.Interval = 350;
      this.timer1.Tick += new EventHandler(this.timer1_Tick);
      this.guna2Elipse1.TargetControl = (Control) this;
      this.timer2.Tick += new EventHandler(this.timer2_Tick);
      this.guna2HtmlLabel5.BackColor = Color.Transparent;
      this.guna2HtmlLabel5.Font = new Font("Bahnschrift SemiCondensed", 30f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.guna2HtmlLabel5.Location = new Point(196, 34);
      this.guna2HtmlLabel5.Name = "guna2HtmlLabel5";
      this.guna2HtmlLabel5.Size = new Size(125, 50);
      this.guna2HtmlLabel5.TabIndex = 39393;
      this.guna2HtmlLabel5.Text = "CTOOLS";
      this.guna2HtmlLabel5.Click += new EventHandler(this.guna2HtmlLabel5_Click);
      this.guna2HtmlLabel6.BackColor = Color.Transparent;
      this.guna2HtmlLabel6.Font = new Font("Bahnschrift SemiCondensed", 15f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.guna2HtmlLabel6.Location = new Point(434, 24);
      this.guna2HtmlLabel6.Name = "guna2HtmlLabel6";
      this.guna2HtmlLabel6.Size = new Size(122, 26);
      this.guna2HtmlLabel6.TabIndex = 39394;
      this.guna2HtmlLabel6.Text = "CerberuS#2079";
      this.guna2HtmlLabel6.Click += new EventHandler(this.guna2HtmlLabel6_Click);
      this.AutoScaleDimensions = new SizeF(5f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(576, 410);
      this.Controls.Add((Control) this.guna2HtmlLabel6);
      this.Controls.Add((Control) this.guna2HtmlLabel5);
      this.Controls.Add((Control) this.metroTabControl1);
      this.Font = new Font("Bahnschrift SemiCondensed", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.Margin = new Padding(2, 3, 2, 3);
      this.MaximizeBox = false;
      this.Name = nameof (Form1);
      this.Padding = new Padding(17, 60, 17, 20);
      this.Resizable = false;
      this.ShadowType = MetroForm.MetroFormShadowType.DropShadow;
      this.Theme = MetroThemeStyle.Light;
      this.Load += new EventHandler(this.Form1_Load);
      this.metroTabControl1.ResumeLayout(false);
      this.metroTabPage2.ResumeLayout(false);
      this.metroTabPage2.PerformLayout();
      this.metroTabPage3.ResumeLayout(false);
      this.metroTabPage3.PerformLayout();
      this.metroTabPage1.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    public class ItemDatabase
    {
      public static List<Form1.ItemDatabase.ItemDefinition> itemDefs = new List<Form1.ItemDatabase.ItemDefinition>();

      public static bool isBackground(int itemID)
      {
        byte actionType = Form1.ItemDatabase.GetItemDef(itemID).actionType;
        switch (actionType)
        {
          case 18:
          case 23:
            return true;
          default:
            return actionType == (byte) 28;
        }
      }

      public static Form1.ItemDatabase.ItemDefinition GetItemDef(int itemID)
      {
        if (itemID < 0 || itemID > Form1.ItemDatabase.itemDefs.Count<Form1.ItemDatabase.ItemDefinition>())
          return Form1.ItemDatabase.itemDefs[0];
        Form1.ItemDatabase.ItemDefinition itemDef1 = Form1.ItemDatabase.itemDefs[itemID];
        if ((int) itemDef1.id != itemID)
        {
          foreach (Form1.ItemDatabase.ItemDefinition itemDef2 in Form1.ItemDatabase.itemDefs)
          {
            if ((int) itemDef2.id == itemID)
              return itemDef2;
          }
        }
        return itemDef1;
      }

      public static bool RequiresTileExtra(int id)
      {
        Form1.ItemDatabase.ItemDefinition itemDef = Form1.ItemDatabase.GetItemDef(id);
        if (itemDef.actionType == (byte) 2 || itemDef.actionType == (byte) 3 || (itemDef.actionType == (byte) 10 || itemDef.actionType == (byte) 13) || (itemDef.actionType == (byte) 19 || itemDef.actionType == (byte) 26 || (itemDef.actionType == (byte) 33 || itemDef.actionType == (byte) 34)) || (itemDef.actionType == (byte) 36 || itemDef.actionType == (byte) 36 || (itemDef.actionType == (byte) 38 || itemDef.actionType == (byte) 40) || (itemDef.actionType == (byte) 43 || itemDef.actionType == (byte) 46 || (itemDef.actionType == (byte) 47 || itemDef.actionType == (byte) 49))) || (itemDef.actionType == (byte) 50 || itemDef.actionType == (byte) 51 || (itemDef.actionType == (byte) 52 || itemDef.actionType == (byte) 53) || (itemDef.actionType == (byte) 54 || itemDef.actionType == (byte) 55 || (itemDef.actionType == (byte) 56 || itemDef.id == (short) 2246)) || (itemDef.actionType == (byte) 57 || itemDef.actionType == (byte) 59 || (itemDef.actionType == (byte) 61 || itemDef.actionType == (byte) 62) || (itemDef.actionType == (byte) 63 || itemDef.id == (short) 3760 || (itemDef.actionType == (byte) 66 || itemDef.actionType == (byte) 67)))) || (itemDef.actionType == (byte) 73 || itemDef.actionType == (byte) 74 || (itemDef.actionType == (byte) 76 || itemDef.actionType == (byte) 78) || (itemDef.actionType == (byte) 80 || itemDef.actionType == (byte) 81 || (itemDef.actionType == (byte) 83 || itemDef.actionType == (byte) 84)) || (itemDef.actionType == (byte) 85 || itemDef.actionType == (byte) 86 || (itemDef.actionType == (byte) 87 || itemDef.actionType == (byte) 88) || (itemDef.actionType == (byte) 89 || itemDef.actionType == (byte) 91 || (itemDef.actionType == (byte) 93 || itemDef.actionType == (byte) 97))) || (itemDef.actionType == (byte) 100 || itemDef.actionType == (byte) 101 || (itemDef.actionType == (byte) 111 || itemDef.actionType == (byte) 113) || (itemDef.actionType == (byte) 115 || itemDef.actionType == (byte) 116 || (itemDef.actionType == (byte) 127 || itemDef.actionType == (byte) 130)))))
          return true;
        return (int) itemDef.id % 2 == 0 && itemDef.id >= (short) 5818 && itemDef.id <= (short) 5932;
      }

      public void SetupItemDefs()
      {
        List<string> list1 = ((IEnumerable<string>) System.IO.File.ReadAllText("include/base.txt").Split('|')).ToList<string>();
        if (list1.Count < 3)
          return;
        int result = -1;
        int.TryParse(list1[2], out result);
        if (result == -1)
          return;
        short num = 0;
        Form1.ItemDatabase.itemDefs.Clear();
        Form1.ItemDatabase.ItemDefinition itemDefinition = new Form1.ItemDatabase.ItemDefinition();
        using (StreamReader streamReader = System.IO.File.OpenText("include/item_defs.txt"))
        {
          string empty = string.Empty;
          string str;
          while ((str = streamReader.ReadLine()) != null)
          {
            if (str.Length >= 2 && !str.Contains("//"))
            {
              List<string> list2 = ((IEnumerable<string>) str.Split('\\')).ToList<string>();
              if (!(list2[0] != "add_item"))
              {
                itemDefinition.id = short.Parse(list2[1]);
                itemDefinition.actionType = byte.Parse(list2[4]);
                itemDefinition.itemName = list2[6];
                int id = (int) itemDefinition.id;
                Form1.ItemDatabase.itemDefs.Add(itemDefinition);
                ++num;
              }
            }
          }
        }
      }

      public struct ItemDefinition
      {
        public short id;
        public byte editType;
        public byte editCategory;
        public byte actionType;
        public byte hitSound;
        public string itemName;
        public string fileName;
        public int texHash;
        public byte itemKind;
        public byte texX;
        public byte texY;
        public byte sprType;
        public byte isStripey;
        public byte collType;
        public byte hitsTaken;
        public byte dropChance;
        public int clothingType;
        public short rarity;
        public short toolKind;
        public string audioFile;
        public int audioHash;
        public short audioVol;
        public byte seedBase;
        public byte seedOver;
        public byte treeBase;
        public byte treeOver;
        public byte color1R;
        public byte color1G;
        public byte color1B;
        public byte color1A;
        public byte color2R;
        public byte color2G;
        public byte color2B;
        public byte color2A;
        public short ing1;
        public short ing2;
        public int growTime;
        public string extraUnk01;
        public string extraUnk02;
        public string extraUnk03;
        public string extraUnk04;
        public string extraUnk05;
        public string extraUnk11;
        public string extraUnk12;
        public string extraUnk13;
        public string extraUnk14;
        public string extraUnk15;
        public short extraUnkShort1;
        public short extraUnkShort2;
        public int extraUnkInt1;
      }
    }

    public class Player
    {
      public string name = "";
      public string country = "";
      public int netID;
      public int userID;
      public int invis;
      public int mstate;
      public int smstate;
      public int X;
      public int Y;
      public bool didClothingLoad;
      public bool didCharacterStateLoad;
      public Form1.Inventory inventory;

      public void SerializePlayerInventory(byte[] inventoryData)
      {
        int length = inventoryData.Length;
        this.inventory.version = inventoryData[0];
        this.inventory.backpackSpace = BitConverter.ToInt16(inventoryData, 1);
        int int16 = (int) BitConverter.ToInt16(inventoryData, 5);
        this.inventory.items = new Form1.InventoryItem[int16];
        for (int index = 0; index < int16; ++index)
        {
          int startIndex = 7 + index * 4;
          this.inventory.items[index].itemID = BitConverter.ToUInt16(inventoryData, startIndex);
          this.inventory.items[index].amount = BitConverter.ToInt16(inventoryData, startIndex + 2);
        }
      }
    }

    public struct Inventory
    {
      public byte version;
      public short backpackSpace;
      public Form1.InventoryItem[] items;
    }

    public struct InventoryItem
    {
      public short amount;
      public ushort itemID;
      public byte flags;
    }

    public struct Tile
    {
      public int x;
      public int y;
      public ushort fg;
      public ushort bg;
      public int tileState;
      public byte[] tileExtra;
      public string str_1;
      public byte type;
    }

    private class VariantList
    {
      [DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl)]
      public static extern IntPtr memcpy(IntPtr dest, IntPtr src, UIntPtr count);

      public static byte[] get_extended_data(byte[] pktData)
      {
        return ((IEnumerable<byte>) pktData).Skip<byte>(56).ToArray<byte>();
      }

      public static byte[] get_struct_data(byte[] package)
      {
        int length = package.Length;
        if (length < 60)
          return (byte[]) null;
        byte[] numArray = new byte[length - 4];
        Array.Copy((Array) package, 4, (Array) numArray, 0, length - 4);
        int int32 = BitConverter.ToInt32(package, 56);
        if (((int) package[16] & 8) != 0)
        {
          if (length >= int32 + 60)
            ;
        }
        else
          Array.Copy((Array) BitConverter.GetBytes(0), 0, (Array) package, 56, 4);
        return numArray;
      }

      public static Form1.VariantList.VarList GetCall(byte[] package)
      {
        Form1.VariantList.VarList varList = new Form1.VariantList.VarList();
        int index1 = 0;
        byte num1 = package[index1];
        int startIndex = index1 + 1;
        if (num1 > (byte) 7)
          return varList;
        varList.functionArgs = new object[(int) num1];
        for (int index2 = 0; index2 < (int) num1; ++index2)
        {
          varList.functionArgs[index2] = (object) 0;
          byte num2 = package[startIndex];
          int index3 = startIndex + 1;
          byte num3 = package[index3];
          startIndex = index3 + 1;
          switch (num3)
          {
            case 1:
              float uint32_1 = (float) BitConverter.ToUInt32(package, startIndex);
              startIndex += 4;
              varList.functionArgs[(int) num2] = (object) uint32_1;
              break;
            case 2:
              int int32_1 = BitConverter.ToInt32(package, startIndex);
              int index4 = startIndex + 4;
              string empty = string.Empty;
              string str = Encoding.ASCII.GetString(package, index4, int32_1);
              startIndex = index4 + int32_1;
              if (num2 == (byte) 0)
                varList.FunctionName = str;
              if (num2 > (byte) 0)
              {
                if (varList.FunctionName == "OnSendToServer")
                {
                  Form1.doorid = str.Substring(str.IndexOf("|") + 1);
                  if (str.Length >= 8)
                    str = str.Substring(0, str.IndexOf("|"));
                }
                varList.functionArgs[(int) num2] = (object) str;
                break;
              }
              break;
            case 5:
              uint uint32_2 = BitConverter.ToUInt32(package, startIndex);
              startIndex += 4;
              varList.functionArgs[(int) num2] = (object) uint32_2;
              break;
            case 9:
              int int32_2 = BitConverter.ToInt32(package, startIndex);
              startIndex += 4;
              varList.functionArgs[(int) num2] = (object) int32_2;
              break;
          }
        }
        return varList;
      }

      public struct VarList
      {
        public string FunctionName;
        public int netID;
        public uint delay;
        public object[] functionArgs;
      }

      public enum OnSendToServerArgs
      {
        port = 1,
        token = 2,
        userId = 3,
        IPWithExtraData = 4,
      }
    }

    public class PacketSending
    {
      private Random rand = new Random();

      public void SendData(byte[] data, ENetPeer peer, ENetPacketFlags flag = ENetPacketFlags.Reliable)
      {
        try
        {
          if (peer == null || peer.State != ENetPeerState.Connected)
            return;
          if (this.rand.Next(0, 1) == 0)
            peer.Send(data, (byte) 0, flag);
          else
            peer.Send(data, (byte) 1, flag);
        }
        catch (Exception ex)
        {
        }
      }

      public void SendPacketRaw(int type, byte[] data, ENetPeer peer, ENetPacketFlags flag = ENetPacketFlags.Reliable)
      {
        byte[] data1 = new byte[data.Length + 5];
        Array.Copy((Array) BitConverter.GetBytes(type), (Array) data1, 4);
        Array.Copy((Array) data, 0, (Array) data1, 4, data.Length);
        this.SendData(data1, peer, ENetPacketFlags.Reliable);
      }

      public void SendPacket(int type, string str, ENetPeer peer, ENetPacketFlags flag = ENetPacketFlags.Reliable)
      {
        this.SendPacketRaw(type, Encoding.ASCII.GetBytes(str.ToCharArray()), peer, ENetPacketFlags.Reliable);
      }

      public void SecondaryLogonAccepted(ENetPeer peer)
      {
        this.SendPacket(2, string.Empty, peer, ENetPacketFlags.Reliable);
      }

      public void InitialLogonAccepted(ENetPeer peer)
      {
        this.SendPacket(1, string.Empty, peer, ENetPacketFlags.Reliable);
      }
    }

    public class NetTypes
    {
      public enum PacketTypes
      {
        PLAYER_LOGIC_UPDATE,
        CALL_FUNCTION,
        UPDATE_STATUS,
        TILE_CHANGE_REQ,
        LOAD_MAP,
        TILE_EXTRA,
        TILE_EXTRA_MULTI,
        TILE_ACTIVATE,
        APPLY_DMG,
        INVENTORY_STATE,
        ITEM_ACTIVATE,
        ITEM_ACTIVATE_OBJ,
        UPDATE_TREE,
        MODIFY_INVENTORY_ITEM,
        MODIFY_ITEM_OBJ,
        APPLY_LOCK,
        UPDATE_ITEMS_DATA,
        PARTICLE_EFF,
        ICON_STATE,
        ITEM_EFF,
        SET_CHARACTER_STATE,
        PING_REPLY,
        PING_REQ,
        PLAYER_HIT,
        APP_CHECK_RESPONSE,
        APP_INTEGRITY_FAIL,
        DISCONNECT,
        BATTLE_JOIN,
        BATTLE_EVENT,
        USE_DOOR,
        PARENTAL_MSG,
        GONE_FISHIN,
        STEAM,
        PET_BATTLE,
        NPC,
        SPECIAL,
        PARTICLE_EFFECT_V2,
        ARROW_TO_ITEM,
        TILE_INDEX_SELECTION,
        UPDATE_PLAYER_TRIBUTE,
      }

      public enum NetMessages
      {
        UNKNOWN,
        SERVER_HELLO,
        GENERIC_TEXT,
        GAME_MESSAGE,
        GAME_PACKET,
        ERROR,
        TRACK,
        LOG_REQ,
        LOG_RES,
      }
    }

    internal static class RandomNumbers
    {
      private static Random r;

      public static int NextNumber()
      {
        if (Form1.RandomNumbers.r == null)
          Form1.RandomNumbers.Seed();
        return Form1.RandomNumbers.r.Next();
      }

      public static int NextNumber(int ceiling)
      {
        if (Form1.RandomNumbers.r == null)
          Form1.RandomNumbers.Seed();
        return Form1.RandomNumbers.r.Next(ceiling);
      }

      public static void Seed()
      {
        Form1.RandomNumbers.r = new Random();
      }

      public static void Seed(int seed)
      {
        Form1.RandomNumbers.r = new Random(seed);
      }
    }

    internal static class StringFunctions
    {
      private static string activeString;
      private static int activePosition;

      public static string ChangeCharacter(string sourceString, int charIndex, char newChar)
      {
        return (charIndex > 0 ? sourceString.Substring(0, charIndex) : "") + newChar.ToString() + (charIndex < sourceString.Length - 1 ? sourceString.Substring(charIndex + 1) : "");
      }

      public static bool IsXDigit(char character)
      {
        return char.IsDigit(character) || "ABCDEFabcdef".IndexOf(character) > -1;
      }

      public static string StrChr(string stringToSearch, char charToFind)
      {
        int startIndex = stringToSearch.IndexOf(charToFind);
        return startIndex > -1 ? stringToSearch.Substring(startIndex) : (string) null;
      }

      public static string StrRChr(string stringToSearch, char charToFind)
      {
        int startIndex = stringToSearch.LastIndexOf(charToFind);
        return startIndex > -1 ? stringToSearch.Substring(startIndex) : (string) null;
      }

      public static string StrStr(string stringToSearch, string stringToFind)
      {
        int startIndex = stringToSearch.IndexOf(stringToFind);
        return startIndex > -1 ? stringToSearch.Substring(startIndex) : (string) null;
      }

      public static string StrTok(string stringToTokenize, string delimiters)
      {
        if (stringToTokenize != null)
        {
          Form1.StringFunctions.activeString = stringToTokenize;
          Form1.StringFunctions.activePosition = -1;
        }
        if (Form1.StringFunctions.activeString == null)
          return (string) null;
        if (Form1.StringFunctions.activePosition == Form1.StringFunctions.activeString.Length)
          return (string) null;
        ++Form1.StringFunctions.activePosition;
        while (Form1.StringFunctions.activePosition < Form1.StringFunctions.activeString.Length && delimiters.IndexOf(Form1.StringFunctions.activeString[Form1.StringFunctions.activePosition]) > -1)
          ++Form1.StringFunctions.activePosition;
        if (Form1.StringFunctions.activePosition == Form1.StringFunctions.activeString.Length)
          return (string) null;
        int activePosition = Form1.StringFunctions.activePosition;
        do
        {
          ++Form1.StringFunctions.activePosition;
        }
        while (Form1.StringFunctions.activePosition < Form1.StringFunctions.activeString.Length && delimiters.IndexOf(Form1.StringFunctions.activeString[Form1.StringFunctions.activePosition]) == -1);
        return Form1.StringFunctions.activeString.Substring(activePosition, Form1.StringFunctions.activePosition - activePosition);
      }
    }

    private delegate void SetTextCallback(string text);
  }
}
