using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace HeightmapToObj
{
    class Program
    {
        #region Constants
        private const string c_Usage = "heightmap2obj <heightmap image> [plane scale]";
        #endregion

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine(c_Usage + "\n\nNot enough arguments!");
                return;
            }

            // load bitmap
            Bitmap bitmap = null;
            try
            {
                bitmap = (Bitmap)Image.FromFile(args[0]);
            }
            catch (Exception ex)
            {
                Console.WriteLine(c_Usage + "\n\n" + ex.Message);
                return;
            }

            // settings
            double planeDistance = 0.5;

            // set scale if found
            if (args.Length == 2)
            {
                try
                {
                    planeDistance = Double.Parse(args[1]);
                }
                catch (Exception)
                {
                    Console.WriteLine(c_Usage + "\n\nInvalid plane scale!");
                    return;
                }
            }

            // variables
            int vertexCount = bitmap.Width * bitmap.Height;
            int curVertex = 0;
            String outputFile = Path.GetFileNameWithoutExtension(args[0]) + ".obj";

            // update
            Console.WriteLine("Compiling " + vertexCount + " vertexes");

            // open file stream
            StreamWriter sw = new StreamWriter(new FileStream(outputFile, FileMode.Create, FileAccess.Write));

            // loop
            double lastPercent = -1;

            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    sw.WriteLine("v " + (x * planeDistance).ToString() + " " + (y * planeDistance).ToString() + " " + (bitmap.GetPixel(x, y).R / 2).ToString());

                    curVertex++;

                    // calculate new percent
                    double percent = Math.Round((double)curVertex / (double)vertexCount * 100, 1);

                    if (percent != lastPercent)
                    {
                        Console.SetCursorPosition(0, 1);
                        Console.Write("Building Vertexes: [" + new String(' ', percent.ToString().Length) + "%]     ");
                        Console.SetCursorPosition(20, 1);
                        Console.Write(percent);
                        Console.SetCursorPosition(24, 1);

                        lastPercent = percent;
                    }
                }
            }

            // output vertex comment
            sw.WriteLine("# " + vertexCount + " vertexes\n");

            // begin object
            sw.WriteLine("g World");

            // loop
            lastPercent = -1;
            curVertex = 0;
            int faceCount = ((bitmap.Height * 2 - 2) * (bitmap.Height - 1));

            for (int i = 0; i < faceCount / 2; i++)
			{
                // face data
                int v1;
                int v2;
                int v3;

                // write face one
                v1 = i + 1;
                v2 = i + 2;
                v3 = (i + 1) + bitmap.Width;

                sw.WriteLine("f " + v1 + " " + v2 + " " + v3);

                // write face two
                v1 = i + 2;
                v2 = (i + 2) + bitmap.Width;
                v3 = (i + 1) + bitmap.Width;

                sw.WriteLine("f " + v1 + " " + v2 + " " + v3);

                curVertex++;

                // calculate new percent
                double percent = Math.Round((double)curVertex / (double)vertexCount * 100, 1);

                if (percent != lastPercent)
                {
                    Console.SetCursorPosition(0, 1);
                    Console.Write("Building Faces: [" + new String(' ', percent.ToString().Length) + "%]       ");
                    Console.SetCursorPosition(17, 1);
                    Console.Write(percent);
                    Console.SetCursorPosition(21, 1);

                    lastPercent = percent;
                }
			}

            sw.WriteLine("# " + faceCount + " faces\n");

            // flush and close
            sw.Flush();
            sw.Close();
        }
    }
}
