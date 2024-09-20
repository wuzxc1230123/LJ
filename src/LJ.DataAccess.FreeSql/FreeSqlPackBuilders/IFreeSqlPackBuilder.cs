using LJ.DataAccess.SeedDatas;
using LJ.Pack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LJ.DataAccess.FreeSql.FreeSqlPackBuilders
{
    /// <summary>
    /// FreeSqlPack构建器
    /// </summary>
    public interface IFreeSqlPackBuilder
    {

        /// <summary>
        /// 加载数据类型
        /// </summary>
        List<Type> LoadDataTypes { get; }

        /// <summary>
        /// 添加数据访问层
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <typeparam name="TDataProvider"></typeparam>
        /// <typeparam name="TDataProviderImplementation"></typeparam>
        IFreeSqlPackBuilder AddDataProvider<TData, TDataProvider, TDataProviderImplementation>()
            where TData : class
            where TDataProvider : class, IDataProvider<TData>
            where TDataProviderImplementation : FreeSqlDataProvider<TData>, TDataProvider;


        /// <summary>
        /// 添加数据访问层
        /// </summary>
        /// <typeparam name="TDataProvider"></typeparam>
        /// <typeparam name="TDataProviderImplementation"></typeparam>
        IFreeSqlPackBuilder AddDataProvider<TDataProvider, TDataProviderImplementation>()
            where TDataProvider : class, IDataProvider
            where TDataProviderImplementation : FreeSqlDataProvider, TDataProvider;


        /// <summary>
        /// 添加模型映射
        /// </summary>
        /// <typeparam name="TCodeFirstMap"></typeparam>
        IFreeSqlPackBuilder AddCodeFirstMap<TCodeFirstMap>()
            where TCodeFirstMap : class,ICodeFirstMap;


        /// <summary>
        /// 添加种子数据
        /// </summary>
        /// <typeparam name="TSeedData"></typeparam>
        IFreeSqlPackBuilder AddSeedData<TSeedData>()
            where TSeedData : class, ISeedData;
    }
}
