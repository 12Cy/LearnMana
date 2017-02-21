using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMonoGame.Tools.Logger
{
    public enum Logtarget
    {
        File, Contructor, EventLog, ParserLog
    }
    public abstract class LogBase
    {
        public abstract void Log(string message);
        protected readonly object lockobj = new object();
        public abstract void DeleteTextFile();


    }
    public class FileLogger : LogBase
    {
        public string filePath = "./LoggerOutput/FileLog.txt";

        public override void Log(string message)
        {
            //string readText = File.ReadAllText(filePath);
            //File.WriteAllText(filePath, readText + message);
            lock (lockobj)
            {

                using (StreamWriter streamWriter = new StreamWriter(filePath, true))
                {

                    streamWriter.WriteLine(message+ " | Zeit: " + System.DateTime.Now.TimeOfDay);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
            }

        }
        public override void DeleteTextFile()
        {
            using (StreamWriter streamWriter = new StreamWriter(filePath, false))
            {
                streamWriter.WriteLine("");
            }
        }
    }

    public class ContructorLogger : LogBase
    {

        public string filePath = "./LoggerOutput/ConstructorLog.txt";
        //static StreamWriter  streamWriter = new StreamWriter("./LoggerOutput/ConstructorLog.txt");

        public override void Log(string message)
        {
            //lock (lockobj)
            //{
            using (StreamWriter streamWriter = new StreamWriter(filePath, true))
            {
                streamWriter.WriteLine(message + " | Zeit: " + System.DateTime.Now.TimeOfDay);
                streamWriter.Flush();
                streamWriter.Close();
            }


        }
        public override void DeleteTextFile()
        {
            using (StreamWriter streamWriter = new StreamWriter(filePath, false))
            {
                streamWriter.Write("");
            }
        }

    }

    public class ParserLogger : LogBase
    {

        public string filePath = "./LoggerOutput/ParserLog.txt";
        //static StreamWriter  streamWriter = new StreamWriter("./LoggerOutput/ConstructorLog.txt");

        public override void Log(string message)
        {
            //lock (lockobj)
            //{
            using (StreamWriter streamWriter = new StreamWriter(filePath, true))
            {
                streamWriter.WriteLine(message + " | Zeit: " + System.DateTime.Now.TimeOfDay);
                streamWriter.Flush();
                streamWriter.Close();
            }


        }
        public override void DeleteTextFile()
        {
            using (StreamWriter streamWriter = new StreamWriter(filePath, false))
            {
                streamWriter.Write("");
            }
        }

    }

    //}





    public class EventLogger : LogBase
    {
        public override void Log(string message )
        {
            lock (lockobj)
            {
                EventLog eventLog = new EventLog("");
                eventLog.Source = "";
                eventLog.WriteEntry(message);
            }
        }
        public override void DeleteTextFile()
        {
            using (StreamWriter streamWriter = new StreamWriter("AHA", false))
            {
                streamWriter.WriteLine("");
            }
        }
    }
}


