using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAppSDKGallery.Helpers
{
    [ComVisible(true)]
    internal class WinRTClassFactory<T> : IClassFactory where T : new()
    {
        private Dictionary<Guid, Func<object, IntPtr>> _marshalFuncByGuid;

        public WinRTClassFactory(Dictionary<Guid, Func<object, IntPtr>> marshalFuncByGuid)
        {
            _marshalFuncByGuid = marshalFuncByGuid;
        }

        public void CreateInstance(
            [MarshalAs(UnmanagedType.Interface)] object pUnkOuter,
            ref Guid riid,
            out IntPtr ppvObject)
        {
            object obj = new T();

            if (pUnkOuter != null)
            {
                const int CLASS_E_NOAGGREGATION = unchecked((int)0x80040110);
                throw new COMException(string.Empty, CLASS_E_NOAGGREGATION);
            }

            if (riid == IID_IUnknown)
            {
                ppvObject = WinRT.MarshalInspectable<object>.FromManaged(obj);
            }
            else
            {
                Func<object, IntPtr> marshalFunc;
                if (!_marshalFuncByGuid.TryGetValue(riid, out marshalFunc))
                    throw new InvalidCastException();

                ppvObject = marshalFunc(obj);
            }
        }

        public void LockServer([MarshalAs(UnmanagedType.Bool)] bool fLock) { }

        private static readonly Guid IID_IUnknown = Guid.Parse("00000000-0000-0000-C000-000000000046");
    }
}
