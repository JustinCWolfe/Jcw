using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

using Jcw.SnapInAPI;
using Jcw.SnapInAPI.Interfaces;

namespace Jcw.SnapInAPI.Test
{
    class Program
    {
        private static Assembly customerOverrideAssembly = null;
        private static Dictionary<Type, List<object>> calculateMethodCache = new Dictionary<Type, List<object>> ();

        static int Main (string[] args)
        {
            try
            {
                string overrideAssemblyName = ConfigurationManager.AppSettings.Get ("JcwCustomerOverrideAssembly");
                if (string.IsNullOrEmpty (overrideAssemblyName) == false)
                    customerOverrideAssembly = Assembly.LoadFrom (overrideAssemblyName);
            }
            catch (FileNotFoundException)
            {
            }

            /// Create new instance from the custom assembly
            ICalculateDiscountFactors calculateDiscountFactorsInstance =
                GetFirstInterfaceImplementor<ICalculateDiscountFactors> (typeof (JcwDiscountFactors));
            calculateDiscountFactorsInstance.CalculateDiscountFactors ();

            /// Pull it out of the cache rather than creating a new instance from the custom assembly
            ICalculateDiscountFactors calculateDiscountFactorsInstance2 =
                GetFirstInterfaceImplementor<ICalculateDiscountFactors> (typeof (JcwDiscountFactors));
            calculateDiscountFactorsInstance2.CalculateDiscountFactors ();

            /// Create new instance from the default implementation
            Type calculateRateType = typeof (ICalculateRate);
            ICalculateRate calculateRateInstance = GetFirstInterfaceImplementor<ICalculateRate> (typeof (JcwRate));
            calculateRateInstance.CalculateRate ();

            /// Pull it out of the cache 
            List<ICalculateRate> calculateRateInstances = GetInterfaceImplementors<ICalculateRate> (typeof (JcwRate));
            foreach (ICalculateRate rateCalculator in calculateRateInstances)
                rateCalculator.CalculateRate ();

            return 0;
        }

        private static T GetFirstInterfaceImplementor<T> (Type defaultTypeToUse)
        {
            Type interfaceType = typeof (T);
            if (calculateMethodCache.ContainsKey (interfaceType) &&
                calculateMethodCache[interfaceType].Count > 0)
            {
                return (T)calculateMethodCache[interfaceType][0];
            }

            Type customerOverrideType = null;
            if (customerOverrideAssembly != null)
            {
                customerOverrideType = (from t in customerOverrideAssembly.GetTypes ()
                                        where t.IsClass && t.GetInterface (interfaceType.Name) != null
                                        select t).FirstOrDefault ();
            }

            T interfaceImplementorInstance = (customerOverrideType != null) ?
                (T)customerOverrideAssembly.CreateInstance (customerOverrideType.FullName) :
                (T)Activator.CreateInstance (defaultTypeToUse);

            if (calculateMethodCache.ContainsKey (interfaceType) == false)
            {
                calculateMethodCache.Add (
                    interfaceType,
                    new List<object> (new object[] { interfaceImplementorInstance }));
            }

            return interfaceImplementorInstance;
        }

        private static List<T> GetInterfaceImplementors<T> (Type defaultTypeToUse)
        {
            Type interfaceType = typeof (T);
            if (calculateMethodCache.ContainsKey (interfaceType))
            {
                return (from instance in calculateMethodCache[interfaceType]
                        select (T)instance).ToList ();
            }

            IEnumerable<Type> customerOverrideTypes = null;
            if (customerOverrideAssembly != null)
            {
                customerOverrideTypes = from t in customerOverrideAssembly.GetTypes ()
                                        where t.IsClass && t.GetInterface (interfaceType.Name) != null
                                        select t;
            }

            List<T> instances = new List<T> ();
            if (customerOverrideTypes != null)
            {
                foreach (Type t in customerOverrideTypes)
                    instances.Add ((T)customerOverrideAssembly.CreateInstance (t.FullName));
            }
            else
                instances.Add ((T)Activator.CreateInstance (defaultTypeToUse));

            if (calculateMethodCache.ContainsKey (interfaceType) == false)
                calculateMethodCache.Add (interfaceType, (from i in instances select (object)i).ToList ());

            return instances;
        }
    }
}
