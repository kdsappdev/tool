﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDT.Tools.Fix.Common.Model
{
    public class Group
    {
        public string Name { get; set; }
        public bool Required { get; set; }
        private List<Field> _fields = new List<Field>();
        public List<Field> Fields
        {
            get { return _fields; }
            set { _fields = value; }
        }
        private List<Group> _groups = new List<Group>();
        public List<Group> Groups
        {
            get { return _groups; }
            set { _groups = value; }
        }
        private List<Component> _components = new List<Component>();
        public List<Component> Components
        {
            get { return _components; }
            set { _components = value; }
        }
    }
}
