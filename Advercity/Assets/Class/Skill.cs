//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using UnityEngine;
namespace AssemblyCSharp
{
    public class Skill
    {
        public string name;
        public Sprite image;
        public float damage;
        public float area;
        public int target;
        public Skill(string _name, string _path, float _damage, float _area, int _target)
        {
            name = _name;
            image = UnityEngine.Resources.Load<Sprite>(_path);
            damage = _damage;
            area = _area;
            target = _target;
        }
    }
}

