#region

using System;

#endregion

namespace ProjectFlip.Test.Mock
{
    /// <summary>
    /// The ServiceProvider mock
    /// </summary>
    /// <remarks></remarks>
    public class ServiceProviderMock : IServiceProvider
    {
        #region Other

        public object GetService(Type serviceType)
        {
            return null;
        }

        #endregion
    }
}