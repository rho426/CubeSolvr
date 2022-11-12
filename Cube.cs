using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CubeSolver2
{
    internal class Cube
    {
        int posX;
        int posY;
        int posZ;

        bool[,,] cubeArray = new bool[4,4,4];

        public Cube(int posX,int posY,int posZ) {
            this.posX = posX;
            this.posY = posY;
            this.posZ = posZ;

            cubeArray[posX, posY, posZ] = true;
        }

        public bool CheckPath(int length, string direction) {
            int pos_X = this.posX;
            int pos_Y = this.posY;
            int pos_Z = this.posZ;
            
            for (int i = 2; i <= length; i++) {
                switch (direction) {
                    case "+x":
                    case "-x":
                        if (!direction.StartsWith('-')) { pos_X++; } else { pos_X--; }
                        if (pos_X > 3 || pos_X < 0) { return false; }
                        break;
                    case "+y":
                    case "-y":
                        if (!direction.StartsWith('-')) { pos_Y++; } else { pos_Y--; }
                        if (pos_Y > 3 || pos_Y < 0) { return false; }
                        break;
                    case "+z":
                    case "-z":
                        if (!direction.StartsWith('-')) { pos_Z++; } else { pos_Z--; }
                        if (pos_Z > 3 || pos_Z < 0) { return false; }
                        break;
                }

                if (cubeArray[pos_X, pos_Y, pos_Z]) {
                    return false;
                }

                //cubeArray[posX, posY, posZ] = true;
            }

            return true;
        }

        public void AddPath(int length, string direction) {

            for (int i = 2; i <= length; i++) {
                switch (direction) {
                    case "+x":
                    case "-x":
                        if (!direction.StartsWith('-')) { posX++; } else { posX--; }
                        break;
                    case "+y":
                    case "-y":
                        if (!direction.StartsWith('-')) { posY++; } else { posY--; }
                        break;
                    case "+z":
                    case "-z":
                        if (!direction.StartsWith('-')) { posZ++; } else { posZ--; }
                        break;
                }

                cubeArray[posX, posY, posZ] = true;
            }
        }

        public void RemovePath(int length, string direction) {

            for (int i = 1; i < length; i++) {
                cubeArray[posX, posY, posZ] = false;
                switch (direction) {
                    case "+x":
                    case "-x":
                        if (!direction.StartsWith('-')) { posX--; } else { posX++; }
                        break;
                    case "+y":
                    case "-y":
                        if (!direction.StartsWith('-')) { posY--; } else { posY++; }
                        break;
                    case "+z":
                    case "-z":
                        if (!direction.StartsWith('-')) { posZ--; } else { posZ++; }
                        break;
                }
            }
        }
    }
}
