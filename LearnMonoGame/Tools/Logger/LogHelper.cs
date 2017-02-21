using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMonoGame.Tools.Logger
{
    class LogHelper
    {
        LogBase fileLogger = new FileLogger();
        LogBase constructorLogger = new ContructorLogger();
        LogBase ParserLogger = new ParserLogger();

        public void Log(Logtarget target, string message)
        {
            
            switch (target)
            {
                case (Logtarget.File):
                    fileLogger.Log(message);
                    break;
                case (Logtarget.Contructor):
                    constructorLogger.Log(message);
                    break;
                case (Logtarget.ParserLog):
                ParserLogger.Log(message);
                break;
                case Logtarget.EventLog:
                    //logger = new EventLogger();
                    //logger.Log(message);
                    break;
                default:
                    return;
            }
        }
        public void ResetAll()
        {
            constructorLogger.DeleteTextFile();
            fileLogger.DeleteTextFile();
            ParserLogger.DeleteTextFile();
        }

        static LogHelper instance;
        public static LogHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LogHelper();

                }
                    

                return instance;
            }
        }

    }
}
