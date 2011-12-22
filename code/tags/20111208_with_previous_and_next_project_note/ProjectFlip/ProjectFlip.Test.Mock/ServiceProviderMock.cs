#region

using System;

#endregion

namespace ProjectFlip.Test.Mock
{
    public class ServiceProviderMock : IServiceProvider
    {
        #region IServiceProvider Members

        public object GetService(Type serviceType)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}