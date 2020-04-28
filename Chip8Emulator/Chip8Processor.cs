using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chip8Emulator
{
    class Chip8Processor
    {
        //internal use
        List<string> logging = new List<string>();
        //emulator use
        public char[] v = new char[16]; //registers
        public ushort i; //index register
        public ushort pc = 0; //program counter
        public char delayTimer; //delay timer counts at 60Hz
        public char soundTimer; //sound Timer counts at 60Hz
        public short[] stack = new short[16]; //stack
        public short sp; //stack pointer
        public short Opcode;


        public void Initialize(Chip8Processor cpu)
        {
            cpu.pc = 0x200;
            cpu.i = 0;
            cpu.sp = 0;
            cpu.Opcode = 0;
        }

        public void LoadGameIntoRAM(BinaryReader rom, Chip8Memory[] ram)
        {
            for (int i = 0; i < rom.BaseStream.Length; i++)
            {
                ram[i + 0x200].value = (char)(rom.ReadByte());
                //ram[i + 0x200].value = rom.ReadChar();
            }
        }

        public void LoadFont(Chip8Memory[] ram)
        {

        }

        public void Cycle(Chip8Processor cpu, Chip8Memory[] ram, Chip8GPU gpu)
        {
            cpu.Opcode = (short)(ram[cpu.pc].value << 8 | ram[cpu.pc + 1].value);
            char test = (char)(cpu.Opcode & 0xF000);
            switch (cpu.Opcode & 0xF000)
            {
                case 0x0000: //call, disp_clear, return
                    if ((char)(cpu.Opcode >> 12 & 0x0) != 0x00) //call (0NNN)
                    {

                    }
                    else
                    {
                        if ((char)(cpu.Opcode & 0x000E) != 0x000E) //return (00EE)
                        {

                        }
                        else //disp_clear (00E0)
                        {

                        }
                    }
                    return;
                case 0x1000: //goto NNN (1NNN)
                    cpu.pc = (ushort)(cpu.Opcode & 0x0FFF);
                    //TODO: make sure pc doesnt have to move 2 after this as well
                    //TODO: gotos and jumps suck make sure this emulates right once more of emu is working
                    return;
                case 0x2000: //call subroutine
                    return;
                case 0x3000: //skip next inst if Vx == xxNN
                    return;
                case 0x4000: //skip next inst if Vx != xxNN
                    return;
                case 0x6000: //set Vx to value NN 
                    v[cpu.Opcode & 0x0F] = (char)(cpu.Opcode & 0x00FF);
                    cpu.pc += 2;
                    return;
                case 0xA000: //set I to ANNN for address use
                    logging.Add(string.Format("Set I to {0}", (ushort)(cpu.Opcode & 0x0FFF)));
                    i = (ushort)(cpu.Opcode & 0x0FFF);
                    pc += 2;
                    return;
                case 0xD000: //draw Vx, Vy, N (DXYN)

                    pc += 2;
                    return;
            }
        }
        #region old_cycle_code
        /*public void cycle(byte[] input, Chip8Memory[] ram, Chip8GPU gpu) //opcode interpreter
        {
            logging.Add("Test");
            ram[pc].value = (char)input[0];
            ram[pc + 1].value = (char)input[1];
            short opcode = (short)(ram[pc].value << 8 | ram[pc + 1].value);
            //TODO: add debugging to logging
            switch (ram[pc].value & 0xF0) //opcode decoding 2, once determined its not a call
            {
                case 0x00: //call, disp_clear, return
                    if ((char)(ram[pc].value & 0x00) != 0x00) //call
                    {

                    }
                    else
                    {
                        if ((char)(ram[pc+1].value & 0x0E) != 0x0E) //return
                        {

                        }
                        else //disp_clear
                        {

                        }
                    }
                    return;
                case 0x10: //goto
                    
                    return;
                case 0x20: //call subroutine
                    return;
                case 0x30: //skip next inst if Vx == xxNN
                    return;
                case 0x40: //skip next inst if Vx != xxNN
                    return;
                case 0x60: //set Vx to value NN 
                    short register = (short)(ram[pc].value & 0x0F);
                    v[register] = ram[pc + 1].value;
                    return;
                case 0xA0: //set I to ANNN for address use
                    short value = (short)(ram[pc].value & 0x0F);
                    i = (ushort)(value << 8 | ram[pc + 1].value);
                    return;
            }
            pc += 2; //advance pc
        }*/
        #endregion
    }
}
