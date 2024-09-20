using LJ.Data.Sort;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LJ.Extensions;

/// <summary>
/// 排序
/// </summary>
public static class SortExtensions
{

    public static List<TopologicalSortNode<T>> TopologicalSort<T>(this List<TopologicalSortNode<T>> graph)
    {
        List<TopologicalSortNode<T>> result = [];
        Queue<TopologicalSortNode<T>> queue = new();
        // 将所有入度为 0 的节点加入队列
        foreach (TopologicalSortNode<T> node in graph)
        {
            if (node.IncomingEdges == 0)
            {
                queue.Enqueue(node);
            }
        }
        // 依次将入度为 0 的节点出队，并将它的依赖节点的入度减 1
        while (queue.Count > 0)
        {
            TopologicalSortNode<T> node = queue.Dequeue();
            result.Add(node);
            foreach (TopologicalSortNode<T> dependent in node.Dependents)
            {
                dependent.IncomingEdges--;
                if (dependent.IncomingEdges == 0)
                {
                    queue.Enqueue(dependent);
                }
            }
        }
        // 如果存在入度不为 0 的节点，则说明图中存在环
        foreach (TopologicalSortNode<T> node in graph)
        {
            if (node.IncomingEdges != 0)
            {
                throw new ArgumentException("The graph contains a cycle.");
            }
        }
        return result;
    }

}
