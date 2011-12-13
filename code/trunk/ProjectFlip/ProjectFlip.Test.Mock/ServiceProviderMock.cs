#region

using System;

#endregion

namespace ProjectFlip.Test.Mock
{
    public class ServiceProviderMock : IServiceProvider
    {
        #region Other

        public object GetService(Type serviceType) { return null; }

        #endregion
    }
}