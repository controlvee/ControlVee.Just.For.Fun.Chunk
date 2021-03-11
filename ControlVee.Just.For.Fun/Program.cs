
using System;
using System.Text;
using System.Threading;

namespace ControlVee.Just.For.Fun
{
    class Program
    {
        private static string[] chunkBufferArray = new string[] { "" };

        static void Main()
        {
            // 0 = Left. 
            // 1 = Center.
            // 2 = Right.
            PrintPathTest(1);
        }

        static void PrintPathTest(byte directionOfPath)
        {
            byte direction = directionOfPath;
            string chunk = "";

            switch (direction)
            {
                case 0:
                    chunk = @"\ \";
                    break;
                default:
                    break;
            }

            // Do until fill up height of terminal buffer
            // and then clear last line.
            FillBuffer(chunk);

        }

        static void FillBuffer(string chunk)
        {


            // Get count currently in chunk buffer.
            int chunkBufferCount = chunkBufferArray.Length;

            if (chunkBufferCount > 0)
                // Clear chunks before adding chunks (this is a buffer).
                Array.Clear(chunkBufferArray, 0, chunkBufferArray.Length);


            int maxBufferCount;
            do
            {
                // Get height of terminal.
                int consoleViewHeight = Console.WindowHeight;

                // Set buffer max.
                maxBufferCount = consoleViewHeight;

                AddToBuffer(chunk, chunkBufferCount);
                Thread.Sleep(1000);
                chunkBufferCount++;
            }
            while (chunkBufferCount <= maxBufferCount);

            WriteContentsOfBuffer();

            //ClearLastLine();
        }

        private static void WriteContentsOfBuffer()
        {
            for (int i = 0; i < chunkBufferArray.Length; i++)
            {
                Console.Write(chunkBufferArray[i].ToString());
            }
        }

        private static void AddToBuffer(string chunk, int chunkBufferCount)
        {
            StringBuilder chunks = new StringBuilder();

            // Write at 1/2 value of buffer width to stay in middle.
            // We may need to round.
            int consoleViewWidth = Console.WindowWidth;
            int numberCharUntilMiddle = (int)Math.Round(consoleViewWidth / 2.0) - 3;
            char spaceChar = ' ';

            chunk = "| |";

            // Add to buffer.
            do
            {
                chunks.Append(spaceChar);
                numberCharUntilMiddle--;
            }
            while (numberCharUntilMiddle > 0);

            chunks.Append(chunk);

            // Optimize?
            chunkBufferArray.SetValue(chunks.ToString(), chunkBufferCount - 1);
        }

        public static void ClearLastLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);

            Console.Write(new string(' ', Console.BufferWidth));

            Console.SetCursorPosition(0, Console.CursorTop - 1);
        }
    }
}