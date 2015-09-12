using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelConverter.Model
{
    public abstract class ConverterBase<TFrom, TTo>
    {
        public abstract TTo ConvertTo(TFrom from);
    }

    public abstract class TwoWayConverterBase<TFrom, TTo> : ConverterBase<TFrom, TTo>
    {
        public abstract TFrom ConvertFrom(TTo to);

    }

    public class DataItemVertexConverter : TwoWayConverterBase<DataItem, Vertex>
    {
        private Dictionary<int, DataItem> _convertedDataItems;
        private Dictionary<int, Vertex> _convertedVertexes;

        public DataItemVertexConverter()
        {
            _convertedDataItems = new Dictionary<int,DataItem>();
            _convertedVertexes = new Dictionary<int,Vertex>();
        }

        public override Vertex ConvertTo(DataItem from)
        {
            var vertex = new Vertex(from.UID, from.Name);
            
            _convertedDataItems.Add(from.UID, from);
            _convertedVertexes.Add(vertex.UID, vertex);

            return vertex;
        }

        public override DataItem ConvertFrom(Vertex to)
        {
            return _convertedDataItems[to.UID];
        }
    }

    public class DataItem
    {
        #region UID Generation Logic

        private static int _lastUID = 0;
        private static int NextUID
        {
            get { return ++_lastUID; }
        }

        #endregion END UID Generation Logic

        public DataItem() : this(NextUID)
        {
        }

        public DataItem(int uid)
        {
            UID = uid;
            PropertyBag = new Dictionary<string,string>();
        }

        public int UID { get; private set; }
        public string Name { get; set; }
        public Dictionary<string, string> PropertyBag { get; private set; }
    }

    public struct Vertex
    {
        #region UID Generation Logic

        private static int _lastUID = 0;
        private static int NextUID
        {
            get { return ++_lastUID; }
        }

        #endregion END UID Generation Logic

        public Vertex(string label) : this(NextUID, label)
        {
        }

        public Vertex(int uid, string label) : this()
        {
            UID = uid;
            Label = label;
            PropertyBag = new Dictionary<string, string>();
        }

        public int UID { get; private set; }
        public string Label { get; set; }
        public Dictionary<string, string> PropertyBag { get; private set; }
    }

    public struct Edge
    {
        public Edge(Vertex source, Vertex target) : this()
        {
            Source = source;
            Target = target;
            PropertyBag = new Dictionary<string, string>();
        }

        public Vertex Source { get; set; }
        public Vertex Target { get; set; }
        public Dictionary<string, string> PropertyBag { get; private set; }
    }
}
