using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace Zefugi.Digraph.Model
{
    public class Project
    {
        public string Title { get; set; }

        private Dictionary<long, Model> _models { get; set; } = new Dictionary<long, Model>();

        public Dictionary<long, Model>.Enumerator GetEnumerator() => _models.GetEnumerator();

        public Model CreateModel(string title)
        {
            if (ModelExists(title))
                return null;

            var model = new Model()
            {
                ID = GetUnusedID(),
                Title = title,
            };
            _models.Add(model.ID, model);

            return model;
        }

        public void DeleteModel(long id)
        {
            if(ModelExists(id))
            {
                _models.Remove(id);
            }
        }

        public bool ModelExists(long id) => _models.ContainsKey(id);

        public bool ModelExists(string title)
        {
            string titleLowerCase = title.ToLower();
            using (var ptr = _models.GetEnumerator())
                while (ptr.MoveNext())
                    if (ptr.Current.Value.Title.ToLower() == titleLowerCase)
                        return true;
            return false;
        }

        private long GetUnusedID()
        {
            long id = 0;
            while (_models.ContainsKey(++id))
                if (id == long.MaxValue)
                    return -1;
            return id;
        }
    }
}
