using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chip8Emulator
{
    public partial class Form1 : Form
    {
        Chip8Processor cpu;
        Chip8Memory[] ram;
        Chip8GPU gpu;
        Chip8Input input;
        Task cycle;
        bool taskPause;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //load pong.rom
            cpu = new Chip8Processor();
            BinaryReader rom = new BinaryReader(File.OpenRead(@"C:\Users\Michael\source\repos\Chip8Emulator\pong.rom"));
            ram = new Chip8Memory[4096];
            for (int i = 0; i < ram.Length; i++)
            {
                ram[i] = new Chip8Memory();
            }
            cpu.LoadGameIntoRAM(rom, ram);
            rom.Close();
            //TODO: need to fill out static data like the font
            gpu = new Chip8GPU();
            input = new Chip8Input();
            taskPause = false;
            cpu.Initialize(cpu);
            cycle = new Task(() => Cycle(cpu, ram));
            Cycle(cpu, ram);
        }

        private void Cycle(Chip8Processor cpu, Chip8Memory[] ram)
        { 
            for (int i = 0; i < 1;)
            {
                if (taskPause == true)
                {
                    Thread.Sleep(200);
                }
                else
                {
                    /*byte[] instruction = new byte[2];
                    instruction = rom.ReadBytes(2);
                    cpu.Opcode = (byte)(instruction[0] << 8 | instruction[1]);*/
                    cpu.Cycle(cpu, ram, gpu);
                    //cpu.cycle(instruction, ram, gpu);
                }
            }
        }
    }
}
