using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jcw.Common.Interfaces
{
    #region IJcwTaskProgress Interface

    public interface IJcwTaskProgress: IDisposable
    {
        TaskProgressType ProgressType
        {
            get;
            set;
        }

        int Progress
        {
            get;
            set;
        }

        string ProgressMessage
        {
            get;
            set;
        }

        string ProgressMessageCaption
        {
            get;
            set;
        }
    }

    #endregion

    #region IJcwTaskResult Interface

    public interface IJcwTaskResult: IDisposable
    {
        TaskResultStatus Status
        {
            get;
            set;
        }

        string Text
        {
            get;
            set;
        }
    }

    #endregion

    #region IJcwRunTask Interface

    public interface IJcwRunTask<T>
    {
        string TaskName { set; }
        string TaskDescription { set; }
        int? TaskDuration { set; }
        T TaskArgument { set; }
        TaskToRun TaskDelegate { set; }
        IJcwTaskResult TaskResult { get; }

        void Execute ();
    }

    #endregion
}
