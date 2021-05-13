﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Obsidian.Nbt
{
    public class NbtCompound : NbtTag, IEnumerable<KeyValuePair<string, NbtTag>>
    {
        private readonly Dictionary<string, NbtTag> children = new();

        public int Count => this.children.Count;

        public NbtTag this[string name] { get => this.children[name]; set => this.children[name] = value; }

        public NbtCompound(string name = null) : base(NbtTagType.Compound)
        {
            if (this.Parent?.Type == NbtTagType.Compound && name == null)
                throw new InvalidOperationException("Tags within a compound must be named.");

            this.Name = name;
        }

        public NbtCompound(List<NbtTag> children) : this()
        {
            foreach (var child in children)
                this.Add(child.Name, child);
        }

        public NbtCompound(string name, List<NbtTag> children) : this(name)
        {
            foreach (var child in children)
                this.Add(child.Name, child);
        }

        public bool Remove(string name) => this.children.Remove(name);

        public bool HasTag(string name) => this.children.ContainsKey(name);

        public bool TryGetTag(string name, out NbtTag tag) => this.children.TryGetValue(name, out tag);

        public void Clear() => this.children.Clear();

        public override string ToString()
        {
            var sb = new StringBuilder();
            var count = this.Count;

            sb.AppendLine($"TAG_Compound('{this.Name}'): {count} {(count > 1 ? "entries" : "entry")}").AppendLine("{");

            foreach (var (_, tag) in this)
                sb.AppendLine($"  {tag}");

            sb.AppendLine("}");

            return sb.ToString();
        }

        public void Add(string name, NbtTag tag) => this.children.Add(name, tag);

        public void Add(NbtTag tag) => this.children.Add(tag.Name, tag);

        public IEnumerator<KeyValuePair<string, NbtTag>> GetEnumerator() => this.children.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}