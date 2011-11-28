using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectFlip.Test.Mock
{
    public class ServiceProviderMock : IServiceProvider
    {
        
        public object GetService(Type serviceType)
        {
            throw new NotImplementedException();
        }
    }
}
