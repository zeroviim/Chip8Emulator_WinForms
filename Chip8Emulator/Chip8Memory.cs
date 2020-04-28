using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chip8Emulator
{
    class Chip8Memory
    {
        public char value { get; set; } = (char)0x00;
        /* Memory Map
        0x000-0x1FF - Chip 8 interpreter (contains font set in emu)
        0x050-0x0A0 - Used for the built in 4x5 pixel font set (0-F)
        0x200-0xFFF - Program ROM and work RAM
        */
        public void createFont(Chip8Memory[] ram)
        {
            //TODO:
            //0x050 - 0x0A0 - Used for the built in 4x5 pixel font set(0 - F)
        }
        public void LoadGameIntoRAM(BinaryReader rom, Chip8Memory[] ram)
        {
            //TODO? Do we need to load the game into 0x200?
            //0x200-0xFFF - Program ROM and work RAM
        }
    }
}
