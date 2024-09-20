using LJ.DataAccess.SeedDatas;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace LJ.DataAccess.FreeSql.FreeSqlPackBuilders
{
    public class FreeSqlPackBuilder(IServiceCollection serviceCollection) : IFreeSqlPackBuilder
    {
        public List<Type> LoadDataTypes { get; } = [];


        private readonly List<Type> _dataTypess = [];
        private readonly IServiceCollection _serviceCollection = serviceCollection;


        public IFreeSqlPackBuilder AddDataProvider<TData, TDataProvider, TDataProviderImplementation>()
            where TData : class
            where TDataProvider : class, IDataProvider<TData>
            where TDataProviderImplementation : FreeSqlDataProvider<TData>, TDataProvider
        {
            var dataType = typeof(TData);
            int index = _dataTypess.FindIndex(a => a == dataType);
            if (index != -1)
            {
                //存在则替换
                _dataTypess[index] = dataType;
            }
            else
            {
                //不存在则添加
                _dataTypess.Add(dataType);
            }
            _serviceCollection.TryAddScoped<TDataProvider, TDataProviderImplementation>();

            return this;
        }

        public IFreeSqlPackBuilder AddDataProvider<TDataProvider, TDataProviderImplementation>()
            where TDataProvider : class, IDataProvider
            where TDataProviderImplementation : FreeSqlDataProvider, TDataProvider
        {
            _serviceCollection.TryAddScoped<TDataProvider, TDataProviderImplementation>();

            return this;
        }

        public IFreeSqlPackBuilder AddCodeFirstMap<TCodeFirstMap>()
            where TCodeFirstMap : class, ICodeFirstMap
        {
            _serviceCollection.AddTransient<ICodeFirstMap, TCodeFirstMap>();

            return this;
        }


        /// <summary>
        /// 添加种子数据
        /// </summary>
        /// <typeparam name="TSeedData"></typeparam>
        public IFreeSqlPackBuilder AddSeedData<TSeedData>()
            where TSeedData : class, ISeedData
        {
            _serviceCollection.AddTransient<ISeedData, TSeedData>();

            return this;
        }


        public void Load()
        {
            LoadDataTypes.Clear();

            LoadDataTypes.AddRange(_dataTypess);
        }



    }
}
