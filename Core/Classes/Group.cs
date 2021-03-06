﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace SkyNinja.Core.Classes
{
    public class Group
    {
        private readonly List<string> parts = new List<string>();

        public Group(params string[] parts)
        {
            this.parts.AddRange(parts);
        }

        /// <summary>
        /// Get all parts except the last.
        /// </summary>
        public string[] Head
        {
            get
            {
                return parts.Take(parts.Count - 1).ToArray();
            }
        }

        /// <summary>
        /// Get the last part.
        /// </summary>
        public string Tail
        {
            get
            {
                return parts[parts.Count - 1];
            }
        }

        public void Add(Group group)
        {
            this.parts.AddRange(group.parts);
        }

        public override string ToString()
        {
            return String.Join("/", parts);
        }

        public bool Equals(Group other)
        {
            return other != null && parts.SequenceEqual(other.parts);
        }
    }
}
