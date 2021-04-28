using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Md_exercise.Log4net
{
    public class LogHelper
    {
        public static ILog GetLogger([CallerFilePath] string fileName = "")
        {
            return LogManager.GetLogger(fileName);
        }

    }
}
