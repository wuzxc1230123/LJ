using LJ.Data.Sort;
using LJ.Extensions;
using LJ.Pack;
using LJ.Pack.Attributes;
using System.Reflection;

namespace LJ.Host.Pack
{
    public class PackBuilder : IPackBuilder
    {
        private readonly List<Type> _packTypes = [];

        public List<LJPack> LoadPacks { get; } = [];
        public List<TopologicalSortNode<Type>> PackNodes { get; } = [];

        public void Add<TPack>() where TPack : LJPack, new()
        {
            var packType = typeof(TPack);
            int index = _packTypes.FindIndex(a => a == packType);
            if (index != -1)
            {
                //存在则替换
                _packTypes[index] = packType;
            }
            else
            {
                //不存在则添加
                _packTypes.Add(packType);
            }
        }

        public void Load()
        {
            LoadPacks.Clear();
            PackNodes.Clear();

            foreach (var packType in _packTypes)
            {
                CraeteNode(packType);
            }
            var packs = PackNodes.TopologicalSort().Select(a => (LJPack?)Activator.CreateInstance(a.Data));
            LoadPacks.AddRange([.. packs]);
        }

        private TopologicalSortNode<Type> CraeteNode(Type packType)
        {
            var node = PackNodes.Where(a => a.Data == packType).FirstOrDefault();
            if (node != null)
            {
                return node;
            }
            node = new TopologicalSortNode<Type>(packType);
            var dependsOnPacksAttributes = packType.GetAttributes<DependsOnPacksAttribute>();
            foreach (var dependsOnPacksAttribute in dependsOnPacksAttributes)
            {
                if (!_packTypes.Any(a => a.IsAssignableFrom(dependsOnPacksAttribute.DependedPackType)))
                {
                    throw new AppException($"无法加载模块{packType.FullName}时无法找到依赖模块{dependsOnPacksAttribute.DependedPackType.FullName}");
                }
                node.Dependents.Add(CraeteNode(dependsOnPacksAttribute.DependedPackType));
            }
            PackNodes.Add(node);
            return node;
        }
    }
}
