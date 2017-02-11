using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IocContainer.InstanceFactories;
using IocContainer.Interfaces;

namespace IocContainer
{
    public class IoCContainer
    {
        private static readonly Dictionary<Type, BoundImplementation> Bindings = new Dictionary<Type, BoundImplementation>();
        private static readonly TransientInstanceFactory TransientInstanceFactory = new TransientInstanceFactory();

        public void Register<TBindTo, TImplementation>() where TImplementation : TBindTo
        {
            var boundImplementation = new BoundImplementation
            {
                Type = typeof (TImplementation),
                InstanceFactory = TransientInstanceFactory
            };

            Bindings.Add(typeof (TBindTo), boundImplementation);
        }
    }
}
