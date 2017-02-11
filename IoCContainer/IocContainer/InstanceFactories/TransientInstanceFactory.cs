using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IocContainer.Interfaces;

namespace IocContainer.InstanceFactories
{
    public class TransientInstanceFactory : IInstanceFactory
    {
        public T BuildInstance<T>()
        {
            throw new NotImplementedException();
        }
    }
}
