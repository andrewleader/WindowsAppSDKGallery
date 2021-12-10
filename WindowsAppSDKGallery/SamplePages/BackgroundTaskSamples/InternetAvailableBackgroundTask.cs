using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

namespace WindowsAppSDKGallery.SamplePages.BackgroundTaskSamples
{
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [Guid("abce76b5-eeb7-4c88-ad6a-0c15dd88eea9")]
    [ComSourceInterfaces(typeof(IBackgroundTask))]
    public class InternetAvailableBackgroundTask : IBackgroundTask
    {
        public void Run(IBackgroundTaskInstance taskInstance)
        {

        }
    }
}
