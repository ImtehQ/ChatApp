using FluentResponses.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentResponses.Models
{
    public class Responses
    {
        internal List<IResponse> responses { get; set; }
        internal Responses(IResponse response)
        {
            responses = new List<IResponse>();
            responses.Add(response);
        }
        internal Responses(List<IResponse> responsesList)
        {
            responses = responsesList;
        }

        internal int IndexOf(IResponse item)
        {
            return responses.IndexOf(item);
        }

        internal void Insert(int index, IResponse item)
        {
            responses.Insert(index, item);
        }

        internal void RemoveAt(int index)
        {
            responses.RemoveAt(index);
        }

        internal void Add(IResponse item)
        {
            responses.Add(item);
        }

        internal void Clear()
        {
            responses.Clear();
        }

        internal bool Contains(IResponse item)
        {
            return responses.Contains(item);
        }

        internal void CopyTo(IResponse[] array, int arrayIndex)
        {
            responses.CopyTo(array, arrayIndex);
        }

        internal bool Remove(IResponse item)
        {
            return responses.Remove(item);
        }

        internal IEnumerator<IResponse> GetEnumerator()
        {
            return responses.GetEnumerator();
        }

        internal IResponse Last()
        {
            return responses.Last();
        }

        internal void AddRange(IResponse[] items)
        {
            responses.AddRange(items);
        }
    }
}
