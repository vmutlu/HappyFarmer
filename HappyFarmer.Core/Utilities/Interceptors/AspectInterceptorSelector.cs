using Castle.DynamicProxy;
using System;
using System.Linq;
using System.Reflection;

namespace HappyFarmer.Core.Utilities.Interceptors
{
    public class AspectInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>(true).ToList();
            var metodAttributes = type.GetMethod(method.Name).GetCustomAttributes<MethodInterceptionBaseAttribute>(true);
            classAttributes.AddRange(metodAttributes);

            return classAttributes.OrderBy(a => a.Priority).ToArray();
        }
    }
}
