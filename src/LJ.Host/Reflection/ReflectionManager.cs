using LJ.Reflection;
using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LJ.Host.Reflection
{
    public class ReflectionManager : IReflectionManager
    {
        private static readonly string[] Filters = ["dotnet-", "Microsoft.", "mscorlib", "netstandard", "System", "Windows"];
        private readonly List<Assembly> _allAssemblies;
        private readonly List<Type> _allAssemblieTypes;
        public ReflectionManager()
        {
            if (DependencyContext.Default == null)
            {
                _allAssemblies = [];
                _allAssemblieTypes = [];
                return;
            }
            _allAssemblies = DependencyContext.Default.GetDefaultAssemblyNames().Where(w => !Filters.Any(m => w.Name != null && w.Name.StartsWith(m)))
                .Select(Assembly.Load).ToList();
            _allAssemblieTypes = _allAssemblies.SelectMany(m => m.GetTypes()).ToList();
        }
      
        public List<Assembly> GetAssemblies(Func<Assembly, bool>? whereFunc = null)
        {
            if (whereFunc ==null)
            {
                return _allAssemblies;
            }
            return _allAssemblies.Where(whereFunc).ToList();
        }

        public List<Type> GetTypes(Func<Type, bool>? whereFunc = null)
        {
            if (whereFunc == null)
            {
                return _allAssemblieTypes;
            }
            return _allAssemblieTypes.Where(whereFunc).ToList();
        }
    }
}
