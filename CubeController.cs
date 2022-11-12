using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CubeSolver2
{
    internal class CubeController
    {
        int[] pathArray = { 2, 4, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 2, 4, 2, 2, 2, 4, 3, 2, 2, 2, 2, 2, 3, 3, 2, 2, 2, 2, 2, 2, 2, 2, 3, 2, 3, 2, 3, 2, 4, 2, 2, 3, 2, 3 };
        string[] dirArray = { "+x", "s", "s", "s", "s", "s", "s", "s", "s", "s", "s", "s", "s", "s", "s", "s", "s", "s", "s", "s", "s", "s", "s", "s", "s", "s", "s", "s", "s", "s", "s", "s", "s", "s", "s", "s", "s", "s", "s", "s", "s", "s", "s", "s", "s", "s" };

        int startX = 0;
        int startY = 0;
        int startZ = 0;

        public string Solve() {
            Cube cube = new(startX, startY, startZ);

            if (ProcessFirstStep(ref cube)) {
                Console.WriteLine(BuildPath(45));
                return "Solved!";
            }

            return "No solution found.";
        }

        private bool ProcessFirstStep(ref Cube cube) {
            Console.WriteLine(BuildPath(0));
            if (cube.CheckPath(pathArray[0], "+x")) {
                cube.AddPath(pathArray[0], "+x");
                if (ProcessStep(cube, 1)) {
                    return true;
                }
                else {
                    cube.RemovePath(pathArray[0], "+x");
                    if (startY < 3) {
                        startY++;
                    }
                    else {
                        startY = 0;
                        startZ++;
                    }
                    if (startZ > 3) { return false; }
                    cube = new(startX, startY, startZ);
                }
            }

            return false;
        }

        private bool ProcessStep(Cube cube,int step) {
            if (step == 46) { return true; }
            
            string direction = dirArray[step];
            Console.WriteLine(BuildPath(step));

            while (direction != "o") {
                if (direction == "s") { direction = GetNextDirection(step); }
                if (cube.CheckPath(pathArray[step], direction)) {
                    cube.AddPath(pathArray[step], direction);
                    if (ProcessStep(cube, step + 1)) {
                        return true;
                    }
                    else {
                        cube.RemovePath(pathArray[step], direction);
                        direction = GetNextDirection(step);
                    }
                }
                else
                    direction = GetNextDirection(step);
            }
            
            return false;
        }

        private string GetNextDirection(int step) {
            switch (dirArray[step - 1]) {
                case "+x": case "-x":
                    switch (dirArray[step]) {
                        case "s": dirArray[step] = "+y"; return "+y";
                        case "+y": dirArray[step] = "+z"; return "+z";
                        case "+z": dirArray[step] = "-y"; return "-y";
                        case "-y": dirArray[step] = "-z"; return "-z";
                        case "-z": dirArray[step] = "s"; return "o";
                    }
                    break;
                case "+y": case "-y":
                    switch (dirArray[step]) {
                        case "s": dirArray[step] = "+x"; return "+x";
                        case "+x": dirArray[step] = "+z"; return "+z";
                        case "+z": dirArray[step] = "-x"; return "-x";
                        case "-x": dirArray[step] = "-z"; return "-z";
                        case "-z": dirArray[step] = "s"; return "o";
                    }
                    break;
                case "+z": case "-z":
                    switch (dirArray[step]) {
                        case "s": dirArray[step] = "+x"; return "+x";
                        case "+x": dirArray[step] = "+y"; return "+y";
                        case "+y": dirArray[step] = "-x"; return "-x";
                        case "-x": dirArray[step] = "-y"; return "-y";
                        case "-y": dirArray[step] = "s"; return "o";
                    }
                    break;
            }

            return "o";
        }

        private string BuildPath(int step) {
            string path = "";

            for (int i = 0; i <= step; i++) {
                path = path + dirArray[i] + pathArray[i].ToString() + "|";
            }
            return path;
        }
    }
}
